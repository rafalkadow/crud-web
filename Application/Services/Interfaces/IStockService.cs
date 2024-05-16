using Domain.Modules;
using Domain.Modules.Communication.Generics;
using Domain.Modules.Product;
using Domain.Modules.ProductStock;
using Domain.Modules.QueryStringParameters;

namespace Application.Services
{
    public interface IStockService
    {
        public Task<ServiceResponse<PaginatedList<ProductStockReadDto>>> GetAllDtoPaginatedAsync(StockParametersDto parameters);

        public Task<ServiceResponse<ProductStockReadDto>> GetDtoByProductIdAsync(Guid productId);

        public Task<ServiceResponse<ProductStockReadDto>> GetDtoByIdAsync(Guid id);

        public ProductStockApp CreateProductStock(ProductApp product, int startingQuantity = 0);

        public Task<ServiceResponse<ProductStockReadDto>> UpdateAsync(Guid id, ProductStockWriteDto dto);

        public Task<ServiceResponse<ProductStockReadDto>> RemoveProductQuantityAsync(Guid id, int quantity);

        public Task<ServiceResponse<ProductStockReadDto>> AddProductQuantityAsync(Guid id, int quantity);
    }
}