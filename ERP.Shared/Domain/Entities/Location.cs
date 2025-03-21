using ERP.Shared.Domain.Entities.Abstract;

namespace ERP.Shared.Domain.Entities;

public class Location : BaseEntity
{
	public string Code { get; protected set; } = string.Empty;
	public string Name { get; protected set; } = string.Empty;

	public int WarehouseNumber { get; protected set; }
	public Warehouse Warehouse { get; protected set; } = null!;
}