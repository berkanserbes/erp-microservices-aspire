var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres(name: "postgres");
var postgresdb = postgres.AddDatabase("postgresdb");

//var redis = builder.AddRedis("cache");

builder.AddProject<Projects.ERP_ProductService_API>("productService")
												   .WithReference(postgres);

builder.AddProject<Projects.ERP_SalesService_API>("salesService");

builder.AddProject<Projects.ERP_PurchaseService_API>("purchaseService");

builder.Build().Run();
