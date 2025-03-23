using ERP.Shared.Domain.Entities.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Shared.Infrastructure.EntityConfigurations;

public class BaseTransactionLineConfiguration : BaseEntityConfiguration<BaseTransactionLine>
{
	public override void Configure(EntityTypeBuilder<BaseTransactionLine> builder)
	{
		base.Configure(builder);

		builder.Property(b => b.ProductCode)
			   .HasColumnName("product_code")
			   .HasColumnType("varchar(20)")
			   .HasMaxLength(20)
			   .IsRequired();

		builder.Property(b => b.ProductName)
			   .HasColumnName("product_name")
			   .HasColumnType("varchar(40)")
			   .HasMaxLength(40)
			   .IsRequired();

		builder.Property(b => b.VariantCode)
			   .HasColumnName("variant_code")
			   .HasColumnType("varchar(20)")
			   .HasMaxLength(20)
			   .IsRequired(false);

		builder.Property(b => b.VariantName)
			   .HasColumnName("variant_name")
			   .HasColumnType("varchar(40)")
			   .HasMaxLength(40)
			   .IsRequired(false);

		builder.Property(b => b.SubUnitsetCode)
			   .HasColumnName("sub_unitset_code")
			   .HasColumnType("varchar(15)")
			   .HasMaxLength(15);

		builder.Property(b => b.UnitsetCode)
			   .HasColumnName("unitset_code")
			   .HasColumnType("varchar(15)")
			   .HasMaxLength(15);

		builder.Property(b => b.Quantity)
			   .HasColumnName("quantity")
			   .HasColumnType("double precision");

		builder.Property(b => b.ConversionRate)
			   .HasColumnName("conversion_rate")
			   .HasColumnType("double precision");

		builder.Property(b => b.OtherConversionRate)
			   .HasColumnName("other_conversion_rate")
			   .HasColumnType("double precision");
	}
}
