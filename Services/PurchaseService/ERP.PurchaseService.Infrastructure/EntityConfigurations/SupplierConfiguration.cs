using ERP.Shared.Domain.Entities;
using ERP.Shared.Infrastructure.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ERP.PurchaseService.Infrastructure.EntityConfigurations;

public class SupplierConfiguration : CurrentConfiguration<Supplier>
{
	public override void Configure(EntityTypeBuilder<Supplier> builder)
	{
		base.Configure(builder);

		builder.ToTable("suppliers");
	}
}
