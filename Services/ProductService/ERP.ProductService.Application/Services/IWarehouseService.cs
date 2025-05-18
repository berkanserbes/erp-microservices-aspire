using ERP.Shared.Contracts.DTOs.ProductService.Warehouse.Requests;
using ERP.Shared.Contracts.DTOs.ProductService.Warehouse.Responses;
using ERP.Shared.Contracts.Results;
using ERP.Shared.Domain.Entities;

namespace ERP.ProductService.Application.Services;

public interface IWarehouseService
{
	Task<DataResult<IEnumerable<GetWarehouseResponse>>> GetAllAsync();
	Task<DataResult<GetWarehouseResponse>> GetByNumberAsync(GetWarehouseRequest getWarehouseRequest);
	Task<DataResult<CreateWarehouseResponse>> AddAsync(CreateWarehouseRequest createWarehouseRequest);
	Task<DataResult<UpdateWarehouseResponse>> UpdateAsync(UpdateWarehouseRequest updateWarehouseRequest);
	Task<DataResult<DeleteWarehouseResponse>> DeleteAsync(DeleteWarehouseRequest deleteWarehouseRequest);
}
