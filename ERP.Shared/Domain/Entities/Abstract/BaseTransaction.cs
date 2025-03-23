using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Shared.Domain.Entities.Abstract;

public abstract class BaseTransaction : BaseEntity
{
	public DateTime TransactionDate { get; protected set; }
	public string Code { get; protected set; } = string.Empty;
	public string DocumentNumber { get; protected set; } = string.Empty;
	public string Description { get; protected set; } = string.Empty;
	public int WarehouseNumber { get; protected set; }
}
