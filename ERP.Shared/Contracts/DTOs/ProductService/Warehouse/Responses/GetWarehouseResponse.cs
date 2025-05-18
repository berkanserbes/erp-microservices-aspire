namespace ERP.Shared.Contracts.DTOs.ProductService.Warehouse.Responses;

public class GetWarehouseResponse
{
	public Guid Id { get; set; }
	public int Number { get; set; }
	public string Name { get; set; } = string.Empty;
}
