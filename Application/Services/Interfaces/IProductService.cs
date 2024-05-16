using Domain.Modules;
using Domain.Modules.Communication;
using Domain.Modules.Communication.Generics;
using Domain.Modules.Product;
using Domain.Modules.QueryStringParameters;

namespace Application.Services
{
    public interface IProductService
    {
        public Task<ServiceResponse<ProductReadWithStockDto>> CreateAsync(ProductWriteDto productDto, int quantity = 0);

        public Task<ServiceResponse<PaginatedList<ProductReadDto>>> GetAllDtoPaginatedAsync(ProductParametersDto parameters);

        public Task<ServiceResponse<ProductReadDto>> GetDtoByIdAsync(Guid id);

        public Task<ServiceResponse<ProductReadDto>> UpdateAsync(Guid productId, ProductWriteDto product);

        public Task<BaseResponse> DeleteAsync(Guid id);
    }
}