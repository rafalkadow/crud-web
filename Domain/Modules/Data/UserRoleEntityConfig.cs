using Domain.Modules.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Modules.Data
{
    public class UserRoleEntityConfig : IEntityTypeConfiguration<UserRoleApp>
    {
        public void Configure(EntityTypeBuilder<UserRoleApp> builder)
        {
            builder.HasData(new List<UserRoleApp>
            {
                new UserRoleApp { RoleId = new Guid("00000000-0000-0000-0000-000000000001"), UserId = new Guid("00000000-0000-0000-0000-000000000001") },
                new UserRoleApp { RoleId = new Guid("00000000-0000-0000-0000-000000000002"), UserId = new Guid("00000000-0000-0000-0000-000000000002") },
                new UserRoleApp { RoleId = new Guid("00000000-0000-0000-0000-000000000003"), UserId = new Guid("00000000-0000-0000-0000-000000000003") },
                new UserRoleApp { RoleId = new Guid("00000000-0000-0000-0000-000000000004"), UserId = new Guid("00000000-0000-0000-0000-000000000004") }
            });
        }
    }
}