using ERP.ProductService.Application.Services;
using ERP.ProductService.Infrastructure.Contexts;
using ERP.ProductService.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
// Add services to the container.

builder.Services.AddDbContext<ProductDbContext>(options =>
{
	//Configure postgresql db
	options.UseNpgsql(builder.Configuration.GetConnectionString("ProductDatabase"));
});

builder.Services.AddControllers();

builder.Services.AddScoped<IWarehouseService, WarehouseService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IVariantService, VariantService>();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.MapOpenApi();

	app.UseSwagger();
	app.UseSwaggerUI();
	
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
