using Domain.Modules;
using Domain.Modules.Product;
using System.Linq.Expressions;

namespace Application.Repositories.Interfaces
{
    public interface IProductRepository
    {
        public Task<PaginatedList<ProductApp>> GetAllWherePaginatedAsync(int pageNumber, int pageSize, Expression<Func<ProductApp, bool>> expression);

        public Task<ProductApp> GetByIdAsync(Guid id);

        public void Add(ProductApp product);

        public void Update(ProductApp product);

        public void Delete(ProductApp product);
    }
}