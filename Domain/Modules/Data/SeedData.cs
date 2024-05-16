using Domain.Modules.Identity;
using Domain.Modules.Product;
using Infrastructure.Shared.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.ObjectModel;

namespace Domain.Modules.Data
{
    public static class SeedData
    {
        public static readonly ReadOnlyCollection<ProductApp> Products = new ReadOnlyCollection<ProductApp>
            (new List<ProductApp> {
                new ProductApp
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000001"),
                    Name = "Abacate",
                    Price = 9.99,
                    Description = "Abacate 1kg"
                },

                new ProductApp
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000002"),
                    Name = "Berinjela",
                    Price = 3.00,
                    Description = "Berinjela preta 1kg"
                },

                new ProductApp
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000003"),
                    Name = "Coco",
                    Price = 7.50,
                    Description = "Coco seco un"
                },

                new ProductApp
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000004"),
                    Name = "Danoninho",
                    Price = 6.00,
                    Description = "Danoninho Ice 70g"
                },

                new ProductApp
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000005"),
                    Name = "Espaguete",
                    Price = 4.00,
                    Description = "Espaguete Isabela 500g"
                }
            });

        public static readonly ReadOnlyCollection<ProductStockApp> ProductStocks = new ReadOnlyCollection<ProductStockApp>
            (new List<ProductStockApp>
            {
                new ProductStockApp
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000001"),
                    ProductId = new Guid("00000000-0000-0000-0000-000000000001"),
                    Quantity = 200
                },

                new ProductStockApp
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000002"),
                    ProductId = new Guid("00000000-0000-0000-0000-000000000002"),
                    Quantity = 100
                },

                new ProductStockApp
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000003"),
                    ProductId = new Guid("00000000-0000-0000-0000-000000000003"),
                    Quantity = 50
                },

                new ProductStockApp
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000004"),
                    ProductId = new Guid("00000000-0000-0000-0000-000000000004"),
                    Quantity = 150
                },

                new ProductStockApp
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000005"),
                    ProductId = new Guid("00000000-0000-0000-0000-000000000005"),
                    Quantity = 200
                }
            });
        

        public static readonly ReadOnlyCollection<RoleApp> Roles = new ReadOnlyCollection<RoleApp>
            (new List<RoleApp>{
                new RoleApp
                {
                    Id = AppConstants.Roles.Admin.Id,
                    Name = AppConstants.Roles.Admin.Name,
                    NormalizedName = AppConstants.Roles.Admin.NormalizedName
    },

                new RoleApp
                {
                    Id = AppConstants.Roles.Manager.Id,
                    Name = AppConstants.Roles.Manager.Name,
                    NormalizedName = AppConstants.Roles.Manager.NormalizedName
},

                new RoleApp
                {
                    Id = AppConstants.Roles.Stock.Id,
                    Name = AppConstants.Roles.Stock.Name,
                    NormalizedName = AppConstants.Roles.Stock.NormalizedName
                },

                new RoleApp
                {
                    Id = AppConstants.Roles.Seller.Id,
                    Name = AppConstants.Roles.Seller.Name,
                    NormalizedName = AppConstants.Roles.Seller.NormalizedName
                }
            });

        
        public static readonly ReadOnlyCollection<UserApp> Users = new ReadOnlyCollection<UserApp> 
         (new List<UserApp>
            {
                //Admin
                new UserApp
                {
                    Id = AppConstants.Users.Admin.Id,
                    UserName = AppConstants.Users.Admin.UserName,
                    NormalizedUserName = AppConstants.Users.Admin.NormalizedUserName,
                    FirstName = AppConstants.Users.Admin.FirstName,
                    LastName = AppConstants.Users.Admin.LastName,
                    PasswordHash = new PasswordHasher<UserApp>().HashPassword(null,AppConstants.Users.Admin.Password),
                    SecurityStamp = Guid.NewGuid().ToString(),
                    DateOfBirth = DateTime.Parse(AppConstants.Users.Admin.DateOfBirth)
    },

                //Manager
                new UserApp
                {
                    Id = AppConstants.Users.Manager.Id,
                    UserName = AppConstants.Users.Manager.UserName,
                    NormalizedUserName = AppConstants.Users.Manager.NormalizedUserName,
                    FirstName = AppConstants.Users.Manager.FirstName,
                    LastName = AppConstants.Users.Manager.LastName,
                    PasswordHash = new PasswordHasher<UserApp>().HashPassword(null,AppConstants.Users.Manager.Password),
                    SecurityStamp = Guid.NewGuid().ToString(),
                    DateOfBirth = DateTime.Parse(AppConstants.Users.Manager.DateOfBirth)
},

                //Stock
                new UserApp
                {
                    Id = AppConstants.Users.Stock.Id,
                    UserName = AppConstants.Users.Stock.UserName,
                    NormalizedUserName = AppConstants.Users.Stock.NormalizedUserName,
                    FirstName = AppConstants.Users.Stock.FirstName,
                    LastName = AppConstants.Users.Stock.LastName,
                    PasswordHash = new PasswordHasher<UserApp>().HashPassword(null, AppConstants.Users.Stock.Password),
                    SecurityStamp = Guid.NewGuid().ToString(),
                    DateOfBirth = DateTime.Parse(AppConstants.Users.Stock.DateOfBirth)
                },

                //Seller
                new UserApp
                {
                    Id = AppConstants.Users.Seller.Id,
                    UserName = AppConstants.Users.Seller.UserName,
                    NormalizedUserName = AppConstants.Users.Seller.NormalizedUserName,
                    FirstName = AppConstants.Users.Seller.FirstName,
                    LastName = AppConstants.Users.Seller.LastName,
                    PasswordHash = new PasswordHasher<UserApp>().HashPassword(null, AppConstants.Users.Seller.Password),
                    SecurityStamp = Guid.NewGuid().ToString(),
                    DateOfBirth = DateTime.Parse(AppConstants.Users.Seller.DateOfBirth)
                },

                //Public
                new UserApp
                {
                    Id = AppConstants.Users.Public.Id,
                    UserName = AppConstants.Users.Public.UserName,
                    NormalizedUserName = AppConstants.Users.Public.NormalizedUserName,
                    FirstName = AppConstants.Users.Public.FirstName,
                    LastName = AppConstants.Users.Public.LastName,
                    PasswordHash = new PasswordHasher<UserApp>().HashPassword(null, AppConstants.Users.Public.Password),
                    SecurityStamp = Guid.NewGuid().ToString(),
                    DateOfBirth = DateTime.Parse(AppConstants.Users.Public.DateOfBirth)
                }
            });

        public static readonly ReadOnlyCollection<UserRoleApp> UserRoles = new ReadOnlyCollection<UserRoleApp> 
            (new List<UserRoleApp>
            {
                new UserRoleApp { RoleId = new Guid("00000000-0000-0000-0000-000000000001"), UserId = new Guid("00000000-0000-0000-0000-000000000001") },
                new UserRoleApp { RoleId = new Guid("00000000-0000-0000-0000-000000000002"), UserId = new Guid("00000000-0000-0000-0000-000000000002") },
                new UserRoleApp { RoleId = new Guid("00000000-0000-0000-0000-000000000003"), UserId = new Guid("00000000-0000-0000-0000-000000000003") },
                new UserRoleApp { RoleId = new Guid("00000000-0000-0000-0000-000000000004"), UserId = new Guid("00000000-0000-0000-0000-000000000004") }
            });
    }
}