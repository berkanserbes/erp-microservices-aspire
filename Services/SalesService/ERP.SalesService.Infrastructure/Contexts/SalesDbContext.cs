using ERP.Shared.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.SalesService.Infrastructure.Contexts;

public class SalesDbContext : DbContext
{
	public DbSet<Customer> Customers { get; set; }
}
