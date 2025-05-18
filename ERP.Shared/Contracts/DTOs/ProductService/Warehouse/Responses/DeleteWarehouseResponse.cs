using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Shared.Contracts.DTOs.ProductService.Warehouse.Responses;

public class DeleteWarehouseResponse
{
	public Guid Id { get; set; }
	public int Number { get; set; }
	public string Name { get; set; } = string.Empty;
}
