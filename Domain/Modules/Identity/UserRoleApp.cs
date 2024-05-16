using Microsoft.AspNetCore.Identity;

namespace Domain.Modules.Identity
{
    public class UserRoleApp : IdentityUserRole<Guid>
    {
        public UserApp User { get; set; }
        public virtual RoleApp Role { get; set; }
    }
}