using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using NLog;
using NLog.Web;
using System.Reflection;
using Shared.Models;
using Persistence.Context;
using Shared.Extensions.GeneralExtensions;
using Microsoft.EntityFrameworkCore;
using Domain.Modules.SignIn.Consts;
using Application.Extensions;
using Web.Extensions;
using Domain.Interfaces;
using Web.Middlewares;

namespace Web
{
    public class Startup
	{
		public IConfiguration Configuration { get; set; }
		public IWebHostEnvironment WebHostEnvironment { get; set; }

		public Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
		{
			Configuration = configuration;
			WebHostEnvironment = webHostEnvironment;

			var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();

			logger.Info($"Startup(EnvironmentName='{webHostEnvironment.EnvironmentName}')");
			ReadConfiguration();
		}

		private void ReadConfiguration()
		{
			ValuesModel.ContentRootPath = WebHostEnvironment.ContentRootPath;
			var logIndent = Configuration["Configuration:LogIndent"];
			
			if (!string.IsNullOrEmpty(logIndent))
			{
				GeneralExtensions.Indent = logIndent;
			}
		}

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
            Application.ServiceCollection.Register(services);

            services.AddDbContext<IDbContext, ApplicationDbContext>(cfg =>
			{
				var connectionString = Configuration.GetConnectionString("DefaultConnection");
				cfg.UseSqlServer(connectionString, providerOptions =>
				{
					providerOptions.CommandTimeout(180);// <--Timeout in seconds
				});
				cfg.EnableSensitiveDataLogging(true);
            }, ServiceLifetime.Scoped);

            services.AddIfrastructure();
            services.AddSession(options =>
			{
				options.IdleTimeout = TimeSpan.FromMinutes(30);
				options.Cookie.Name = "crud-mvc-session";
				options.Cookie.HttpOnly = true;
				options.Cookie.IsEssential = true;
			});

			services.AddDataProtection().SetApplicationName("crud-mvc")
				  .AddKeyManagementOptions(options =>
				  {
					  options.NewKeyLifetime = new TimeSpan(12, 0, 0, 0);
					  options.AutoGenerateKeys = true;
				  });

			services.AddHttpContextAccessor();
			services.AddDirectoryBrowser();
			services.AddAntiforgery(options => options.HeaderName = "X-XSRF-TOKEN");
			services.AddMvcCore();
			services.AddControllersWithViews().AddRazorRuntimeCompilation(); ;
			services.AddAutoMapper(Assembly.GetExecutingAssembly());
			
			services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
				 .AddCookie(options =>
				 {
					 options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
					 options.SlidingExpiration = true;
					 options.LoginPath = "/" + SignInConsts.Url;
				 });

     
        }

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			app.MigrateAndSeedDb();
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			//app.UseHttpsRedirection();
		
			app.UseStatusCodePages();
			app.UseStatusCodePagesWithReExecute("/error/{0}");

			var options = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
			app.UseRequestLocalization(options!.Value);

			app.UseRouting();
			app.UseHttpContext();
			app.UseDefaultFiles();
			app.UseStaticFiles(new StaticFileOptions
			{
				OnPrepareResponse = (context) =>
				{
					var headers = context.Context.Response.GetTypedHeaders();

					headers.CacheControl = new CacheControlHeaderValue
					{
						Public = true,
						MaxAge = TimeSpan.FromDays(10)
					};
				}
			});

			app.UseAuthentication();
			app.UseAuthorization();

			app.UseSession();
			app.UseMiddleware<ExceptionMiddleware>();
			app.UseMiddleware<TransactionMiddleware>();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					 name: "default",
					 pattern: "{area:exists}/{controller}/{action}/{id?}"
			   );
			});
		}
	}
}