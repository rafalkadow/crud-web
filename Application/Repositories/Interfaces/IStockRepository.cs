using Domain.Modules;
using Domain.Modules.Product;
using System.Linq.Expressions;

namespace Application.Repositories.Interfaces
{
    public interface IStockRepository
    {
        public Task<PaginatedList<ProductStockApp>> GetAllWherePaginatedAsync(int pageNumber, int pageSize, Expression<Func<ProductStockApp, bool>> expression);

        public Task<ProductStockApp> GetByProductIdAsync(Guid productId);

        public Task<ProductStockApp> GetByIdAsync(Guid id);

        public void Create(ProductStockApp productStock);

        public void Update(ProductStockApp productStock);

        public void Delete(ProductStockApp productStock);
    }
}