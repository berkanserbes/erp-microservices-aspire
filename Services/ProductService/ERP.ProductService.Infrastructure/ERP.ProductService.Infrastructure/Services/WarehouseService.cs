using ERP.ProductService.Application.Services;
using ERP.ProductService.Infrastructure.Contexts;
using ERP.Shared.Contracts.DTOs.ProductService.Warehouse.Requests;
using ERP.Shared.Contracts.DTOs.ProductService.Warehouse.Responses;
using ERP.Shared.Contracts.Results;
using ERP.Shared.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ERP.ProductService.Infrastructure.Services;

public class WarehouseService(ILogger<WarehouseService> logger,
							  ProductDbContext context) : IWarehouseService
{
	private readonly ILogger<WarehouseService> _logger = logger;
	private readonly ProductDbContext _context = context;

	public async Task<DataResult<CreateWarehouseResponse>> AddAsync(CreateWarehouseRequest request)
	{
		DataResult<CreateWarehouseResponse> result = null!;
		try
		{
			var exists = await _context.Warehouses.AnyAsync(x => x.Number == request.Number);

			if (exists)
			{
				result = new DataResult<CreateWarehouseResponse>
				{
					IsSuccess = false,
					Message = $"Warehouse with number ({request.Number}) already exists.",
					Data = null
				};

				_logger.LogWarning($"Warning (CreateWarehouseRequest - ProductService.Infrastructure): {result.Message}");
				return result;
			}

			var warehouse = new Warehouse
			{
				Id = Guid.NewGuid(),
				Name = request.Name,
				Number = request.Number
			};

			_context.Warehouses.Add(warehouse);
			await _context.SaveChangesAsync();

			var response = new CreateWarehouseResponse
			{
				Id = warehouse.Id,
				Number = warehouse.Number,
				Name = warehouse.Name
			};

			result = new DataResult<CreateWarehouseResponse>
			{
				IsSuccess = true,
				Message = "Warehouse created successfully.",
				Data = response
			};

			_logger.LogInformation($"Success (CreateWarehouseRequest - ProductService.Infrastructure): {result.Message} - {result.Data.Id}");
		}
		catch (Exception ex)
		{
			_logger.LogError($"Error (CreateWarehouseRequest - ProductService.Infrastructure): {ex.Message}");
			result = new DataResult<CreateWarehouseResponse>
			{
				IsSuccess = false,
				Message = $"Error: {ex.Message}",
				Data = null
			};
		}
		
		return result;
	}

	public async Task<DataResult<DeleteWarehouseResponse>> DeleteAsync(DeleteWarehouseRequest request)
	{
		DataResult<DeleteWarehouseResponse> result = null!;
		try
		{
			var existingWarehouse = await _context.Warehouses.FirstOrDefaultAsync(x => x.Number == request.Number);

			if(existingWarehouse is null)
			{
				result = new DataResult<DeleteWarehouseResponse>
				{
					IsSuccess = false,
					Message = $"Warehouse with number ({request.Number}) does not exist.",
					Data = null
				};

				_logger.LogWarning($"Warning (DeleteWarehouseRequest - ProductService.Infrastructure): {result.Message}");
				return result;
			}

			_context.Warehouses.Remove(existingWarehouse);
			await _context.SaveChangesAsync();

			var deleteWarehouseResponse = new DeleteWarehouseResponse
			{
				Id = existingWarehouse.Id,
				Number = existingWarehouse.Number,
				Name = existingWarehouse.Name
			};

			result = new DataResult<DeleteWarehouseResponse>
			{
				IsSuccess = true,
				Message = $"Warehouse with number ({existingWarehouse.Number}) deleted successfully.",
				Data = deleteWarehouseResponse
			};

			_logger.LogInformation($"Success (DeleteWarehouseRequest - ProductService.Infrastructure): {result.Message} - {result.Data.Id}");
		}
		catch (Exception ex)
		{
			_logger.LogError($"Error (DeleteWarehouseRequest - ProductService.Infrastructure): {ex.Message}");
			result = new DataResult<DeleteWarehouseResponse>
			{
				IsSuccess = false,
				Message = $"Error: {ex.Message}",
				Data = null
			};

		}
		
		return result;
	}

	public async Task<DataResult<IEnumerable<GetWarehouseResponse>>> GetAllAsync()
	{
		DataResult<IEnumerable<GetWarehouseResponse>> result = null!;
		try
		{
			var warehouses = await _context.Warehouses.AsNoTracking()
				.Select(x => new GetWarehouseResponse
				{
					Id = x.Id,
					Number = x.Number,
					Name = x.Name
				}).ToListAsync();

			result = new DataResult<IEnumerable<GetWarehouseResponse>>
			{
				IsSuccess = true,
				Message = "Warehouses retrieved successfully.",
				Data = warehouses
			};

			_logger.LogInformation($"Success (GetWarehouseResponse - ProductService.Infrastructure): {result.Message} - {result.Data.Count()}");
		}
		catch (Exception ex)
		{

			_logger.LogError($"Error (GetWarehouseResponse - ProductService.Infrastructure): {ex.Message}");
			result = new DataResult<IEnumerable<GetWarehouseResponse>>
			{
				IsSuccess = false,
				Message = $"Error: {ex.Message}",
				Data = null
			};
		}

		return result;
	}

	public async Task<DataResult<GetWarehouseResponse>> GetByNumberAsync(GetWarehouseRequest request)
	{
		DataResult<GetWarehouseResponse> result = null!;
		try
		{
			var warehouse = await _context.Warehouses.AsNoTracking()
				.Where(x => x.Number == request.Number)
				.Select(x => new GetWarehouseResponse
				{
					Id = x.Id,
					Number = x.Number,
					Name = x.Name
				}).FirstOrDefaultAsync();

			if(warehouse is null)
			{
				result = new DataResult<GetWarehouseResponse>
				{
					IsSuccess = false,
					Message = $"Warehouse with number ({request.Number}) does not exist.",
					Data = null
				};

				_logger.LogWarning($"Warning (GetWarehouseResponse - ProductService.Infrastructure): {result.Message}");
				return result;
			}

			result = new DataResult<GetWarehouseResponse>
			{
				IsSuccess = true,
				Message = "Warehouses retrieved successfully.",
				Data = warehouse
			};

			_logger.LogInformation($"Success (GetWarehouseResponse - ProductService.Infrastructure): {result.Message} - {result.Data.Id}");
		}
		catch (Exception ex)
		{

			_logger.LogError($"Error (GetWarehouseResponse - ProductService.Infrastructure): {ex.Message}");
			result = new DataResult<GetWarehouseResponse>
			{
				IsSuccess = false,
				Message = $"Error: {ex.Message}",
				Data = null
			};
		}

		return result;
	}

	public async Task<DataResult<UpdateWarehouseResponse>> UpdateAsync(UpdateWarehouseRequest updateWarehouseRequest)
	{
		DataResult<UpdateWarehouseResponse> result = null!;
		try
		{
			var warehouse = await _context.Warehouses.FirstOrDefaultAsync(x => x.Number == updateWarehouseRequest.Number);

			if (warehouse is null)
			{
				result = new DataResult<UpdateWarehouseResponse>
				{
					IsSuccess = false,
					Message = $"Warehouse with number ({updateWarehouseRequest.Number}) does not exist.",
					Data = null
				};
				_logger.LogWarning($"Warning (UpdateWarehouseRequest - ProductService.Infrastructure): {result.Message}");
				return result;
			}

			warehouse.Name = updateWarehouseRequest.Name;
			_context.Warehouses.Update(warehouse);
			await _context.SaveChangesAsync();

			UpdateWarehouseResponse updateWarehouseResponse = new UpdateWarehouseResponse
			{
				Id = warehouse.Id,
				Number = warehouse.Number,
				Name = warehouse.Name
			};

			result = new DataResult<UpdateWarehouseResponse>
			{
				IsSuccess = true,
				Message = $"Warehouse with number ({warehouse.Number}) updated successfully.",
				Data = updateWarehouseResponse
			};

			_logger.LogInformation($"Success (UpdateWarehouseRequest - ProductService.Infrastructure): {result.Message}");
		}
		catch (Exception ex)
		{
			_logger.LogError($"Error (UpdateWarehouseRequest - ProductService.Infrastructure): {ex.Message}");
			result = new DataResult<UpdateWarehouseResponse>
			{
				IsSuccess = false,
				Message = $"Error: {ex.Message}",
				Data = null
			};
		}

		return result;
	}
}
