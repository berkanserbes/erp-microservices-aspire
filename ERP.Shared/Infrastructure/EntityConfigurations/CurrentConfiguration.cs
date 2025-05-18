using ERP.Shared.Domain.Entities;
using ERP.Shared.Domain.Entities.Abstract;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Shared.Infrastructure.EntityConfigurations;

public class CurrentConfiguration<T> : BaseEntityConfiguration<T> where T : Current
{
	public override void Configure(EntityTypeBuilder<T> builder)
	{
		base.Configure(builder);

		builder.Property(b => b.Code)
			   .HasColumnName("code")
			   .HasColumnType("varchar(20)")
			   .HasMaxLength(20)
			   .IsRequired();

		builder.Property(b => b.Name)
			   .HasColumnName("name")
			   .HasColumnType("varchar(40)")
			   .HasMaxLength(40)
			   .IsRequired();

		builder.Property(b => b.Email)
			   .HasColumnName("email")
			   .HasColumnType("varchar(255)")
			   .HasMaxLength(255);

		builder.Property(b => b.Phone)
			   .HasColumnName("phone")
			   .HasColumnType("varchar(15)")
			   .HasMaxLength(15);

		builder.Property(b => b.Address)
			.HasColumnName("address")
			.HasColumnType("varchar(255)")
			.HasMaxLength(255);
	}
}
