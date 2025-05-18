using ERP.Shared.Contracts.DTOs.PurchaseService.Supplier.Requests;
using ERP.Shared.Contracts.DTOs.PurchaseService.Supplier.Responses;
using ERP.Shared.Contracts.Results;

namespace ERP.PurchaseService.Application.Services;

public interface ISupplierService
{
	Task<DataResult<IEnumerable<GetSupplierResponse>>> GetAllAsync();
	Task<DataResult<GetSupplierResponse>> GetByCodeAsync(GetSupplierRequest getSupplierRequest);
	Task<DataResult<CreateSupplierResponse>> AddAsync(CreateSupplierRequest createSupplierRequest);
	Task<DataResult<UpdateSupplierResponse>> UpdateAsync(UpdateSupplierRequest updateSupplierRequest);
	Task<DataResult<DeleteSupplierResponse>> DeleteAsync(DeleteSupplierRequest deleteSupplierRequest);
}
