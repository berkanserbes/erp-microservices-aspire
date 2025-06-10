using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddOpenApi();

builder.Configuration
	.SetBasePath(Directory.GetCurrentDirectory())
	.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
	.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true)
	.AddEnvironmentVariables()
	.AddOcelot();

builder.Services.AddOcelot(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//	app.MapOpenApi();
//}

//app.UseHttpsRedirection();

await app.UseOcelot();

await app.RunAsync();
