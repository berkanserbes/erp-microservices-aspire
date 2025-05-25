using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Shared.Contracts.DTOs.ProductService.Product.Requests;

public class DeleteProductRequest
{
	public string Code { get; set; } = string.Empty;
}
