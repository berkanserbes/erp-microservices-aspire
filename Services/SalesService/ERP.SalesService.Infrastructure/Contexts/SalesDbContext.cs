using ERP.Shared.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ERP.SalesService.Infrastructure.Contexts;

public class SalesDbContext : DbContext
{
	public DbSet<Customer> Customers { get; set; }

	public SalesDbContext(DbContextOptions<SalesDbContext> options) : base(options)
	{

	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfigurationsFromAssembly(typeof(SalesDbContext).Assembly);
		base.OnModelCreating(modelBuilder);
	}
}
