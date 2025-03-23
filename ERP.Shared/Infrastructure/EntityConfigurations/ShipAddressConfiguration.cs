using ERP.Shared.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ERP.Shared.Infrastructure.EntityConfigurations;

public class ShipAddressConfiguration : BaseEntityConfiguration<ShipAddress>
{
	public override void Configure(EntityTypeBuilder<ShipAddress> builder)
	{
		base.Configure(builder);

		builder.ToTable("ship_addresses");

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

		builder.Property(b => b.Address)
			.HasColumnName("address")
			.HasColumnType("varchar(255)")
			.HasMaxLength(255);

		builder.Property(b => b.City)
			   .HasColumnName("city")
			   .HasColumnType("varchar(35)")
			   .HasMaxLength(35);

		builder.Property(b => b.Country)
			   .HasColumnName("country")
			   .HasColumnType("varchar(30)")
			   .HasMaxLength(30);
	}
}
