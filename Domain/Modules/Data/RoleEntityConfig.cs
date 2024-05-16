using Domain.Modules.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Modules.Data
{
    public class RoleEntityConfig : IEntityTypeConfiguration<RoleApp>
    {
        public void Configure(EntityTypeBuilder<RoleApp> builder)
        {
            builder.HasData(SeedData.Roles);
            //    new List<Role>
            //{
            //    new Role
            //    {
            //        Id = AppConstants.Roles.Admin.Id,
            //        Name = AppConstants.Roles.Admin.Name,
            //        NormalizedName = AppConstants.Roles.Admin.NormalizedName
            //    },

            //    new Role
            //    {
            //        Id = AppConstants.Roles.Manager.Id,
            //        Name = AppConstants.Roles.Manager.Name,
            //        NormalizedName = AppConstants.Roles.Manager.NormalizedName
            //    },

            //    new Role
            //    {
            //        Id = AppConstants.Roles.Stock.Id,
            //        Name = AppConstants.Roles.Stock.Name,
            //        NormalizedName = AppConstants.Roles.Stock.NormalizedName
            //    },

            //    new Role
            //    {
            //        Id = AppConstants.Roles.Seller.Id,
            //        Name = AppConstants.Roles.Seller.Name,
            //        NormalizedName = AppConstants.Roles.Seller.NormalizedName
            //    }
            //});
        }
    }
}