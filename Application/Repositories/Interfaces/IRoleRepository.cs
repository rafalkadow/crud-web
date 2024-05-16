using Domain.Modules;
using Domain.Modules.Identity;
using Microsoft.AspNetCore.Identity;
using System.Linq.Expressions;

namespace Application.Repositories.Interfaces
{
    public interface IRoleRepository
    {
        public Task<PaginatedList<RoleApp>> GetAllWherePaginatedAsync(int pageNumber, int pageSize, Expression<Func<RoleApp, bool>> expression);

        public Task<RoleApp> GetByNameAsync(string roleName);

        public Task<RoleApp> GetByIdAsync(Guid id);

        public Task<IdentityResult> CreateAsync(RoleApp role);

        public Task<IdentityResult> DeleteAsync(RoleApp role);
    }
}