using ERP.ProductService.Application.Services;
using ERP.ProductService.Infrastructure.Contexts;
using ERP.Shared.Contracts.DTOs.ProductService.Variant.Requests;
using ERP.Shared.Contracts.DTOs.ProductService.Variant.Responses;
using ERP.Shared.Contracts.Results;
using ERP.Shared.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ERP.ProductService.Infrastructure.Services;

public class VariantService(ILogger<VariantService> logger, ProductDbContext context) : IVariantService
{
	private readonly ILogger<VariantService> _logger = logger;
	private readonly ProductDbContext _context = context;

	public async Task<DataResult<IEnumerable<GetVariantResponse>>> GetAllAsync()
	{
		DataResult<IEnumerable<GetVariantResponse>> result = null!;
		try
		{
			var variants = await _context.Variants.AsNoTracking()
				.Select(x => new GetVariantResponse
				{
					Id = x.Id,
					Code = x.Code,
					Name = x.Name,
					ProductId = x.ProductId,

				}).ToListAsync();

			result = new DataResult<IEnumerable<GetVariantResponse>>
			{
				IsSuccess = true,
				Message = "Variants retrieved successfully.",
				Data = variants
			};

			_logger.LogInformation($"Success (GetVariantResponse - ProductService.Infrastructure): {result.Message} - {result.Data.Count()}");
		}
		catch (Exception ex)
		{
			_logger.LogError($"Error (GetVariantResponse - ProductService.Infrastructure): {ex.Message}");
			result = new DataResult<IEnumerable<GetVariantResponse>>
			{
				IsSuccess = false,
				Message = $"Error: {ex.Message}",
				Data = null
			};
		}
		return result;
	}

	public async Task<DataResult<GetVariantResponse>> GetByCodeAsync(GetVariantRequest request)
	{
		DataResult<GetVariantResponse> result = null!;
		try
		{
			var variant = await _context.Variants.AsNoTracking()
								.Where(x => x.Code == request.Code)
								.Select(x => new GetVariantResponse
								{
									Id = x.Id,
									Code = x.Code,
									Name = x.Name,
									ProductId = x.ProductId
								}).FirstOrDefaultAsync();

			if (variant is null)
			{
				result = new DataResult<GetVariantResponse>
				{
					IsSuccess = false,
					Message = $"Variant with code ({request.Code}) does not exist.",
					Data = null
				};

				_logger.LogWarning($"(GetVariantResponse - ProductService.Infrastructure): {result.Message}");

				return result;
			}

			result = new DataResult<GetVariantResponse>
			{
				IsSuccess = true,
				Message = "Variant retrieved successfully.",
				Data = variant,
			};

			_logger.LogInformation($"Success (GetVariantResponse - ProductService.Infrastructure): {result.Message} - {result.Data.Id}");
		}
		catch (Exception ex)
		{
			_logger.LogError($"(GetVariantResponse - ProductService.Infrastructure): {ex.Message}");
			result = new DataResult<GetVariantResponse>
			{
				IsSuccess = false,
				Message = $"Error: {ex.Message}",
				Data = null
			};
		}
		return result;
	}

	public async Task<DataResult<CreateVariantResponse>> AddAsync(CreateVariantRequest request)
	{
		DataResult<CreateVariantResponse> result = null!;
		try
		{
			var isVariantExist = await _context.Variants.AnyAsync(x => x.Code == request.Code);
			if (isVariantExist)
			{
				result = new DataResult<CreateVariantResponse>
				{
					IsSuccess = false,
					Message = $"Variant with code ({request.Code}) already exists.",
					Data = null
				};
				_logger.LogWarning($"(CreateVariantRequest - ProductService.Infrastructure): {result.Message}");
				return result;
			}

			var existingProduct = await _context.Products.FirstOrDefaultAsync(x => x.Id == request.ProductId);
			if (existingProduct is null)
			{
				result = new DataResult<CreateVariantResponse>
				{
					IsSuccess = false,
					Message = $"Product with ID ({request.ProductId}) does not exist.",
					Data = null
				};
				_logger.LogWarning($"(CreateVariantRequest - ProductService.Infrastructure): {result.Message}");
				return result;
			}

			if (existingProduct.VariantTracking == 0)
			{
				result = new DataResult<CreateVariantResponse>
				{
					IsSuccess = false,
					Message = $"Product with ID ({request.ProductId}) is not variant-tracked.",
					Data = null
				};
				_logger.LogWarning($"(CreateVariantRequest - ProductService.Infrastructure): {result.Message}");
				return result;
			}

			Variant variant = new()
			{
				Id = Guid.NewGuid(),
				Code = request.Code,
				Name = request.Name,
				ProductId = request.ProductId,
				Product = existingProduct
			};

			_context.Variants.Add(variant);
			await _context.SaveChangesAsync();

			var response = new CreateVariantResponse
			{
				Id = variant.Id,
				Code = variant.Code,
				Name = variant.Name,
				ProductId = variant.ProductId
			};

			result = new DataResult<CreateVariantResponse>
			{
				Data = response,
				IsSuccess = true,
				Message = "Variant created successfully."
			};

			_logger.LogInformation($"Success (CreateVariantRequest - ProductService.Infrastructure): {result.Message} - {result.Data.Id}");

		}
		catch (Exception ex)
		{
			_logger.LogError($"(CreateVariantRequest - ProductService.Infrastructure): {ex.Message}");
			result = new DataResult<CreateVariantResponse>
			{
				IsSuccess = false,
				Message = $"Error: {ex.Message}",
				Data = null
			};
		}

		return result;
	}

	public async Task<DataResult<DeleteVariantResponse>> DeleteAsync(DeleteVariantRequest request)
	{
		DataResult<DeleteVariantResponse> result = null!;
		try
		{
			var existingVariant = await _context.Variants.FirstOrDefaultAsync(x => x.Code == request.Code);

			if (existingVariant is null)
			{
				result = new DataResult<DeleteVariantResponse>
				{
					IsSuccess = false,
					Message = $"Variant with code ({request.Code}) does not exist.",
					Data = null
				};

				_logger.LogWarning($"(DeleteVariantRequest - ProductService.Infrastructure): {result.Message}");
				return result;
			}

			_context.Variants.Remove(existingVariant);
			_context.Products.Remove(existingVariant.Product);
			await _context.SaveChangesAsync();

			var deleteVariantResponse = new DeleteVariantResponse
			{
				Id = existingVariant.Id,
				Code = existingVariant.Code,
				Name = existingVariant.Name,
				ProductId = existingVariant.ProductId

			};

			result = new DataResult<DeleteVariantResponse>
			{
				IsSuccess = true,
				Message = $"Variant with code ({existingVariant.Code}) deleted successfully.",
				Data = deleteVariantResponse
			};

			_logger.LogInformation($"Success (DeleteVariantRequest - ProductService.Infrastructure): {result.Message} - {result.Data.Id}");
		}
		catch (Exception ex)
		{
			_logger.LogError($"Error (DeleteVariantRequest - ProductService.Infrastructure): {ex.Message}");
			result = new DataResult<DeleteVariantResponse>
			{
				IsSuccess = false,
				Message = $"Error: {ex.Message}",
				Data = null
			};

		}

		return result;
	}

	public async Task<DataResult<UpdateVariantResponse>> UpdateAsync(UpdateVariantRequest request)
	{
		DataResult<UpdateVariantResponse> result = null!;
		try
		{
			var variant = await _context.Variants.FirstOrDefaultAsync(x => x.Code == request.Code);

			if (variant is null)
			{
				result = new DataResult<UpdateVariantResponse>
				{
					IsSuccess = false,
					Message = $"Variant with code ({request.Code}) does not exist.",
					Data = null
				};
				_logger.LogWarning($"(UpdateVariantRequest - ProductService.Infrastructure): {result.Message}");
				return result;
			}

			variant.Name = request.Name;
			_context.Variants.Update(variant);
			await _context.SaveChangesAsync();

			UpdateVariantResponse updateProductResponse = new()
			{
				Id = variant.Id,
				Code = variant.Code,
				Name = variant.Name
			};

			result = new DataResult<UpdateVariantResponse>
			{
				IsSuccess = true,
				Message = $"Variant with code ({variant.Code}) updated successfully.",
				Data = updateProductResponse
			};

			_logger.LogInformation($"Success (UpdateVariantRequest - ProductService.Infrastructure): {result.Message}");
		}
		catch (Exception ex)
		{
			_logger.LogError($"Error (UpdateVariantRequest - ProductService.Infrastructure): {ex.Message}");
			result = new DataResult<UpdateVariantResponse>
			{
				IsSuccess = false,
				Message = $"Error: {ex.Message}",
				Data = null
			};
		}

		return result;
	}
}
