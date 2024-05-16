using Microsoft.EntityFrameworkCore;
using Application.Repositories.Interfaces.Extensions;
using System.Linq.Expressions;
using Persistence.Context;
using Domain.Modules;
using Domain.Modules.Product;

namespace Application.Repositories.Interfaces
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDbContext _context;

        public StockRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<PaginatedList<ProductStockApp>> GetAllWherePaginatedAsync(int pageNumber, int pageSize, Expression<Func<ProductStockApp, bool>> expression)
        {
            var result = await _context.ProductStocks
                .Include(s => s.Product)
                .OrderBy(s => s.Product.Name)
                .Where(expression)
                .ToPaginatedListAsync(pageNumber, pageSize);

            return result;
        }


        public async Task<ProductStockApp> GetByProductIdAsync(Guid productId)
        {
            return await _context.ProductStocks
                                .Include(ps => ps.Product)
                                .FirstOrDefaultAsync(ps => ps.ProductId == productId);
        }


        public async Task<ProductStockApp> GetByIdAsync(Guid id)
        {
            return await _context.ProductStocks
                                .Include(ps => ps.Product)
                                .FirstOrDefaultAsync(ps => ps.Id == id);
        }


        public void Create(ProductStockApp productStock)
        {
            _context.ProductStocks.Add(productStock);
        }


        public void Update(ProductStockApp productStock)
        {
            _context.ProductStocks.Update(productStock);
        }


        public void Delete(ProductStockApp productStock)
        {
            _context.ProductStocks.Remove(productStock);
        }
    }
}