using ERP.Shared.Contracts.DTOs.ProductService.Product.Requests;
using ERP.Shared.Contracts.DTOs.ProductService.Product.Responses;
using ERP.Shared.Contracts.DTOs.ProductService.Variant.Requests;
using ERP.Shared.Contracts.DTOs.ProductService.Variant.Responses;
using ERP.Shared.Contracts.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.ProductService.Application.Services;

public interface IVariantService
{
	Task<DataResult<IEnumerable<GetVariantResponse>>> GetAllAsync();
	Task<DataResult<GetVariantResponse>> GetByCodeAsync(GetVariantRequest getVariantRequest);
	Task<DataResult<CreateVariantResponse>> AddAsync(CreateVariantRequest createVariantRequest);
	Task<DataResult<UpdateVariantResponse>> UpdateAsync(UpdateVariantRequest updateVariantRequest);
	Task<DataResult<DeleteVariantResponse>> DeleteAsync(DeleteVariantRequest deleteVariantRequest);
}
