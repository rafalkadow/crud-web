using Domain.Modules;
using Domain.Modules.Identity;
using Microsoft.AspNetCore.Identity;
using System.Linq.Expressions;

namespace Application.Repositories.Interfaces
{
    public interface IUserRepository
    {
        public Task<PaginatedList<UserApp>> GetAllWherePaginatedAsync(int pageNumber, int pageSize, Expression<Func<UserApp, bool>> expression);

        public Task<UserApp> GetByUserNameAsync(string userName);

        public Task<UserApp> GetCurrentUserAsync();

        public Task<UserApp> GetByIdAsync(Guid id);

        //public Task<IEnumerable<User>> SearchAsync(string search);

        public Task<IList<string>> GetRolesNamesAsync(UserApp user);

        public Task<IdentityResult> CreateAsync(UserApp user, string password);

        public Task<IdentityResult> UpdateUserAsync(UserApp user);

        public Task<IdentityResult> ChangePasswordAsync(UserApp user, string currentPassword, string newPassword);

        public Task<IdentityResult> ResetPasswordAsync(UserApp user, string newPassword);

        public Task<IdentityResult> AddToRoleAsync(UserApp user, string roleName);

        public Task<IdentityResult> RemoveFromRoleAsync(UserApp user, string roleName);

        public Task ResetTestUsers();

        public Task<List<UserApp>> GetUserByNameRangeAsync(IEnumerable<string> userNames);
        public Task<IdentityResult> DeleteAsync(UserApp user);
    }
}