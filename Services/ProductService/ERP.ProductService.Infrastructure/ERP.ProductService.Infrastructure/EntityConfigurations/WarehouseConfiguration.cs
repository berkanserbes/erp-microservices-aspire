using ERP.Shared.Domain.Entities;
using ERP.Shared.Infrastructure.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.ProductService.Infrastructure.EntityConfigurations;

public class WarehouseConfiguration : BaseEntityConfiguration<Warehouse>
{
	public override void Configure(EntityTypeBuilder<Warehouse> builder)
	{
		builder.ToTable("warehouses");

		builder.Property(x => x.Number)
			   .HasColumnName("number")
			   .HasColumnType("int")
			   .IsRequired();

		builder.Property(x => x.Name)
			.HasColumnName("name")
			.HasColumnType("varchar(50)")
			.HasMaxLength(50)
			.IsRequired();

		//builder.HasMany(x => x.Locations)
		//	   .WithOne()
		//	   .HasForeignKey(x => x.WarehouseNumber)
		//	   .OnDelete(DeleteBehavior.Cascade);

		base.Configure(builder);
	}
}
