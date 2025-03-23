using ERP.Shared.Domain.Entities.Abstract;

namespace ERP.Shared.Domain.Entities;

public class ShipAddress : BaseEntity
{
	public string Code { get; protected set; } = string.Empty;
	public string Name { get; protected set; } = string.Empty;
	public string Address { get; protected set; } = string.Empty;
	public string City { get; protected set; } = string.Empty;
	public string Country { get; protected set; } = string.Empty;	
}
