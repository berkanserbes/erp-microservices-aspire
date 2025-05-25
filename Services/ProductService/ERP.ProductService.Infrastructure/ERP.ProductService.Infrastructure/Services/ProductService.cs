using ERP.ProductService.Application.Services;
using ERP.ProductService.Infrastructure.Contexts;
using ERP.Shared.Contracts.DTOs.ProductService.Product.Requests;
using ERP.Shared.Contracts.DTOs.ProductService.Product.Responses;
using ERP.Shared.Contracts.Results;
using ERP.Shared.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ERP.ProductService.Infrastructure.Services;
public class ProductService(ILogger<ProductService> logger, ProductDbContext context) : IProductService
{
	private readonly ILogger<ProductService> _logger = logger;
	private readonly ProductDbContext _context = context;

	public async Task<DataResult<CreateProductResponse>> AddAsync(CreateProductRequest request)
	{
		DataResult<CreateProductResponse> result = null!;
		try
		{
			var exists = await _context.Products.AnyAsync(x => x.Code == request.Code);
			if (exists)
			{
				result = new DataResult<CreateProductResponse>
				{
					IsSuccess = false,
					Message = $"Product with code ({request.Code}) already exists.",
					Data = null
				};

				_logger.LogWarning($"(CreateProductRequest - ProductService.Infrastructure): {result.Message}");
				return result;
			}

			var product = new Product
			{
				Id = Guid.NewGuid(),
				Code = request.Code,
				Name = request.Name,
				LotTracking = request.LotTracking,
				LocationTracking = request.LocationTracking,
				VariantTracking = request.VariantTracking
			};

			_context.Products.Add(product);
			await _context.SaveChangesAsync();

			var response = new CreateProductResponse
			{
				Id = product.Id,
				Code = product.Code,
				Name = product.Name,
				LotTracking = product.LotTracking,
				LocationTracking = product.LocationTracking,
				VariantTracking = product.VariantTracking
			};

			result = new DataResult<CreateProductResponse>
			{
				Data = response,
				IsSuccess = true,
				Message = "Product created successfully."
			};

			_logger.LogInformation($"Success (CreateProductRequest - ProductService.Infrastructure): {result.Message} - {result.Data.Id}");
		}
		catch (Exception ex)
		{
			_logger.LogError($"(CreateProductRequest - ProductService.Infrastructure): {ex.Message}");
			result = new DataResult<CreateProductResponse>
			{
				IsSuccess = false,
				Message = $"Error: {ex.Message}",
				Data = null
			};
		}

		return result;
	}

	public async Task<DataResult<DeleteProductResponse>> DeleteAsync(DeleteProductRequest request)
	{
		DataResult<DeleteProductResponse> result = null!;
		try
		{
			var existingProduct = await _context.Products.FirstOrDefaultAsync(x => x.Code == request.Code);

			if (existingProduct is null)
			{
				result = new DataResult<DeleteProductResponse>
				{
					IsSuccess = false,
					Message = $"Product with code ({request.Code}) does not exist.",
					Data = null
				};

				_logger.LogWarning($"(DeleteProductRequest - ProductService.Infrastructure): {result.Message}");
				return result;
			}

			_context.Products.Remove(existingProduct);
			await _context.SaveChangesAsync();

			var deleteProductResponse = new DeleteProductResponse
			{
				Id = existingProduct.Id,
				Code = existingProduct.Code,
				Name = existingProduct.Name,
				VariantTracking = existingProduct.VariantTracking,
				LocationTracking = existingProduct.LocationTracking,
				LotTracking = existingProduct.LotTracking
			};

			result = new DataResult<DeleteProductResponse>
			{
				IsSuccess = true,
				Message = $"Product with code ({existingProduct.Code}) deleted successfully.",
				Data = deleteProductResponse
			};

			_logger.LogInformation($"Success (DeleteProductRequest - ProductService.Infrastructure): {result.Message} - {result.Data.Id}");
		}
		catch (Exception ex)
		{
			_logger.LogError($"Error (DeleteProductRequest - ProductService.Infrastructure): {ex.Message}");
			result = new DataResult<DeleteProductResponse>
			{
				IsSuccess = false,
				Message = $"Error: {ex.Message}",
				Data = null
			};

		}

		return result;
	}

	public async Task<DataResult<IEnumerable<GetProductResponse>>> GetAllAsync()
	{
		DataResult<IEnumerable<GetProductResponse>> result = null!;
		try
		{
			var products = await _context.Products.AsNoTracking()
				.Select(x => new GetProductResponse
				{
					Id = x.Id,
					Code = x.Code,
					Name = x.Name,
					VariantTracking = x.VariantTracking,
					LotTracking = x.LotTracking,
					LocationTracking = x.LocationTracking
				}).ToListAsync();

			result = new DataResult<IEnumerable<GetProductResponse>>
			{
				IsSuccess = true,
				Message = "Products retrieved successfully.",
				Data = products
			};

			_logger.LogInformation($"Success (GetProductResponse - ProductService.Infrastructure): {result.Message} - {result.Data.Count()}");
		}
		catch (Exception ex)
		{

			_logger.LogError($"Error (GetProductResponse - ProductService.Infrastructure): {ex.Message}");
			result = new DataResult<IEnumerable<GetProductResponse>>
			{
				IsSuccess = false,
				Message = $"Error: {ex.Message}",
				Data = null
			};
		}

		return result;
	}

	public async Task<DataResult<GetProductResponse>> GetByCodeAsync(GetProductRequest request)
	{
		DataResult<GetProductResponse> result = null!;
		try
		{
			var product = await _context.Products.AsNoTracking()
								.Where(x => x.Code == request.Code)
								.Select(x => new GetProductResponse
								{
									Id = x.Id,
									Code = x.Code,
									Name = x.Name,
									VariantTracking = x.VariantTracking,
									LocationTracking = x.LocationTracking,
									LotTracking = x.LotTracking
								}).FirstOrDefaultAsync();

			if (product is null)
			{
				result = new DataResult<GetProductResponse>
				{
					IsSuccess = false,
					Message = $"Product with code ({request.Code}) does not exist.",
					Data = null
				};

				_logger.LogWarning($"(GetProductResponse - ProductService.Infrastructure): {result.Message}");

				return result;
			}

			result = new DataResult<GetProductResponse>
			{
				IsSuccess = true,
				Message = "Product retrieved successfully.",
				Data = product,
			};

			_logger.LogInformation($"Success (GetProductResponse - ProductService.Infrastructure): {result.Message} - {result.Data.Id}");
		}
		catch (Exception ex)
		{
			_logger.LogError($"(GetProductResponse - ProductService.Infrastructure): {ex.Message}");
			result = new DataResult<GetProductResponse>
			{
				IsSuccess = false,
				Message = $"Error: {ex.Message}",
				Data = null
			};
		}
		return result;
	}

	public async Task<DataResult<UpdateProductResponse>> UpdateAsync(UpdateProductRequest request)
	{
		DataResult<UpdateProductResponse> result = null!;
		try
		{
			var product = await _context.Products.FirstOrDefaultAsync(x => x.Code == request.Code);

			if (product is null)
			{
				result = new DataResult<UpdateProductResponse>
				{
					IsSuccess = false,
					Message = $"Product with code ({request.Code}) does not exist.",
					Data = null
				};
				_logger.LogWarning($"(UpdateProductRequest - ProductService.Infrastructure): {result.Message}");
				return result;
			}

			product.Name = request.Name;
			_context.Products.Update(product);
			await _context.SaveChangesAsync();

			UpdateProductResponse updateProductResponse = new()
			{
				Id = product.Id,
				Code = product.Code,
				Name = product.Name,
				LocationTracking = product.LocationTracking,
				LotTracking = product.LotTracking,
				VariantTracking = product.VariantTracking
			};

			result = new DataResult<UpdateProductResponse>
			{
				IsSuccess = true,
				Message = $"Product with code ({product.Code}) updated successfully.",
				Data = updateProductResponse
			};

			_logger.LogInformation($"Success (UpdateProductRequest - ProductService.Infrastructure): {result.Message}");
		}
		catch (Exception ex)
		{
			_logger.LogError($"Error (UpdateProductRequest - ProductService.Infrastructure): {ex.Message}");
			result = new DataResult<UpdateProductResponse>
			{
				IsSuccess = false,
				Message = $"Error: {ex.Message}",
				Data = null
			};
		}

		return result;
	}
}
