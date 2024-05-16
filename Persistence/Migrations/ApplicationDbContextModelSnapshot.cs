﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Persistence.Context;

#nullable disable

namespace Persistence.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseCollation("SQL_Latin1_General_CP1250_CS_AS")
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Domain.Modules.Account.AccountModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AccountEmail")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("AccountPassword")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("AccountTypeId")
                        .HasColumnType("int");

                    b.Property<string>("AccountTypeName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTime?>("CreatedOnDateTimeUTC")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("CreatedUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CreatedUserName")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("FirstName")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("LastName")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTime?>("ModifiedOnDateTimeUTC")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("ModifiedUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ModifiedUserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("OrderId")
                        .HasColumnType("decimal(20,0)");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("RecordStatus")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.HasKey("Id");

                    b.HasIndex("AccountEmail")
                        .IsUnique();

                    b.HasIndex("OrderId");

                    b.ToTable("Account", "dbo");
                });

            modelBuilder.Entity("Domain.Modules.Base.Models.Audit", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("AffectedColumns")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("NewValues")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OldValues")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PrimaryKey")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TableName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Audits");
                });

            modelBuilder.Entity("Domain.Modules.CategoryOfProduct.Models.CategoryOfProductModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Code")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime?>("CreatedOnDateTimeUTC")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("CreatedUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CreatedUserName")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTime?>("ModifiedOnDateTimeUTC")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("ModifiedUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ModifiedUserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<decimal>("OrderId")
                        .HasColumnType("decimal(20,0)");

                    b.Property<string>("RecordStatus")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.HasIndex("OrderId");

                    b.ToTable("CategoryOfProduct", "dbo");
                });

            modelBuilder.Entity("Domain.Modules.Identity.RoleApp", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000001"),
                            Name = "Administrator",
                            NormalizedName = "ADMINISTRATOR"
                        },
                        new
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000002"),
                            Name = "Manager",
                            NormalizedName = "MANAGER"
                        },
                        new
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000003"),
                            Name = "Stock",
                            NormalizedName = "STOCK"
                        },
                        new
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000004"),
                            Name = "Seller",
                            NormalizedName = "SELLER"
                        });
                });

            modelBuilder.Entity("Domain.Modules.Identity.UserApp", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreatedOnDateTimeUTC")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("CreatedUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CreatedUserName")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTime?>("ModifiedOnDateTimeUTC")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("ModifiedUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ModifiedUserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<decimal>("OrderId")
                        .HasColumnType("decimal(20,0)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("RecordStatus")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("RefreshTokenExpiryTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000001"),
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "fc9819ad-f14b-4085-bc2b-27ae13e6fefe",
                            DateOfBirth = new DateTime(1979, 12, 31, 23, 0, 0, 0, DateTimeKind.Utc),
                            EmailConfirmed = false,
                            FirstName = "Admin",
                            IsActive = false,
                            IsDeleted = false,
                            LastName = "Istrator",
                            LockoutEnabled = false,
                            NormalizedUserName = "ADMIN",
                            OrderId = 0m,
                            PasswordHash = "AQAAAAIAAYagAAAAEHobOZnlzarn3+NNeZXHKezC33TtHnaa1WlaXr7W4+V1IdWD73lo1vDeYTLi8dId/w==",
                            PhoneNumberConfirmed = false,
                            RecordStatus = "Inactived",
                            SecurityStamp = "1893b4b8-cb23-4a82-a115-38bb75f984b4",
                            TwoFactorEnabled = false,
                            UserName = "admin"
                        },
                        new
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000002"),
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "0f739e79-28ff-4521-ac18-ec1f1cdc594f",
                            DateOfBirth = new DateTime(1989, 12, 31, 23, 0, 0, 0, DateTimeKind.Utc),
                            EmailConfirmed = false,
                            FirstName = "Manager",
                            IsActive = false,
                            IsDeleted = false,
                            LastName = "Test Acc",
                            LockoutEnabled = false,
                            NormalizedUserName = "MANAGER",
                            OrderId = 0m,
                            PasswordHash = "AQAAAAIAAYagAAAAENrHTVapqBDbxefxCXOJhtsgGvYCmrwjXvGHDv71RYj1HpLT9wB+a7S25I7mG8vPDg==",
                            PhoneNumberConfirmed = false,
                            RecordStatus = "Inactived",
                            SecurityStamp = "eda7118c-3dee-47bf-934b-1b346dc40941",
                            TwoFactorEnabled = false,
                            UserName = "manager"
                        },
                        new
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000003"),
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "cfa01d2c-8569-4a62-8a8b-4509febd35fc",
                            DateOfBirth = new DateTime(1994, 12, 31, 23, 0, 0, 0, DateTimeKind.Utc),
                            EmailConfirmed = false,
                            FirstName = "Stock",
                            IsActive = false,
                            IsDeleted = false,
                            LastName = "Test Acc",
                            LockoutEnabled = false,
                            NormalizedUserName = "STOCK",
                            OrderId = 0m,
                            PasswordHash = "AQAAAAIAAYagAAAAEHvF6o7osvL5GtspmC5oPkqHqubQKwJlZ9SRA5JVjYH5RUo++QEyPepIQtWQuO1FrQ==",
                            PhoneNumberConfirmed = false,
                            RecordStatus = "Inactived",
                            SecurityStamp = "276fe383-38e5-4eef-8fce-333057bfc558",
                            TwoFactorEnabled = false,
                            UserName = "stock"
                        },
                        new
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000004"),
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "81b9823b-2f5a-4ef5-9d1c-425587103a60",
                            DateOfBirth = new DateTime(1999, 12, 31, 23, 0, 0, 0, DateTimeKind.Utc),
                            EmailConfirmed = false,
                            FirstName = "Seller",
                            IsActive = false,
                            IsDeleted = false,
                            LastName = "Test Acc",
                            LockoutEnabled = false,
                            NormalizedUserName = "SELLER",
                            OrderId = 0m,
                            PasswordHash = "AQAAAAIAAYagAAAAEE80axQM5JvEL1LK3YTEqhr8F49oFlhphUPsDPE6cdwtD8Ei0VRiRKt2UC6HJsAmIA==",
                            PhoneNumberConfirmed = false,
                            RecordStatus = "Inactived",
                            SecurityStamp = "78c72d4e-0ac8-4ce8-9b1e-f67b6b7053ca",
                            TwoFactorEnabled = false,
                            UserName = "seller"
                        },
                        new
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000005"),
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "eea1dd56-6ede-47bc-9bf8-282e1d9f18ef",
                            DateOfBirth = new DateTime(2001, 12, 31, 23, 0, 0, 0, DateTimeKind.Utc),
                            EmailConfirmed = false,
                            FirstName = "Public",
                            IsActive = false,
                            IsDeleted = false,
                            LastName = "Test Acc",
                            LockoutEnabled = false,
                            NormalizedUserName = "PUBLIC",
                            OrderId = 0m,
                            PasswordHash = "AQAAAAIAAYagAAAAEHy72xd9qZCMQrD3IhXImPHIgbOOOWbxz+8q4MlBT+RLslULDk6hYr36d1TygoYm1A==",
                            PhoneNumberConfirmed = false,
                            RecordStatus = "Inactived",
                            SecurityStamp = "ac127cad-558f-46c2-b77f-a9ac6e78ff90",
                            TwoFactorEnabled = false,
                            UserName = "public"
                        });
                });

            modelBuilder.Entity("Domain.Modules.Identity.UserRoleApp", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);

                    b.HasData(
                        new
                        {
                            UserId = new Guid("00000000-0000-0000-0000-000000000001"),
                            RoleId = new Guid("00000000-0000-0000-0000-000000000001")
                        },
                        new
                        {
                            UserId = new Guid("00000000-0000-0000-0000-000000000002"),
                            RoleId = new Guid("00000000-0000-0000-0000-000000000002")
                        },
                        new
                        {
                            UserId = new Guid("00000000-0000-0000-0000-000000000003"),
                            RoleId = new Guid("00000000-0000-0000-0000-000000000003")
                        },
                        new
                        {
                            UserId = new Guid("00000000-0000-0000-0000-000000000004"),
                            RoleId = new Guid("00000000-0000-0000-0000-000000000004")
                        });
                });

            modelBuilder.Entity("Domain.Modules.Product.Models.ProductModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CategoryOfProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Code")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime?>("CreatedOnDateTimeUTC")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("CreatedUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CreatedUserName")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTime>("DateTimeUtc")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateUtc")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ModifiedOnDateTimeUTC")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("ModifiedUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ModifiedUserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<decimal>("OrderId")
                        .HasColumnType("decimal(20,0)");

                    b.Property<string>("RecordStatus")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<decimal>("Value")
                        .HasPrecision(18, 6)
                        .HasColumnType("decimal(18,6)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryOfProductId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.HasIndex("OrderId");

                    b.ToTable("Product", "dbo");
                });

            modelBuilder.Entity("Domain.Modules.Product.ProductApp", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000001"),
                            Description = "Abacate 1kg",
                            Name = "Abacate",
                            Price = 9.9900000000000002
                        },
                        new
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000002"),
                            Description = "Berinjela preta 1kg",
                            Name = "Berinjela",
                            Price = 3.0
                        },
                        new
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000003"),
                            Description = "Coco seco un",
                            Name = "Coco",
                            Price = 7.5
                        },
                        new
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000004"),
                            Description = "Danoninho Ice 70g",
                            Name = "Danoninho",
                            Price = 6.0
                        },
                        new
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000005"),
                            Description = "Espaguete Isabela 500g",
                            Name = "Espaguete",
                            Price = 4.0
                        });
                });

            modelBuilder.Entity("Domain.Modules.Product.ProductStockApp", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProductId")
                        .IsUnique();

                    b.ToTable("ProductStocks");

                    b.HasData(
                        new
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000001"),
                            ProductId = new Guid("00000000-0000-0000-0000-000000000001"),
                            Quantity = 200
                        },
                        new
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000002"),
                            ProductId = new Guid("00000000-0000-0000-0000-000000000002"),
                            Quantity = 100
                        },
                        new
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000003"),
                            ProductId = new Guid("00000000-0000-0000-0000-000000000003"),
                            Quantity = 50
                        },
                        new
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000004"),
                            ProductId = new Guid("00000000-0000-0000-0000-000000000004"),
                            Quantity = 150
                        },
                        new
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000005"),
                            ProductId = new Guid("00000000-0000-0000-0000-000000000005"),
                            Quantity = 200
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Domain.Modules.Identity.UserRoleApp", b =>
                {
                    b.HasOne("Domain.Modules.Identity.RoleApp", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Modules.Identity.UserApp", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Modules.Product.Models.ProductModel", b =>
                {
                    b.HasOne("Domain.Modules.CategoryOfProduct.Models.CategoryOfProductModel", "CategoryOfProduct")
                        .WithMany("Product")
                        .HasForeignKey("CategoryOfProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CategoryOfProduct");
                });

            modelBuilder.Entity("Domain.Modules.Product.ProductStockApp", b =>
                {
                    b.HasOne("Domain.Modules.Product.ProductApp", "Product")
                        .WithOne("ProductStock")
                        .HasForeignKey("Domain.Modules.Product.ProductStockApp", "ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("Domain.Modules.Identity.RoleApp", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("Domain.Modules.Identity.UserApp", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("Domain.Modules.Identity.UserApp", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("Domain.Modules.Identity.UserApp", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Modules.CategoryOfProduct.Models.CategoryOfProductModel", b =>
                {
                    b.Navigation("Product");
                });

            modelBuilder.Entity("Domain.Modules.Identity.RoleApp", b =>
                {
                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("Domain.Modules.Identity.UserApp", b =>
                {
                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("Domain.Modules.Product.ProductApp", b =>
                {
                    b.Navigation("ProductStock")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
