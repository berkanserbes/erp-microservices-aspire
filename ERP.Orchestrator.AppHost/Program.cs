var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres(name: "postgres");
var postgresdb = postgres.AddDatabase("postgresdb");

//var redis = builder.AddRedis("cache");

builder.AddProject<Projects.ERP_ProductService_API>("productService")
												   .WithReference(postgres);


builder.Build().Run();
