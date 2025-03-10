using Aspire.Hosting;

var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedis("cache");

var sqlserver = builder.AddSqlServer("sqlserver")
  // Configure the container to store data in a volume so that it persists across instances.
  .WithDataVolume()
  .WithLifetime(ContainerLifetime.Persistent);

var db = sqlserver
  .AddDatabase("master");

var apiService = builder.AddProject<Projects.AspireSample_ApiService>("apiservice")
  .WithReference(db)
  .WaitFor(db);

builder.AddProject<Projects.AspireSample_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(cache)
    .WaitFor(cache)
    .WithReference(apiService)
    .WaitFor(apiService);

builder.Build().Run();
