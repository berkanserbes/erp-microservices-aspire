using ERP.PurchaseService.Application.Services;
using ERP.PurchaseService.Infrastructure.Contexts;
using ERP.Shared.Contracts.DTOs.PurchaseService.Supplier.Requests;
using ERP.Shared.Contracts.DTOs.PurchaseService.Supplier.Responses;
using ERP.Shared.Contracts.Results;
using ERP.Shared.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ERP.PurchaseService.Infrastructure.Services;

public class SupplierService(ILogger<SupplierService> logger,
							 PurchaseDbContext context) : ISupplierService
{
	private readonly ILogger<SupplierService> _logger = logger;
	private readonly PurchaseDbContext _context = context;

	public async Task<DataResult<CreateSupplierResponse>> AddAsync(CreateSupplierRequest request)
	{
		DataResult<CreateSupplierResponse> result = null!;
		try
		{
			var exists = _context.Suppliers.Any(x => x.Code == request.Code);

			if (exists)
			{
				result = new DataResult<CreateSupplierResponse>
				{
					IsSuccess = false,
					Message = $"Supplier with code ({request.Code}) already exists.",
					Data = null
				};
				_logger.LogWarning($"Warning (CreateSupplierRequest - PurchaseService.Infrastructure): {result.Message}");
				return result;
			}

			var supplier = new Supplier
			{
				Code = request.Code,
				Name = request.Name,
				Email = request.Email,
				Phone = request.Phone,
				Address = request.Address,
			};

			_context.Suppliers.Add(supplier);
			await _context.SaveChangesAsync();

			var response = new CreateSupplierResponse
			{
				Id = supplier.Id,
				Code = supplier.Code,
				Name = supplier.Name,
				Email = supplier.Email,
				Phone = supplier.Phone,
				Address = supplier.Address,
			};

			result = new DataResult<CreateSupplierResponse>
			{
				IsSuccess = true,
				Message = "Supplier created successfully.",
				Data = response
			};

			_logger.LogInformation($"Success (CreateSupplierRequest - PurchaseService.Infrastructure): {result.Message} - {result.Data.Id}");
		}
		catch (Exception ex)
		{
			_logger.LogError($"Error (CreateSupplierRequest - PurchaseService.Infrastructure): {ex.Message}");
			result = new DataResult<CreateSupplierResponse>
			{
				IsSuccess = false,
				Message = $"Error: {ex.Message}",
				Data = null
			};

		}

		return result;
	}

	public async Task<DataResult<DeleteSupplierResponse>> DeleteAsync(DeleteSupplierRequest request)
	{
		DataResult<DeleteSupplierResponse> result = null!;
		try
		{
			var existingSupplier = await _context.Suppliers.FirstOrDefaultAsync(x => x.Code == request.Code);

			if (existingSupplier is null)
			{
				result = new DataResult<DeleteSupplierResponse>
				{
					IsSuccess = false,
					Message = $"Supplier with code ({request.Code}) does not exist.",
					Data = null
				};
				_logger.LogWarning($"Warning (DeleteSupplierRequest - PurchaseService.Infrastructure): {result.Message}");
				return result;
			}

			_context.Suppliers.Remove(existingSupplier);
			await _context.SaveChangesAsync();

			var deleteSupplierResponse = new DeleteSupplierResponse
			{
				Id = existingSupplier.Id,
				Code = existingSupplier.Code,
				Name = existingSupplier.Name,
				Email = existingSupplier.Email,
				Phone = existingSupplier.Phone,
				Address = existingSupplier.Address,
			};

			result = new DataResult<DeleteSupplierResponse>
			{
				IsSuccess = true,
				Message = $"Supplier with code ({existingSupplier.Code}) deleted successfully.",
				Data = deleteSupplierResponse
			};

			_logger.LogInformation($"Success (DeleteSupplierRequest - PurchaseService.Infrastructure): {result.Message} - {result.Data.Id}");

		}
		catch (Exception ex)
		{
			_logger.LogError($"Error (DeleteSupplierRequest - SalesService.Infrastructure): {ex.Message}");

			result = new DataResult<DeleteSupplierResponse>
			{
				IsSuccess = false,
				Message = $"Error: {ex.Message}",
				Data = null
			};
		}

		return result;
	}

	public async Task<DataResult<IEnumerable<GetSupplierResponse>>> GetAllAsync()
	{
		DataResult<IEnumerable<GetSupplierResponse>> result = null!;
		try
		{
			var suppliers = await _context.Suppliers.AsNoTracking().Select(x => new GetSupplierResponse
			{
				Id = x.Id,
				Code = x.Code,
				Name = x.Name,
				Email = x.Email,
				Phone = x.Phone,
				Address = x.Address,
			}).ToListAsync();

			result = new DataResult<IEnumerable<GetSupplierResponse>>
			{
				IsSuccess = true,
				Message = "Suppliers retrieved successfully.",
				Data = suppliers
			};

			_logger.LogInformation($"Success (GetSupplierResponse - PurchaseService.Infrastructure): {result.Message} - {result.Data.Count()}");
		}
		catch (Exception ex)
		{
			_logger.LogError($"Error (GetSupplierResponse - PurchaseService.Infrastructure): {ex.Message}");
			result = new DataResult<IEnumerable<GetSupplierResponse>>
			{
				IsSuccess = false,
				Message = $"Error: {ex.Message}",
				Data = null
			};

		}
		return result;
	}

	public async Task<DataResult<GetSupplierResponse>> GetByCodeAsync(GetSupplierRequest request)
	{
		DataResult<GetSupplierResponse> result = null!;
		try
		{
			var supplier = await _context.Suppliers.AsNoTracking()
													.Where(x => x.Code == request.Code)
													.Select(x => new GetSupplierResponse
													{
														Id = x.Id,
														Code = x.Code,
														Name = x.Name,
														Email = x.Email,
														Phone = x.Phone,
														Address = x.Address,
													})
													.FirstOrDefaultAsync();

			if (supplier is null)
			{
				result = new DataResult<GetSupplierResponse>
				{
					IsSuccess = false,
					Message = $"Supplier with code ({request.Code}) does not exist.",
					Data = null
				};

				_logger.LogWarning($"Warning (GetSupplierRequest - PurchaseService.Infrastructure): {result.Message}");
				return result;
			}


			result = new DataResult<GetSupplierResponse>
			{
				IsSuccess = true,
				Message = "Supplier retrieved successfully.",
				Data = supplier
			};

			_logger.LogInformation($"Success (GetSupplierResponse - PurchaseService.Infrastructure): {result.Message} - {result.Data.Id}");

		}
		catch (Exception ex)
		{
			_logger.LogError($"Error (GetSupplierResponse - PurchaseService.Infrastructure): {ex.Message}");

			result = new DataResult<GetSupplierResponse>
			{
				IsSuccess = false,
				Message = $"Error: {ex.Message}",
				Data = null
			};
		}
		return result;
	}

	public async Task<DataResult<UpdateSupplierResponse>> UpdateAsync(UpdateSupplierRequest request)
	{
		DataResult<UpdateSupplierResponse> result = null!;
		try
		{
			var supplier = await _context.Suppliers.FirstOrDefaultAsync(x => x.Code == request.Code);

			if (supplier is null)
			{
				result = new DataResult<UpdateSupplierResponse>
				{
					IsSuccess = false,
					Message = $"Supplier with code ({request.Code}) does not exist.",
					Data = null
				};
				_logger.LogWarning($"Warning (UpdateSupplierRequest - PurchaseService.Infrastructure): {result.Message}");
				return result;
			}

			supplier.Phone = request.Phone;
			supplier.Email = request.Email;
			supplier.Address = request.Address;
			supplier.Name = request.Name;

			_context.Suppliers.Update(supplier);
			await _context.SaveChangesAsync();

			UpdateSupplierResponse updateSupplierResponse = new UpdateSupplierResponse
			{
				Code = supplier.Code,
				Name = supplier.Name,
				Phone = supplier.Phone,
				Email = supplier.Email,
				Address = supplier.Address
			};

			result = new DataResult<UpdateSupplierResponse>
			{
				IsSuccess = true,
				Message = $"Supplier with code ({supplier.Code}) updated successfully.",
				Data = updateSupplierResponse
			};

			_logger.LogInformation($"Success (UpdateSupplierRequest - PurchaseService.Infrastructure): {result.Message}");
		}
		catch (Exception ex)
		{
			_logger.LogError($"Error (UpdateSupplierRequest - PurchaseService.Infrastructure): {ex.Message}");
			result = new DataResult<UpdateSupplierResponse>
			{
				IsSuccess = false,
				Message = $"Error: {ex.Message}",
				Data = null
			};
		}

		return result;
	}
}
