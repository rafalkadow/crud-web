﻿using Application;
using Application.Extensions;
using Application.Repositories.Interfaces;
using Application.Services;
using Domain.Interfaces;
using Domain.Modules.Exceptions;
using Domain.Modules.Identity;
using Infrastructure.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Logging;
using Persistence.Context;
using Swashbuckle.AspNetCore.Filters;
using System.Text.Json;
using System.Text.Json.Serialization;
using Web.Api.Exceptions;
using Web.Api.Extensions;
using Web.Api.Swagger;

namespace Web.Api
{
    public class Startup
    {
        private readonly IWebHostEnvironment _env;

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            _env = env;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public virtual void ConfigureServices(IServiceCollection services)
        {

            var section = Configuration.GetSection("StoreDbSQLServerConnectionStringSettings");

            var connectionStringBuilder = new SqlConnectionStringBuilder()
            {
                WorkstationID = section["WorkstationId"],
                DataSource = section["DataSource"],
                InitialCatalog = section["InitialCatalog"],
                MultipleActiveResultSets = bool.Parse(section["MultipleActiveResultSets"]),
                UserID = section["UserId"],
                Password = section["DbPassword"]
            };

            var connectionString = connectionStringBuilder.ConnectionString;


            services.AddDbContext<IDbContext, ApplicationDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));

                if (_env.IsDevelopment())
                {
                    options.EnableSensitiveDataLogging()
                           .EnableDetailedErrors()
                           .LogTo(Console.WriteLine);
                }
                else
                {
                    options.LogTo(Console.WriteLine, LogLevel.Error);
                }
            });

            services.AddIdentityCore<UserApp>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequiredLength = AppConstants.Validations.User.PasswordMinLength;
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
            })
                .AddRoles<RoleApp>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders()
                .AddRoleValidator<RoleValidator<RoleApp>>()
                .AddRoleManager<RoleManager<RoleApp>>()
                .AddSignInManager<SignInManager<UserApp>>();


            var x = services.AddMvc(options =>
            {
                var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();

                options.Filters.Add(new AuthorizeFilter(policy));
                options.ModelMetadataDetailsProviders.Add(new CustomMetadataProvider());
            })

            .ConfigureApiBehaviorOptions(o =>
            {
                o.InvalidModelStateResponseFactory = m => throw new ModelValidationException(m.ModelState);
                o.SuppressMapClientErrors = true;
            });
            x = x.AddJsonOptions(o =>
            {
                o.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                o.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
                o.JsonSerializerOptions.Converters.Add(new CustomDateTimeConverter());
                o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

            services.AddCors();
            services.Register();
            //app services
            //services.AddAutoMapper(typeof(IAssemblyApplicationMarker).Assembly);
            services.AddIfrastructure();
            //jwt token
            services.AddJwtAuthentication(Configuration);
            services.AddScoped<IStockService, StockService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITestAccountService, TestAccountService>();

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IStockRepository, StockRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            //swagger

            services.AddSwaggerExamplesFromAssemblies(typeof(IAssemblyDomainMarker).Assembly);
            services.AddSwaggerConfiguration();

            //services.AddSwaggerExamplesFromAssemblies(typeof(IAssemblyApplicationMarker).Assembly);


            services.AddTransient<ExceptionHandlerMiddleware>();
            services.AddSwaggerGenNewtonsoftSupport();
            //services.AddHttpContextAccessor();
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public virtual void Configure(IApplicationBuilder app, IWebHostEnvironment env, IActionDescriptorCollectionProvider actionProvider, ILogger<Startup> logger)
        {
            //CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
            //CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.InvariantCulture;

            logger.Log(LogLevel.Information, "Configuring application ...");

            if (env.IsDevelopment())
            {
                IdentityModelEventSource.ShowPII = true;
            }

            app.UseMiddleware<ExceptionHandlerMiddleware>();

            app.UseCors(p => p
                .AllowAnyHeader()
                .AllowAnyMethod()
                .SetIsOriginAllowed(origin => true)
                .AllowCredentials()
            );

            app.UseAuthentication();

            app.UseSwagger();

            app.UseSwaggerUI(opt =>
            {
                opt.EnablePersistAuthorization();
                opt.ShowCommonExtensions();
                opt.ShowExtensions();
                opt.EnableDeepLinking();
                opt.SwaggerEndpoint("/swagger/v1/swagger.json", "crud-web API");
            });


            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}