using ERP.Shared.Domain.Entities.Abstract;

namespace ERP.Shared.Domain.Entities;

public class Warehouse : BaseEntity
{
	public int Number { get;  set; }
	public string Name { get; set; } = string.Empty;

	public ICollection<Location> Locations { get; } = new List<Location>();
	//public ICollection<Product> Products { get; } = new List<Product>();
}