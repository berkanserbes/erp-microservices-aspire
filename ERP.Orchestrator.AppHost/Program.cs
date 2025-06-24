using ERP.Orchestrator.AppHost.Extensions;

var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres(name: "postgres").WithPgAdmin();
var productDb = postgres.AddDatabase("ProductDatabase", "erp_product_db");
var salesDb = postgres.AddDatabase("SalesDatabase", "erp_sales_db");
var purchaseDb = postgres.AddDatabase("PurchaseDatabase", "erp_purchase_db");

builder.AddContainer(name: "portainer",
					 image: "portainer/portainer-ce",
					 tag: "latest")
		 .WithHttpEndpoint(port: 9000, targetPort: 9000);

builder.AddContainer(name: "grafana",
					image: "grafana/grafana",
					tag: "latest")
		.WithHttpEndpoint(port: 3000, targetPort: 3000);

builder.AddContainer(name: "prometheus",
					image: "prom/prometheus",
					tag: "latest")
		.WithHttpEndpoint(port: 9090, targetPort: 9090);



//var redis = builder.AddRedis("cache");

builder.AddProject<Projects.ERP_ProductService_API>("productService").WithSwaggerUI().WithScalar().WithReDoc();

builder.AddProject<Projects.ERP_SalesService_API>("salesService").WithSwaggerUI().WithScalar().WithReDoc();

builder.AddProject<Projects.ERP_PurchaseService_API>("purchaseService").WithSwaggerUI().WithScalar().WithReDoc();

builder.Build().Run();
