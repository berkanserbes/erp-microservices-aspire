using ERP.Shared.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ERP.ProductService.Infrastructure.Contexts;

public class ProductDbContext : DbContext
{
	public DbSet<Product> Products { get; set; }
	public DbSet<Variant> Variants { get; set; }
	public DbSet<Warehouse> Warehouses { get; set; }
}
