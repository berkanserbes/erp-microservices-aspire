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

public class ProductConfiguration : BaseEntityConfiguration<Product>
{
	public override void Configure(EntityTypeBuilder<Product> builder)
	{
		base.Configure(builder);

		builder.ToTable("products");

		builder.Property(p => p.Code)
				.IsRequired()
				.HasColumnName("code")
				.HasMaxLength(20)
				.HasColumnType("varchar(20)");

		builder.Property(p => p.Name)
				.IsRequired()
				.HasColumnName("name")
				.HasMaxLength(40)
				.HasColumnType("varchar(40)");

		builder.Property(p => p.LotTracking)
				.IsRequired()
				.HasColumnName("lot_tracking")
				.HasColumnType("smallint"); 

		builder.Property(p => p.LocationTracking)
				.IsRequired()
				.HasColumnName("location_tracking")
				.HasColumnType("smallint");

		builder.Property(p => p.VariantTracking)
				.IsRequired()
				.HasColumnName("variant_tracking")
				.HasColumnType("smallint");

		builder.HasMany(p => p.Variants)
			   .WithOne(v => v.Product)
			   .HasForeignKey(v => v.ProductId)
			   .OnDelete(DeleteBehavior.Cascade);
	}
}
