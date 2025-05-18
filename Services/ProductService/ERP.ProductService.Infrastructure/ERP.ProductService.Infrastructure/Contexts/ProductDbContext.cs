using ERP.Shared.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ERP.ProductService.Infrastructure.Contexts;

public class ProductDbContext : DbContext
{	public DbSet<Product> Products { get; set; }
	public DbSet<Variant> Variants { get; set; }
	public DbSet<Warehouse> Warehouses { get; set; }	public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
	{
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductDbContext).Assembly);

		base.OnModelCreating(modelBuilder);
	}

}
