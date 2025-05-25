using ERP.Shared.Domain.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Shared.Domain.Entities;

public class Product : BaseEntity
{
	public string Code { get; set; } = string.Empty;
	public string Name { get; set; } = string.Empty;
	public short LotTracking { get; set; }
	public short LocationTracking { get; set; }
	public short VariantTracking { get; set; }

	public ICollection<Variant> Variants { get; } = new List<Variant>();
}
