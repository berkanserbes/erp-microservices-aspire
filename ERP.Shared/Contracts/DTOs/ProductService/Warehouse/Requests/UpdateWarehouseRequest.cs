namespace ERP.Shared.Contracts.DTOs.ProductService.Warehouse.Requests;

public class UpdateWarehouseRequest
{
	public int Number { get; set; }
	public string Name { get; set; } = string.Empty;
}
