﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Shared.Contracts.DTOs.ProductService.Warehouse.Requests;

public class CreateWarehouseRequest
{
	public int Number { get; set; }
	public string Name { get; set; } = string.Empty;
}
