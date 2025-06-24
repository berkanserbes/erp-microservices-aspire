using ERP.PurchaseService.Application.Services;
using ERP.PurchaseService.Infrastructure.Contexts;
using ERP.PurchaseService.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.Services.AddHealthChecks();
// Add services to the container.

builder.Services.AddDbContext<PurchaseDbContext>(options =>
{
	// Configure postgresql db
	options.UseNpgsql(builder.Configuration.GetConnectionString("PurchaseDatabase"));
});

builder.Services.AddControllers();
builder.Services.AddMemoryCache();

builder.Services.AddScoped<ISupplierService, SupplierService>();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MapOpenApi();
app.UseSwagger();
app.UseSwaggerUI();
app.UseReDoc(options =>
{
	options.SpecUrl("/openapi/v1.json");
});
app.MapScalarApiReference();

app.MapHealthChecks("/health");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
