using ERP.ProductService.Application.Services;
using ERP.Shared.Contracts.DTOs.ProductService.Warehouse.Requests;
using ERP.Shared.Contracts.DTOs.ProductService.Warehouse.Responses;
using ERP.Shared.Contracts.Results;
using Microsoft.AspNetCore.Mvc;

namespace ERP.ProductService.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WarehouseController(ILogger<WarehouseController> logger, IWarehouseService warehouseService) : ControllerBase
{
	private readonly ILogger<WarehouseController> _logger = logger;
	private readonly IWarehouseService _warehouseService = warehouseService;

	[HttpGet]
	public async Task<IActionResult> GetAllAsync()
	{
		DataResult<IEnumerable<GetWarehouseResponse>> result = null!;
		try
		{
			result = await _warehouseService.GetAllAsync();

			if (!result.IsSuccess)
			{
				return BadRequest(result);
			}
			
		}
		catch (Exception ex)
		{
			result = new DataResult<IEnumerable<GetWarehouseResponse>>
			{
				IsSuccess = false,
				Message = $"Error: {ex.Message}",
				Data = null
			};

			_logger.LogError($"Error (GetAllAsync - ProductService.API.Controllers.WarehouseController): {ex.Message}");
			return BadRequest(result);
		}

		return Ok(result);
	}

	[HttpGet("{number}")]
	public async Task<IActionResult> GetByNumberAsync([FromRoute] int number)
	{
		DataResult<GetWarehouseResponse> result = null!;
		try
		{
			var request = new GetWarehouseRequest { Number = number };
			result = await _warehouseService.GetByNumberAsync(request);
			if (!result.IsSuccess)
			{
				return BadRequest(result);
			}
		}
		catch (Exception ex)
		{
			result = new DataResult<GetWarehouseResponse>
			{
				IsSuccess = false,
				Message = $"Error: {ex.Message}",
				Data = null
			};
			_logger.LogError($"Error (GetByNumberAsync - ProductService.API.Controllers.WarehouseController): {ex.Message}");
			return BadRequest(result);
		}
		return Ok(result);
	}

	[HttpPost]
	public async Task<IActionResult> AddAsync([FromBody] CreateWarehouseRequest createWarehouseRequest)
	{
		DataResult<CreateWarehouseResponse> result = null!;
		try
		{
			result = await _warehouseService.AddAsync(createWarehouseRequest);
			if (!result.IsSuccess)
			{
				return BadRequest(result);
			}
		}
		catch (Exception ex)
		{
			result = new DataResult<CreateWarehouseResponse>
			{
				IsSuccess = false,
				Message = $"Error: {ex.Message}",
				Data = null
			};
			_logger.LogError($"Error (AddAsync - ProductService.API.Controllers.WarehouseController): {ex.Message}");
			return BadRequest(result);
		}
		return Ok(result);
	}

	[HttpDelete]
	public async Task<IActionResult> DeleteAsync([FromBody] DeleteWarehouseRequest deleteWarehouseRequest)
	{
		DataResult<DeleteWarehouseResponse> result = null!;
		try
		{
			result = await _warehouseService.DeleteAsync(deleteWarehouseRequest);
			if (!result.IsSuccess)
			{
				return BadRequest(result);
			}
		}
		catch (Exception ex)
		{
			result = new DataResult<DeleteWarehouseResponse>
			{
				IsSuccess = false,
				Message = $"Error: {ex.Message}",
				Data = null
			};

			_logger.LogError($"Error (DeleteAsync - ProductService.API.Controllers.WarehouseController): {ex.Message}");
			return BadRequest(result);
		}
		return Ok(result);
	}

	[HttpPut]
	public async Task<IActionResult> UpdateAsync([FromBody] UpdateWarehouseRequest updateWarehouseRequest)
	{
		DataResult<UpdateWarehouseResponse> result = null!;
		try
		{
			result = await _warehouseService.UpdateAsync(updateWarehouseRequest);
			if (!result.IsSuccess)
			{
				return BadRequest(result);
			}
		}
		catch (Exception ex)
		{
			result = new DataResult<UpdateWarehouseResponse>
			{
				IsSuccess = false,
				Message = $"Error: {ex.Message}",
				Data = null
			};
			_logger.LogError($"Error (UpdateAsync - ProductService.API.Controllers.WarehouseController): {ex.Message}");
			return BadRequest(result);
		}
		return Ok(result);
	}

}
