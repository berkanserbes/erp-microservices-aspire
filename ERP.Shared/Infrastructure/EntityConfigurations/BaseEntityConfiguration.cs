using ERP.Shared.Domain.Entities.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Shared.Infrastructure.EntityConfigurations;

public class BaseEntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : BaseEntity
{
	public virtual void Configure(EntityTypeBuilder<TEntity> builder)
	{
		builder.HasKey(e => e.Id);

		builder.Property(e => e.Id)
				.HasColumnName("id");

		builder.Property(x => x.CreatedAt)
			   .IsRequired(true)
			   .HasColumnType("timestamp")
			   .HasColumnName("created_at")
			   .HasDefaultValueSql("NOW()");

		builder.Property(x => x.UpdatedAt)
			   .IsRequired(false)
			   .HasColumnType("timestamp")
			   .HasColumnName("updated_at");
	}
}
