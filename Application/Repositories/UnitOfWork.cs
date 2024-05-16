using Domain.Modules.Exceptions;
using Persistence.Context;

namespace Application.Repositories.Interfaces
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> CompleteAsync()
        {
            try
            {
                var result = await _context.SaveChangesAsync();
                return result;
            }
            catch (Exception e)
            {
                throw new InfrastructureException()
                    .SetTitle("Error saving data")
                    .SetDetail(e.Message);
            }
        }
    }
}