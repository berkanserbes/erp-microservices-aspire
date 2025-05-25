using ERP.ProductService.Application.Services;
using ERP.ProductService.Infrastructure.Services;
using ERP.Shared.Contracts.DTOs.ProductService.Product.Requests;
using ERP.Shared.Contracts.DTOs.ProductService.Product.Responses;
using ERP.Shared.Contracts.DTOs.ProductService.Warehouse.Requests;
using ERP.Shared.Contracts.DTOs.ProductService.Warehouse.Responses;
using ERP.Shared.Contracts.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ERP.ProductService.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController(ILogger<ProductController> logger, IProductService productService) : ControllerBase
{
	private readonly ILogger<ProductController> _logger = logger;
	private readonly IProductService _productService = productService;

	[HttpGet]
	public async Task<IActionResult> GetAllAsync()
	{
		DataResult<IEnumerable<GetProductResponse>> result = null!;
		try
		{
			result = await _productService.GetAllAsync();

			if (!result.IsSuccess)
			{
				return BadRequest(result);
			}

		}
		catch (Exception ex)
		{
			result = new DataResult<IEnumerable<GetProductResponse>>
			{
				IsSuccess = false,
				Message = $"Error: {ex.Message}",
				Data = null
			};

			_logger.LogError($"Error (GetAllAsync - ProductService.API.Controllers.ProductController): {ex.Message}");
			return BadRequest(result);
		}

		return Ok(result);
	}

	[HttpGet("{code}")]
	public async Task<IActionResult> GetByCodeAsync([FromRoute] string code)
	{
		DataResult<GetProductResponse> result = null!;
		try
		{
			var request = new GetProductRequest { Code = code };
			result = await _productService.GetByCodeAsync(request);
			if (!result.IsSuccess)
			{
				return BadRequest(result);
			}
		}
		catch (Exception ex)
		{
			result = new DataResult<GetProductResponse>
			{
				IsSuccess = false,
				Message = $"Error: {ex.Message}",
				Data = null
			};
			_logger.LogError($"Error (GetByCodeAsync - ProductService.API.Controllers.ProductController): {ex.Message}");
			return BadRequest(result);
		}
		return Ok(result);
	}

	[HttpPost]
	public async Task<IActionResult> AddAsync([FromBody] CreateProductRequest createProductRequest)
	{
		DataResult<CreateProductResponse> result = null!;
		try
		{
			result = await _productService.AddAsync(createProductRequest);
			if (!result.IsSuccess)
			{
				return BadRequest(result);
			}
		}
		catch (Exception ex)
		{
			result = new DataResult<CreateProductResponse>
			{
				IsSuccess = false,
				Message = $"Error: {ex.Message}",
				Data = null
			};
			_logger.LogError($"Error (AddAsync - ProductService.API.Controllers.ProductController): {ex.Message}");
			return BadRequest(result);
		}
		return Ok(result);
	}

	[HttpDelete]
	public async Task<IActionResult> DeleteAsync([FromBody] DeleteProductRequest deleteProductRequest)
	{
		DataResult<DeleteProductResponse> result = null!;
		try
		{
			result = await _productService.DeleteAsync(deleteProductRequest);
			if (!result.IsSuccess)
			{
				return BadRequest(result);
			}
		}
		catch (Exception ex)
		{
			result = new DataResult<DeleteProductResponse>
			{
				IsSuccess = false,
				Message = $"Error: {ex.Message}",
				Data = null
			};

			_logger.LogError($"Error (DeleteAsync - ProductService.API.Controllers.ProductController): {ex.Message}");
			return BadRequest(result);
		}
		return Ok(result);
	}

	[HttpPut]
	public async Task<IActionResult> UpdateAsync([FromBody] UpdateProductRequest updateProductRequest)
	{
		DataResult<UpdateProductResponse> result = null!;
		try
		{
			result = await _productService.UpdateAsync(updateProductRequest);
			if (!result.IsSuccess)
			{
				return BadRequest(result);
			}
		}
		catch (Exception ex)
		{
			result = new DataResult<UpdateProductResponse>
			{
				IsSuccess = false,
				Message = $"Error: {ex.Message}",
				Data = null
			};
			_logger.LogError($"Error (UpdateAsync - ProductService.API.Controllers.ProductController): {ex.Message}");
			return BadRequest(result);
		}
		return Ok(result);
	}

}
