using ERP.Shared.Domain.Entities.Abstract;

namespace ERP.Shared.Domain.Entities;

public class Warehouse : BaseEntity
{
	public int Number { get; protected set; }
	public string Name { get; protected set; } = string.Empty;

	public ICollection<Location> Locations { get; } = new List<Location>();
	//public ICollection<Product> Products { get; } = new List<Product>();
}