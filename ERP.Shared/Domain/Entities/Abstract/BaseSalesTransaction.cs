﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Shared.Domain.Entities.Abstract;

public abstract class BaseSalesTransaction : BaseTransaction
{
	public string CustomerCode { get; protected set; } = string.Empty;
}
