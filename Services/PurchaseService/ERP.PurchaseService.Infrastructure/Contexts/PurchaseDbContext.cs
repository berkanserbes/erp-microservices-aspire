using ERP.Shared.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ERP.PurchaseService.Infrastructure.Contexts;

public class PurchaseDbContext : DbContext
{
	public DbSet<Supplier> Suppliers { get; set; }
}
