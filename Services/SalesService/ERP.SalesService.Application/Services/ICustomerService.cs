using ERP.Shared.Contracts.DTOs.SalesService.Customer.Requests;
using ERP.Shared.Contracts.DTOs.SalesService.Customer.Responses;
using ERP.Shared.Contracts.Results;

namespace ERP.SalesService.Application.Services;

public interface ICustomerService
{
	Task<DataResult<IEnumerable<GetCustomerResponse>>> GetAllAsync();
	Task<DataResult<GetCustomerResponse>> GetByCodeAsync(GetCustomerRequest getCustomerRequest);
	Task<DataResult<CreateCustomerResponse>> AddAsync(CreateCustomerRequest createCustomerRequest);
	Task<DataResult<UpdateCustomerResponse>> UpdateAsync(UpdateCustomerRequest updateCustomerResponse);
	Task<DataResult<DeleteCustomerResponse>> DeleteAsync(DeleteCustomerRequest deleteCustomerResponse);
}
