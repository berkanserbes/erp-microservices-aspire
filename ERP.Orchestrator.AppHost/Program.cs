var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres(name: "postgres");
var postgresdb = postgres.AddDatabase("postgresdb");

//var redis = builder.AddRedis("cache");


builder.Build().Run();
