using ERP.Shared.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.ProductService.Infrastructure;

public class ProductDbContext : DbContext
{
	public DbSet<Product> Products { get; set; }
	public DbSet<Variant> Variants { get; set; }
}
