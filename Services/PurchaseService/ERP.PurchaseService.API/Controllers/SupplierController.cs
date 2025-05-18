using ERP.PurchaseService.Application.Services;
using ERP.Shared.Contracts.DTOs.PurchaseService.Supplier.Requests;
using ERP.Shared.Contracts.DTOs.PurchaseService.Supplier.Responses;
using ERP.Shared.Contracts.DTOs.SalesService.Customer.Requests;
using ERP.Shared.Contracts.DTOs.SalesService.Customer.Responses;
using ERP.Shared.Contracts.Results;
using Microsoft.AspNetCore.Mvc;

namespace ERP.PurchaseService.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SupplierController(ILogger<SupplierController> logger, ISupplierService supplierService) : ControllerBase
{
	private readonly ILogger<SupplierController> _logger = logger;
	private readonly ISupplierService _supplierService = supplierService;

	[HttpGet]
	public async Task<IActionResult> GetAllAsync()
	{
		DataResult<IEnumerable<GetSupplierResponse>> result = null!;
		try
		{
			result = await _supplierService.GetAllAsync();

			if (!result.IsSuccess)
			{
				return BadRequest(result);
			}
		}
		catch (Exception ex)
		{
			result = new DataResult<IEnumerable<GetSupplierResponse>>
			{
				IsSuccess = false,
				Message = $"Error: {ex.Message}",
				Data = null
			};
			_logger.LogError($"Error (GetAllAsync - PurchaseService.API.Controllers.SupplierController): {ex.Message}");
			return BadRequest(result);
		}
		return Ok(result);
	}

	[HttpGet("{code}")]
	public async Task<IActionResult> GetByCodeAsync([FromRoute] string code)
	{
		DataResult<GetSupplierResponse> result = null!;

		try
		{
			var request = new GetSupplierRequest { Code = code };
			result = await _supplierService.GetByCodeAsync(request);
			if (!result.IsSuccess)
			{
				return BadRequest(result);
			}
		}
		catch (Exception ex)
		{
			result = new DataResult<GetSupplierResponse>
			{
				IsSuccess = false,
				Message = $"Error: {ex.Message}",
				Data = null
			};
			_logger.LogError($"Error (GetByCodeAsync - PurchaseService.API.Controllers.SupplierController): {ex.Message}");
			return BadRequest(result);
		}
		return Ok(result);
	}

	[HttpPost]
	public async Task<IActionResult> AddAsync([FromBody] CreateSupplierRequest request)
	{
		DataResult<CreateSupplierResponse> result = null!;
		try
		{
			result = await _supplierService.AddAsync(request);
			if (!result.IsSuccess)
			{
				return BadRequest(result);
			}
		}
		catch (Exception ex)
		{
			result = new DataResult<CreateSupplierResponse>
			{
				IsSuccess = false,
				Message = $"Error: {ex.Message}",
				Data = null
			};
			_logger.LogError($"Error (AddAsync - PurchaseService.API.Controllers.SupplierController): {ex.Message}");
			return BadRequest(result);
		}

		return Ok(result);
	}

	[HttpDelete]
	public async Task<IActionResult> DeleteAsync([FromBody] DeleteSupplierRequest request)
	{
		DataResult<DeleteSupplierResponse> result = null!;
		try
		{
			result = await _supplierService.DeleteAsync(request);
			if (!result.IsSuccess)
			{
				return BadRequest(result);
			}
		}
		catch (Exception ex)
		{
			result = new DataResult<DeleteSupplierResponse>
			{
				IsSuccess = false,
				Message = $"Error: {ex.Message}",
				Data = null
			};

			_logger.LogError($"Error (DeleteAsync - PurchaseService.API.Controllers.SupplierController): {ex.Message}");
			return BadRequest(result);
		}

		return Ok(result);
	}

	[HttpPut]
	public async Task<IActionResult> UpdateAsync([FromBody] UpdateSupplierRequest request)
	{
		DataResult<UpdateSupplierResponse> result = null!;
		try
		{
			result = await _supplierService.UpdateAsync(request);
			if (!result.IsSuccess)
			{
				return BadRequest(result);
			}
		}
		catch (Exception ex)
		{
			result = new DataResult<UpdateSupplierResponse>
			{
				IsSuccess = false,
				Message = $"Error: {ex.Message}",
				Data = null
			};

			_logger.LogError($"Error (UpdateAsync - PurchaseService.API.Controllers.SupplierController): {ex.Message}");
			return BadRequest(result);
		}

		return Ok(result);
	}
}
