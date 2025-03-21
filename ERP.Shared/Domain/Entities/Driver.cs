using ERP.Shared.Domain.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Shared.Domain.Entities;

public class Driver : BaseEntity
{
	public string Name { get; protected set; } = string.Empty;
	public string SurName { get; protected set; } = string.Empty;
	public string IdentityNumber { get; protected set; } = string.Empty;
}
