using ERP.SalesService.Application.Services;
using ERP.Shared.Contracts.DTOs.SalesService.Customer.Requests;
using ERP.Shared.Contracts.DTOs.SalesService.Customer.Responses;
using ERP.Shared.Contracts.Results;
using Microsoft.AspNetCore.Mvc;

namespace ERP.SalesService.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomerController(ILogger<CustomerController> logger, ICustomerService customerService) : ControllerBase
{
	private readonly ILogger<CustomerController> _logger = logger;
	private readonly ICustomerService _customerService = customerService;

	[HttpGet]
	public async Task<IActionResult> GetAllAsync()
	{
		DataResult<IEnumerable<GetCustomerResponse>> result = null!;
		try
		{
			result = await _customerService.GetAllAsync();

			if (!result.IsSuccess)
			{
				return BadRequest(result);
			}
		}
		catch (Exception ex)
		{
			result = new DataResult<IEnumerable<GetCustomerResponse>>
			{
				IsSuccess = false,
				Message = $"Error: {ex.Message}",
				Data = null
			};
			_logger.LogError($"Error (GetAllAsync - SalesService.API.Controllers.CustomerController): {ex.Message}");
			return BadRequest(result);
		}
		return Ok(result);
	}

	[HttpGet("{code}")]
	public async Task<IActionResult> GetByCodeAsync([FromRoute] string code)
	{
		DataResult<GetCustomerResponse> result = null!;

		try
		{
			var request = new GetCustomerRequest { Code = code };
			result = await _customerService.GetByCodeAsync(request);
			if (!result.IsSuccess)
			{
				return BadRequest(result);
			}
		}
		catch (Exception ex)
		{
			result = new DataResult<GetCustomerResponse>
			{
				IsSuccess = false,
				Message = $"Error: {ex.Message}",
				Data = null
			};
			_logger.LogError($"Error (GetByCodeAsync - SalesService.API.Controllers.CustomerController): {ex.Message}");
			return BadRequest(result);
		}
		return Ok(result);
	}

	[HttpPost]
	public async Task<IActionResult> AddAsync([FromBody] CreateCustomerRequest request)
	{
		DataResult<CreateCustomerResponse> result = null!;
		try
		{
			result = await _customerService.AddAsync(request);
			if (!result.IsSuccess)
			{
				return BadRequest(result);
			}
		}
		catch (Exception ex)
		{
			result = new DataResult<CreateCustomerResponse>
			{
				IsSuccess = false,
				Message = $"Error: {ex.Message}",
				Data = null
			};
			_logger.LogError($"Error (AddAsync - SalesService.API.Controllers.SalesController.CustomerController): {ex.Message}");
			return BadRequest(result);
		}

		return Ok(result);
	}

	[HttpDelete]
	public async Task<IActionResult> DeleteAsync([FromBody] DeleteCustomerRequest request)
	{
		DataResult<DeleteCustomerResponse> result = null!;
		try
		{
			result = await _customerService.DeleteAsync(request);
			if (!result.IsSuccess)
			{
				return BadRequest(result);
			}
		}
		catch (Exception ex)
		{
			result = new DataResult<DeleteCustomerResponse>
			{
				IsSuccess = false,
				Message = $"Error: {ex.Message}",
				Data = null
			};

			_logger.LogError($"Error (DeleteAsync - SalesService.API.Controllers.CustomerController): {ex.Message}");
			return BadRequest(result);
		}

		return Ok(result);
	}

	[HttpPut]
	public async Task<IActionResult> UpdateAsync([FromBody] UpdateCustomerRequest request)
	{
		DataResult<UpdateCustomerResponse> result = null!;
		try
		{
			result = await _customerService.UpdateAsync(request);
			if (!result.IsSuccess)
			{
				return BadRequest(result);
			}
		}
		catch (Exception ex)
		{
			result = new DataResult<UpdateCustomerResponse>
			{
				IsSuccess = false,
				Message = $"Error: {ex.Message}",
				Data = null
			};

			_logger.LogError($"Error (UpdateAsync - SalesService.API.Controllers.CustomerController): {ex.Message}");
			return BadRequest(result);
		}

		return Ok(result);
	}
}
