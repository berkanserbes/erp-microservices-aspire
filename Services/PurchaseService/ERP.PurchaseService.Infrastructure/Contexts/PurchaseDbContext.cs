using ERP.Shared.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ERP.PurchaseService.Infrastructure.Contexts;

public class PurchaseDbContext : DbContext
{
	public DbSet<Supplier> Suppliers { get; set; }

	public PurchaseDbContext(DbContextOptions<PurchaseDbContext> options) : base(options)
	{
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfigurationsFromAssembly(typeof(PurchaseDbContext).Assembly);
		base.OnModelCreating(modelBuilder);
	}
}
