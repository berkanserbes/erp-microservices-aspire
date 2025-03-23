using ERP.Shared.Domain.Entities.Abstract;
using ERP.Shared.Infrastructure.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ERP.SalesService.Infrastructure.EntityConfigurations;

public class CustomerConfiguration : CurrentConfiguration
{
	public override void Configure(EntityTypeBuilder<Current> builder)
	{
		base.Configure(builder);

		builder.ToTable("customers");
	}
}
