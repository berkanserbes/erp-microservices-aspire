using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Shared.Domain.Entities.Abstract;

public abstract class BasePurchaseTransaction : BaseTransaction
{
	public string SupplierCode { get; protected set; } = string.Empty;
}
