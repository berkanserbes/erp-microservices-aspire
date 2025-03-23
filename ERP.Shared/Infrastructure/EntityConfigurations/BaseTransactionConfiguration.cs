using ERP.Shared.Domain.Entities.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Shared.Infrastructure.EntityConfigurations;

public class BaseTransactionConfiguration : BaseEntityConfiguration<BaseTransaction>
{
	public override void Configure(EntityTypeBuilder<BaseTransaction> builder)
	{
		base.Configure(builder);

		builder.Property(b => b.Code)
			   .HasColumnName("code")
			   .HasColumnType("varchar(20)")
			   .HasMaxLength(20);

		builder.Property(b => b.TransactionDate)
			   .HasColumnName("transaction_date")
			   .HasColumnType("timestamp");

		builder.Property(b => b.Description)
			   .HasColumnName("description")
			   .HasColumnType("varchar(100)")
			   .HasMaxLength(100);

		builder.Property(b => b.DocumentNumber)
			   .HasColumnName("document_number")
			   .HasColumnType("varchar(30)")
			   .HasMaxLength(30);

		builder.Property(b => b.WarehouseNumber)
			   .HasColumnName("warehouse_number")
			   .HasColumnType("int");
	}
}
