using Microsoft.AspNetCore.Identity;

namespace Domain.Modules.Identity
{
    public class RoleApp : IdentityRole<Guid>
    {
        public List<UserApp> Users { get; set; } = new List<UserApp>();
        public List<UserRoleApp> UserRoles { get; set; } = new List<UserRoleApp>();
    }
}