using Microsoft.EntityFrameworkCore;
using Application.Repositories.Interfaces.Extensions;
using System.Linq.Expressions;
using Persistence.Context;
using Domain.Modules;
using Domain.Modules.Product;

namespace Application.Repositories.Interfaces
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }



        public async Task<PaginatedList<ProductApp>> GetAllWherePaginatedAsync(int pageNumber, int pageSize, Expression<Func<ProductApp, bool>> expression)
        {
            var result = await _context.Products
                 .Include(p => p.ProductStock)
                 .OrderBy(p => p.Name)
                 .Where(expression)
                 .ToPaginatedListAsync(pageNumber, pageSize);

            return result;
        }


        public async Task<ProductApp> GetByIdAsync(Guid id)
        {
            return await _context.Products
                                .Include(p => p.ProductStock)
                                .FirstOrDefaultAsync(p => p.Id == id);
        }


        public async Task<IEnumerable<ProductApp>> SearchAsync(string search)
        {
            return await _context.Products
                                    .Include(p => p.ProductStock)
                                    .Where(p =>
                                        p.Name.ToLower().Contains(search) ||
                                        p.Description.ToLower().Contains(search))
                                    .ToListAsync();
        }


        public void Add(ProductApp product)
        {
            _context.Products.Add(product);
        }


        public void Update(ProductApp product)
        {
            _context.Products.Update(product);
        }


        public void Delete(ProductApp product)
        {
            _context.Products.Remove(product);
        }
    }
}