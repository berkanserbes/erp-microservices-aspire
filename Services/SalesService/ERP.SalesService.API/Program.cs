using ERP.SalesService.Application.Services;
using ERP.SalesService.Infrastructure.Contexts;
using ERP.SalesService.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.Services.AddHealthChecks();
// Add services to the container.

builder.Services.AddDbContext<SalesDbContext>(options =>
{
	// Configure postgresql db
	options.UseNpgsql(builder.Configuration.GetConnectionString("SalesDatabase"));
});


builder.Services.AddControllers();
builder.Services.AddMemoryCache();

builder.Services.AddScoped<ICustomerService, CustomerService>();

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

app.MapHealthChecks("/health");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
