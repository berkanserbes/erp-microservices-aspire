using ERP.Shared.Domain.Entities.Abstract;

namespace ERP.Shared.Domain.Entities;

public class Location : BaseEntity
{
	public string Code { get;  } = string.Empty;
	public string Name { get;  } = string.Empty;

	public int WarehouseNumber { get; }
	public Warehouse Warehouse { get; } = null!;
}