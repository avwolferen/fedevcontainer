using AspireSample.ApiService;
using AspireSample.ApiService.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire client integrations.
builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddProblemDetails();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<FedDbContext>(options =>
{
  options.UseSqlServer(builder.Configuration.GetConnectionString("master") ?? throw new InvalidOperationException("Connection string 'database' not found."));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
  app.MapOpenApi();

  app.UseSwaggerUI(options =>
  {
    options.SwaggerEndpoint("/openapi/v1.json", "v1");
  });

  // Retrieve an instance of the DbContext class and manually run migrations during startup
  using (var scope = app.Services.CreateScope())
  {
    var context = scope.ServiceProvider.GetRequiredService<FedDbContext>();
    context.Database.EnsureCreated();
    //    context.Database.Migrate();

    if (context.PostalCodes.Count() == 0)
    {
      JsonDocument jsonDoc = JsonDocument.Parse(System.IO.File.ReadAllText("Data/sqlserver/apeldoorn.json"));
      int id = 1;
      var nextId = 1;
      jsonDoc.RootElement.GetProperty("features").EnumerateArray().ToList().ForEach(je =>
      {
        if (id > 1000)
        {
          return;
        }

        var properties = je.GetProperty("properties");
        nextId = id;
        var postalCode = new PostalCode
        {
          Id = nextId,
          Code = properties.GetProperty("postcode").GetString(),
          City = properties.GetProperty("woonplaatsnaam").GetString(),
          HouseNumber = properties.GetProperty("huisnummer").GetInt16().ToString(),
          StreetName = properties.GetProperty("openbareruimtenaam").GetString()
        };
        context.PostalCodes.Add(postalCode);

        context.SaveChanges();
        id++;
      });
    }
  }
}

app.MapWeatherEndpoints();
app.MapPostalCodeEndpoints();

app.MapDefaultEndpoints();

app.Run();