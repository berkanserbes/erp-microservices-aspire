using ERP.SalesService.Application.Services;
using ERP.SalesService.Infrastructure.Contexts;
using ERP.Shared.Contracts.DTOs.ProductService.Product.Responses;
using ERP.Shared.Contracts.DTOs.SalesService.Customer.Requests;
using ERP.Shared.Contracts.DTOs.SalesService.Customer.Responses;
using ERP.Shared.Contracts.Results;
using ERP.Shared.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace ERP.SalesService.Infrastructure.Services;

public class CustomerService(ILogger<CustomerService> logger,
							SalesDbContext context,
							IMemoryCache memoryCache) : ICustomerService
{
	private readonly ILogger<CustomerService> _logger = logger;
	private readonly SalesDbContext _context = context;
	private readonly IMemoryCache _memoryCache = memoryCache;


	public async Task<DataResult<CreateCustomerResponse>> AddAsync(CreateCustomerRequest request)
	{
		DataResult<CreateCustomerResponse> result = null!;
		try
		{
			var exists = _context.Customers.Any(x => x.Code == request.Code);

			if (exists)
			{
				result = new DataResult<CreateCustomerResponse>
				{
					IsSuccess = false,
					Message = $"Customer with code ({request.Code}) already exists.",
					Data = null
				};
				_logger.LogWarning($"Warning (CreateCustomerRequest - SalesService.Infrastructure): {result.Message}");
				return result;
			}

			var customer = new Customer
			{
				Code = request.Code,
				Name = request.Name,
				Email = request.Email,
				Phone = request.Phone,
				Address = request.Address,
			};

			_context.Customers.Add(customer);
			await _context.SaveChangesAsync();

			var cacheOptions = new MemoryCacheEntryOptions {
				AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10),
				SlidingExpiration = TimeSpan.FromMinutes(2)
			};

			_memoryCache.Set($"customer:code:{customer.Code}", customer, cacheOptions);
			_memoryCache.Remove("customer:all");

			var response = new CreateCustomerResponse
			{
				Id = customer.Id,
				Code = customer.Code,
				Name = customer.Name,
				Email = customer.Email,
				Phone = customer.Phone,
				Address = customer.Address,
			};

			result = new DataResult<CreateCustomerResponse>
			{
				IsSuccess = true,
				Message = "Customer created successfully.",
				Data = response
			};

			_logger.LogInformation($"Success (CreateCustomerRequest - SalesService.Infrastructure): {result.Message} - {result.Data.Id}");
		}
		catch (Exception ex)
		{
			_logger.LogError($"Error (CreateCustomerRequest - SalesService.Infrastructure): {ex.Message}");
			result = new DataResult<CreateCustomerResponse>
			{
				IsSuccess = false,
				Message = $"Error: {ex.Message}",
				Data = null
			};

		}

		return result;
	}

	public async Task<DataResult<DeleteCustomerResponse>> DeleteAsync(DeleteCustomerRequest request)
	{
		DataResult<DeleteCustomerResponse> result = null!;
		try
		{
			var existingCustomer = await _context.Customers.FirstOrDefaultAsync(x => x.Code == request.Code);

			if (existingCustomer is null)
			{
				result = new DataResult<DeleteCustomerResponse>
				{
					IsSuccess = false,
					Message = $"Customer with code ({request.Code}) does not exist.",
					Data = null
				};
				_logger.LogWarning($"Warning (DeleteCustomerRequest - SalesService.Infrastructure): {result.Message}");
				return result;
			}

			_context.Customers.Remove(existingCustomer);
			await _context.SaveChangesAsync();

			_memoryCache.Remove($"customer:code:{existingCustomer.Code}");
			_memoryCache.Remove("customer:all");

			var deleteCustomerResponse = new DeleteCustomerResponse
			{
				Id = existingCustomer.Id,
				Code = existingCustomer.Code,
				Name = existingCustomer.Name,
				Email = existingCustomer.Email,
				Phone = existingCustomer.Phone,
				Address = existingCustomer.Address,
			};

			result = new DataResult<DeleteCustomerResponse>
			{
				IsSuccess = true,
				Message = $"Customer with code ({existingCustomer.Code}) deleted successfully.",
				Data = deleteCustomerResponse
			};

			_logger.LogInformation($"Success (DeleteCustomerRequest - SalesService.Infrastructure): {result.Message} - {result.Data.Id}");

		}
		catch (Exception ex)
		{
			_logger.LogError($"Error (DeleteCustomerRequest - SalesService.Infrastructure): {ex.Message}");

			result = new DataResult<DeleteCustomerResponse>
			{
				IsSuccess = false,
				Message = $"Error: {ex.Message}",
				Data = null
			};
		}

		return result;
	}

	public async Task<DataResult<IEnumerable<GetCustomerResponse>>> GetAllAsync()
	{
		DataResult<IEnumerable<GetCustomerResponse>> result = null!;
		try
		{
			if(_memoryCache.TryGetValue("customer:all", out var cachedObjects) && cachedObjects is IEnumerable<GetCustomerResponse> cachedCustomers)
			{
				result = new DataResult<IEnumerable<GetCustomerResponse>>
				{
					IsSuccess = true,
					Message = "Customers retrieved from cache.",
					Data = cachedCustomers
				};

				_logger.LogInformation($"Success (GetCustomerResponse - SalesService.Infrastructure): {result.Message} - {result.Data.Count()}");
				return result;
			}

			var customers = await _context.Customers.AsNoTracking().Select(x => new GetCustomerResponse
			{
				Id = x.Id,
				Code = x.Code,
				Name = x.Name,
				Email = x.Email,
				Phone = x.Phone,
				Address = x.Address,
			}).ToListAsync();

			if(customers.Any())
			{
				var cacheOptions = new MemoryCacheEntryOptions
				{
					AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10),
					SlidingExpiration = TimeSpan.FromMinutes(2)
				};

				_memoryCache.Set("customer:all", customers, cacheOptions);
			}

			result = new DataResult<IEnumerable<GetCustomerResponse>>
			{
				IsSuccess = true,
				Message = "Customers retrieved successfully.",
				Data = customers
			};

			_logger.LogInformation($"Success (GetCustomerResponse - SalesService.Infrastructure): {result.Message} - {result.Data.Count()}");
		}
		catch (Exception ex)
		{
			_logger.LogError($"Error (GetCustomerResponse - SalesService.Infrastructure): {ex.Message}");
			result = new DataResult<IEnumerable<GetCustomerResponse>>
			{
				IsSuccess = false,
				Message = $"Error: {ex.Message}",
				Data = null
			};

		}
		return result;
	}

	public async Task<DataResult<GetCustomerResponse>> GetByCodeAsync(GetCustomerRequest request)
	{
		DataResult<GetCustomerResponse> result = null!;
		try
		{
			if (_memoryCache.TryGetValue($"customer:code:{request.Code}", out var cachedObj) && cachedObj is GetCustomerResponse cachedProduct)
			{
				result = new DataResult<GetCustomerResponse>
				{
					IsSuccess = true,
					Message = $"Customer with code ({request.Code}) retrieved from cache.",
					Data = cachedProduct
				};
				_logger.LogInformation($"Success (GetCustomerResponse - SalesService.Infrastructure): {result.Message} - {result.Data.Id}");
				return result;
			}

			var customer = await _context.Customers.AsNoTracking()
													.Where(x => x.Code == request.Code)
													.Select(x => new GetCustomerResponse
													{
														Id = x.Id,
														Code = x.Code,
														Name = x.Name,
														Email = x.Email,
														Phone = x.Phone,
														Address = x.Address,
													})
													.FirstOrDefaultAsync();

			if (customer is null)
			{
				result = new DataResult<GetCustomerResponse>
				{
					IsSuccess = false,
					Message = $"Customer with code ({request.Code}) does not exist.",
					Data = null
				};

				_logger.LogWarning($"Warning (GetCustomerRequest - SalesService.Infrastructure): {result.Message}");
				return result;
			}

			var cacheOptions = new MemoryCacheEntryOptions
			{
				AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10),
				SlidingExpiration = TimeSpan.FromMinutes(2)
			};

			_memoryCache.Set($"customer:code:{customer.Code}", customer, cacheOptions);

			result = new DataResult<GetCustomerResponse>
			{
				IsSuccess = true,
				Message = "Customer retrieved successfully.",
				Data = customer
			};

			_logger.LogInformation($"Success (GetCustomerResponse - SalesService.Infrastructure): {result.Message} - {result.Data.Id}");

		}
		catch (Exception ex)
		{
			_logger.LogError($"Error (GetCustomerResponse - SalesService.Infrastructure): {ex.Message}");

			result = new DataResult<GetCustomerResponse>
			{
				IsSuccess = false,
				Message = $"Error: {ex.Message}",
				Data = null
			};
		}
		return result;
	}

	public async Task<DataResult<UpdateCustomerResponse>> UpdateAsync(UpdateCustomerRequest request)
	{
		DataResult<UpdateCustomerResponse> result = null!;
		try
		{
			var customer = await _context.Customers.FirstOrDefaultAsync(x => x.Code == request.Code);

			if (customer is null)
			{
				result = new DataResult<UpdateCustomerResponse>
				{
					IsSuccess = false,
					Message = $"Customer with code ({request.Code}) does not exist.",
					Data = null
				};
				_logger.LogWarning($"Warning (UpdateCustomerRequest - SalesService.Infrastructure): {result.Message}");
				return result;
			}

			customer.Phone = request.Phone;
			customer.Email = request.Email;
			customer.Address = request.Address;
			customer.Name = request.Name;

			_context.Customers.Update(customer);
			await _context.SaveChangesAsync();

			_memoryCache.Remove($"customer:code:{customer.Code}");
			_memoryCache.Remove("customer:all");

			var cacheOptions = new MemoryCacheEntryOptions
			{
				AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10),
				SlidingExpiration = TimeSpan.FromMinutes(2)
			};

			_memoryCache.Set($"customer:code:{customer.Code}", customer, cacheOptions);

			UpdateCustomerResponse updateCustomerResponse = new UpdateCustomerResponse
			{
				Code = customer.Code,
				Name = customer.Name,
				Phone = customer.Phone,
				Email = customer.Email,
				Address = customer.Address
			};

			result = new DataResult<UpdateCustomerResponse>
			{
				IsSuccess = true,
				Message = $"Customer with code ({customer.Code}) updated successfully.",
				Data = updateCustomerResponse
			};

			_logger.LogInformation($"Success (UpdateCustomerRequest - SalesService.Infrastructure): {result.Message}");
		}
		catch (Exception ex)
		{
			_logger.LogError($"Error (UpdateCustomerRequest - SalesService.Infrastructure): {ex.Message}");
			result = new DataResult<UpdateCustomerResponse>
			{
				IsSuccess = false,
				Message = $"Error: {ex.Message}",
				Data = null
			};
		}

		return result;
	}
}
