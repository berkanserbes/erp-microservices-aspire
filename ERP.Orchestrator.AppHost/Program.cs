var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres(name: "postgres");
var postgresdb = postgres.AddDatabase("postgresdb");




builder.Build().Run();
