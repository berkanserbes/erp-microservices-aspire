using ERP.Shared.Contracts.DTOs.ProductService.Product.Requests;
using ERP.Shared.Contracts.DTOs.ProductService.Product.Responses;
using ERP.Shared.Contracts.Results;

namespace ERP.ProductService.Application.Services;

public interface IProductService
{
	Task<DataResult<IEnumerable<GetProductResponse>>> GetAllAsync();
	Task<DataResult<GetProductResponse>> GetByCodeAsync(GetProductRequest getProductRequest);
	Task<DataResult<CreateProductResponse>> AddAsync(CreateProductRequest createProductRequest);
	Task<DataResult<UpdateProductResponse>> UpdateAsync(UpdateProductRequest updateProductRequest);
	Task<DataResult<DeleteProductResponse>> DeleteAsync(DeleteProductRequest deleteProductRequest);
}
