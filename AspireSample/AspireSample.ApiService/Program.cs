using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire client integrations.
builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddProblemDetails();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

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
}

string[] summaries = ["Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"];

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

// app.MapGet("/postalcode", (HttpContext context) =>
// {
//     using var reader = new StreamReader("nl_subset.geojson");
//     var json = reader.ReadToEnd();
//     var geoJson = JsonDocument.Parse(json);

//     var city = context.Request.Query["city"].ToString();
//     var street = context.Request.Query["street"].ToString();
//     var houseNumber = context.Request.Query["housenumber"].ToString();
//     var features = geoJson.RootElement.GetProperty("features").EnumerateArray();

//     var result = features
//         .Where(feature => 
//             feature.GetProperty("properties").GetProperty("woonplaatsnaam").GetString() == city &&
//             feature.GetProperty("properties").GetProperty("openbareruimtenaam").GetString() == street &&
//             feature.GetProperty("properties").GetProperty("huisnummer").GetString() == houseNumber)
//         .Select(feature => new
//         {
//             Coordinates = feature.GetProperty("geometry").GetProperty("coordinates").EnumerateArray().Select(c => c.GetDouble()).ToArray(),
//             Properties = feature.GetProperty("properties").EnumerateObject().ToDictionary(p => p.Name, p => p.Value.ToString())
//         })
//         .ToList();

//     if (result.Any())
//     {
//         context.Response.WriteAsJsonAsync(result);
//     }
//     else
//     {
//         context.Response.StatusCode = StatusCodes.Status404NotFound;
//     }
// }).WithName("GetPostalCode");

app.MapDefaultEndpoints();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
