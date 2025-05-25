using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Shared.Contracts.DTOs.ProductService.Product.Requests;

public class UpdateProductRequest
{
	public string Code { get; set; } = string.Empty;
	public string Name { get; set; } = string.Empty;
	public short LotTracking { get; set; }
	public short LocationTracking { get; set; }
	public short VariantTracking { get; set; }
}
