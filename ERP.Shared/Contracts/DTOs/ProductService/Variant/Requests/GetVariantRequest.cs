using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Shared.Contracts.DTOs.ProductService.Variant.Requests;

public class GetVariantRequest
{
	public string Code { get; set; } = string.Empty;
	public string Name { get; set; } = string.Empty;

	public Guid ProductId { get; set; }
}
