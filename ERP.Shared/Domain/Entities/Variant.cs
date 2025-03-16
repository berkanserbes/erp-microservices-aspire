using ERP.Shared.Domain.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Shared.Domain.Entities;

public class Variant : BaseEntity
{
	public string Code { get; protected set; } = string.Empty;
	public string Name { get; protected set; } = string.Empty;
	
	public Guid ProductId { get; protected set; }
	public Product Product { get; protected set; } = null!;
}
