using Aspire.Hosting;

var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedis("cache");

var sqlserver = builder.AddSqlServer("sqlserver")
  // Mount the init scripts directory into the container.
  .WithBindMount("./sqlserverconfig", "/usr/config")
  // Mount the SQL scripts directory into the container so that the init scripts run.
  .WithBindMount("../AspireSample.ApiService/data/sqlserver", "/mnt/sql-init")
  // Run the custom entrypoint script on startup.
  //.WithEntrypoint("/usr/config/entrypoint.sh")
  // Configure the container to store data in a volume so that it persists across instances.
  .WithDataVolume()
  .WithLifetime(ContainerLifetime.Persistent);

var db = sqlserver
  .AddDatabase(name: "master");

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
