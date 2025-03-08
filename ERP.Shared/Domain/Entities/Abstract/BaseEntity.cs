﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Shared.Domain.Entities.Abstract;

public abstract class BaseEntity
{
	public Guid Id { get; protected set; }
	public DateTime CreatedAt { get; protected set; }
	public DateTime? UpdatedAt { get; protected set; }

	protected BaseEntity()
	{
		Id = Guid.NewGuid();
		CreatedAt = DateTime.UtcNow;
	}
}
