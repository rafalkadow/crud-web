using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Application.Repositories.Interfaces.Extensions;
using System.Linq.Expressions;
using Domain.Modules.Identity;
using Persistence.Context;
using Domain.Modules;

namespace Application.Repositories.Interfaces
{
    public class RoleRepository : IRoleRepository
    {
        private readonly RoleManager<RoleApp> _roleManager;
        private readonly ApplicationDbContext _context;

        public RoleRepository(ApplicationDbContext context, RoleManager<RoleApp> roleManager)
        {
            _roleManager = roleManager;
            _context = context;
        }



        public async Task<PaginatedList<RoleApp>> GetAllWherePaginatedAsync(int pageNumber, int pageSize, Expression<Func<RoleApp, bool>> expression)
        {
            var result = await _context.Roles
                    .OrderBy(r => r.Id)
                    .Include(r => r.Users)
                    .Where(expression)
                    .ToPaginatedListAsync(pageNumber, pageSize);

            return result;
        }


        public async Task<RoleApp> GetByNameAsync(string roleName)
        {
            var role = await _roleManager.Roles
                                .Include(r => r.Users)
                                .FirstOrDefaultAsync(r => r.NormalizedName == roleName.ToUpper());
            return role;
        }


        public async Task<RoleApp> GetByIdAsync(Guid id)
        {
            var role = await _roleManager.Roles
                                .Include(r => r.Users)
                                .FirstOrDefaultAsync(r => r.Id == id);

            return role;
        }


        public async Task<IdentityResult> CreateAsync(RoleApp role)
        {
            var result = await _roleManager.CreateAsync(role);
            return result;
        }


        public async Task<IdentityResult> DeleteAsync(RoleApp role)
        {
            var result = await _roleManager.DeleteAsync(role);
            return result;
        }
    }
}