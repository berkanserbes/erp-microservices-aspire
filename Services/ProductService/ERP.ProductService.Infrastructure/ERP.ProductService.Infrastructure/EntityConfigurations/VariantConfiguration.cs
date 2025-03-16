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

public class VariantConfiguration : BaseEntityConfiguration<Variant>
{

	public override void Configure(EntityTypeBuilder<Variant> builder)
	{
		builder.ToTable("variants");

		builder.Property(v => v.Code)
				.IsRequired()
				.HasColumnName("code")
				.HasMaxLength(20)
				.HasColumnType("varchar(20)");

		builder.Property(v => v.Name)
				.IsRequired()
				.HasColumnName("name")
				.HasMaxLength(40)
				.HasColumnType("varchar(40)");

		builder.Property(v => v.ProductId)
				.IsRequired()
				.HasColumnName("product_id")
				.HasColumnType("uuid");

		builder.HasOne(v => v.Product)
			   .WithMany(p => p.Variants) 
			   .HasForeignKey(v => v.ProductId)
		       .OnDelete(DeleteBehavior.Cascade);

		base.Configure(builder);
	}
}
