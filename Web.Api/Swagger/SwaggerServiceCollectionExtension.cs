using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.IO;
using System.Reflection;

namespace Web.Api.Swagger
{
    public static class SwaggerServiceCollectionExtension
    {
        public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
        {
            return services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. " +
                                  "\r\n\r\nEnter your token in the text input below. "
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                          {
                              Reference = new OpenApiReference
                              {
                                  Type = ReferenceType.SecurityScheme,
                                  Id = "Bearer"
                              }
                          },
                         new string[] {}
                    }
                });

                options.SwaggerDoc(
                    "v1",
                    new OpenApiInfo
                    {
                        Title = "crud web",
                        Version = "v1",
                        Description = "Demo API for crud-web using JWT authentication",
                        Contact = new OpenApiContact
                        {
                            Name = "Rafał Kadow",
                            Email = "rafal.kadow@gmail.com"
                        }
                    });
                options.ExampleFilters();
                // Include docs from current API assembly (as described in MS Docs)
                var executingAssembly = Assembly.GetExecutingAssembly();
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{executingAssembly.GetName().Name}.xml"), true);



                //var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                //var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                //options.IncludeXmlComments(xmlPath, true);
                // Additionally include the documentation of all other "relevant" projects
                var referencedProjectsXmlDocPaths = executingAssembly.GetReferencedAssemblies()
                    .Where(assembly => assembly.Name != null && assembly.Name.StartsWith("Domain", StringComparison.InvariantCultureIgnoreCase))
                    .Select(assembly => Path.Combine(AppContext.BaseDirectory, $"{assembly.Name}.xml"))
                    .Where(path => File.Exists(path));
                foreach (var xmlDocPath in referencedProjectsXmlDocPaths)
                {
                    options.IncludeXmlComments(xmlDocPath, true);
                }
                options.EnableAnnotations();


                options.OperationFilter<AddResponseHeadersFilter>();
            });
            
        }
    }
}