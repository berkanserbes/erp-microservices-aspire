using ERP.Shared.Domain.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Shared.Domain.Entities;

public class Variant : BaseEntity
{
	public string Code { get; set; } = string.Empty;
	public string Name { get; set; } = string.Empty;
	
	public Guid ProductId { get; set; }
	public Product Product { get; set; } = null!;
}
