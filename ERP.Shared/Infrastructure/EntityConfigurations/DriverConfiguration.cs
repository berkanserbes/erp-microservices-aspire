using ERP.Shared.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Shared.Infrastructure.EntityConfigurations;
public class DriverConfiguration : BaseEntityConfiguration<Driver>
{
	public override void Configure(EntityTypeBuilder<Driver> builder)
	{
		base.Configure(builder);

		builder.ToTable("drivers");

		builder.Property(b => b.Name)
			   .HasColumnName("code")
			   .HasColumnType("varchar(20)")
			   .HasMaxLength(20)
			   .IsRequired();

		builder.Property(b => b.Surname)
			   .HasColumnName("name")
			   .HasColumnType("varchar(40)")
			   .HasMaxLength(40)
			   .IsRequired();

		builder.Property(b => b.IdentityNumber)
			   .HasColumnName("identity_number")
			   .HasColumnType("varchar(11)")
			   .HasMaxLength(11);
	}
}
