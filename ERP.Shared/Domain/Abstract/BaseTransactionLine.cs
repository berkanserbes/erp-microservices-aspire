using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Shared.Domain.Abstract;

public abstract class BaseTransactionLine : BaseEntity
{
	public string ProductCode { get; protected set; } = string.Empty;
	public string ProductName { get; protected set; } = string.Empty;
	public string SubUnitsetCode { get; protected set; } = string.Empty;
	public string SubUnitsetName { get; protected set; } = string.Empty;
	public string UnitsetCode { get; protected set; } = string.Empty;
	public string UnitsetName { get; protected set; } = string.Empty;
	public double Quantity { get; protected set; } = default;
	public double ConversionRate { get; protected set; } = default;
	public double OtherConversionRate { get; protected set; } = default;
}
