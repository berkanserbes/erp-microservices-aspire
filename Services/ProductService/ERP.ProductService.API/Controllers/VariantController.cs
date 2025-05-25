using ERP.ProductService.Application.Services;
using ERP.ProductService.Infrastructure.Services;
using ERP.Shared.Contracts.DTOs.ProductService.Product.Requests;
using ERP.Shared.Contracts.DTOs.ProductService.Product.Responses;
using ERP.Shared.Contracts.DTOs.ProductService.Variant.Requests;
using ERP.Shared.Contracts.DTOs.ProductService.Variant.Responses;
using ERP.Shared.Contracts.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ERP.ProductService.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VariantController(ILogger<VariantController> logger, IVariantService variantService) : ControllerBase
{
	private readonly ILogger<VariantController> _logger = logger;
	private readonly IVariantService _variantService = variantService;

	[HttpGet]
	public async Task<IActionResult> GetAllAsync()
	{
		DataResult<IEnumerable<GetVariantResponse>> result = null!;
		try
		{
			result = await _variantService.GetAllAsync();

			if (!result.IsSuccess)
			{
				return BadRequest(result);
			}

		}
		catch (Exception ex)
		{
			result = new DataResult<IEnumerable<GetVariantResponse>>
			{
				IsSuccess = false,
				Message = $"Error: {ex.Message}",
				Data = null
			};

			_logger.LogError($"Error (GetAllAsync - ProductService.API.Controllers.VariantController): {ex.Message}");
			return BadRequest(result);
		}

		return Ok(result);
	}

	[HttpGet("{code}")]
	public async Task<IActionResult> GetByCodeAsync([FromRoute] string code)
	{
		DataResult<GetVariantResponse> result = null!;
		try
		{
			var request = new GetVariantRequest { Code = code };
			result = await _variantService.GetByCodeAsync(request);
			if (!result.IsSuccess)
			{
				return BadRequest(result);
			}
		}
		catch (Exception ex)
		{
			result = new DataResult<GetVariantResponse>
			{
				IsSuccess = false,
				Message = $"Error: {ex.Message}",
				Data = null
			};
			_logger.LogError($"Error (GetByCodeAsync - ProductService.API.Controllers.VariantController): {ex.Message}");
			return BadRequest(result);
		}
		return Ok(result);
	}

	[HttpPost]
	public async Task<IActionResult> AddAsync([FromBody] CreateVariantRequest createVariantRequest)
	{
		DataResult<CreateVariantResponse> result = null!;
		try
		{
			result = await _variantService.AddAsync(createVariantRequest);
			if (!result.IsSuccess)
			{
				return BadRequest(result);
			}
		}
		catch (Exception ex)
		{
			result = new DataResult<CreateVariantResponse>
			{
				IsSuccess = false,
				Message = $"Error: {ex.Message}",
				Data = null
			};
			_logger.LogError($"Error (AddAsync - ProductService.API.Controllers.VariantController): {ex.Message}");
			return BadRequest(result);
		}
		return Ok(result);
	}

	[HttpDelete]
	public async Task<IActionResult> DeleteAsync([FromBody] DeleteVariantRequest deleteVariantRequest)
	{
		DataResult<DeleteVariantResponse> result = null!;
		try
		{
			result = await _variantService.DeleteAsync(deleteVariantRequest);
			if (!result.IsSuccess)
			{
				return BadRequest(result);
			}
		}
		catch (Exception ex)
		{
			result = new DataResult<DeleteVariantResponse>
			{
				IsSuccess = false,
				Message = $"Error: {ex.Message}",
				Data = null
			};

			_logger.LogError($"Error (DeleteAsync - ProductService.API.Controllers.VariantController): {ex.Message}");
			return BadRequest(result);
		}
		return Ok(result);
	}

	[HttpPut]
	public async Task<IActionResult> UpdateAsync([FromBody] UpdateVariantRequest updateVariantRequest)
	{
		DataResult<UpdateVariantResponse> result = null!;
		try
		{
			result = await _variantService.UpdateAsync(updateVariantRequest);
			if (!result.IsSuccess)
			{
				return BadRequest(result);
			}
		}
		catch (Exception ex)
		{
			result = new DataResult<UpdateVariantResponse>
			{
				IsSuccess = false,
				Message = $"Error: {ex.Message}",
				Data = null
			};
			_logger.LogError($"Error (UpdateAsync - ProductService.API.Controllers.VariantController): {ex.Message}");
			return BadRequest(result);
		}
		return Ok(result);
	}
}
