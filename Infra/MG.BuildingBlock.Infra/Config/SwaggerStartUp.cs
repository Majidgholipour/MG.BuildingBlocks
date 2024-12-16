using Garnet.Standard.Pagination;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Interfaces;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Resources;

namespace MG.BuildingBlock.Infra.Config
{
    public static class SwaggerStartUp
    {
        public static IServiceCollection AddSwaggerModule(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Project Swagger",
                    Version = "v1",
                    Description = Resources.ConstantTexts.SwaggerDescription,
                    Contact = new OpenApiContact
                    {
                        Name = "Majid Gholipour",
                        Email = "m.gholipour.official@gmail.com",
                        Extensions = new Dictionary<string, IOpenApiExtension>
                    {
                        { "x-company", new OpenApiString("Company Name") },
                        { "x-contact", new OpenApiString("contact@email.com") }
                    }
                    },

                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please insert JWT into field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "bearer"
                });

                /*c.DocInclusionPredicate((version, apiDesc) =>
                {
                    var apiVersionAttributes = apiDesc.ActionDescriptor.EndpointMetadata.OfType<ApiVersionAttribute>();

                    if (apiVersionAttributes.Any())
                    {
                        return apiDesc.GroupName == version;
                    }

                    return true;
                });*/

                // c.OperationFilter<SecurityRequirementsOperationFilter>();

                c.OperationFilter<AddAcceptLanguageHeaderParameter>();

                // c.ExampleFilters();

                //c.EnableAnnotations();
            });

            return services;
        }

        //public static IServiceCollection AddSwaggerModule(this IServiceCollection @this,IConfiguration configuration)
        //{
        //    @this.AddSwaggerGen(options =>
        //    {
        //        options.CustomSchemaIds(type => $"{type.GetHashCode()}__{type.Name}");
        //        options.OperationFilter<IPageableOperationFilter>();
                
        //        //options.DescribeAllEnumsAsStrings();
        //        //options.SwaggerDoc("v1", new OpenApiInfo
        //        //{
        //        //    Title = "Host -  HTTP API",
        //        //    Version = "v1",
        //        //    Description = "The Host Service HTTP API"
        //        //});
        //        var jwtSecurityScheme = new OpenApiSecurityScheme
        //        {
        //            Scheme = "bearer",
        //            BearerFormat = "JWT",
        //            Name = "JWT Authentication",
        //            In = ParameterLocation.Header,
        //            Type = SecuritySchemeType.Http,
        //            Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",

        //            Reference = new OpenApiReference
        //            {
        //                Id = JwtBearerDefaults.AuthenticationScheme,
        //                Type = ReferenceType.SecurityScheme
        //            }
        //        };

        //        options.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

        //        options.AddSecurityRequirement(new OpenApiSecurityRequirement
        //            {
        //                { jwtSecurityScheme, Array.Empty<string>() }
        //            });



        //    });
        //    @this.AddSwaggerGenNewtonsoftSupport();
        //    return @this;

        //}
        public static IApplicationBuilder UsingSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
                //options.SwaggerEndpoint("/swagger/v2/swagger.json", "API V2");
            });

            return app;
        }
    }

    internal class IPageableOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (context.ApiDescription.ParameterDescriptions.All(description =>
                description.ParameterDescriptor.ParameterType != typeof(IPagination)))
                return;

            var namesToRemove = context.ApiDescription.ParameterDescriptions
                .Where(description => description.ParameterDescriptor.ParameterType == typeof(IPagination)).ToList()
                .Select(description => description.Name)
                .Where(name => !new List<string>
                {
                    nameof(IPagination.PageNumber),
                    nameof(IPagination.PageSize),
                    nameof(IPagination.Filters),
                    nameof(IPagination.Orders)
                }.Contains(name));

            operation.Parameters = operation.Parameters.Where(parameter => !namesToRemove.Contains(parameter.Name))
                .ToList();

            var dic = new Dictionary<string, string>
            {
                [nameof(IPagination.PageNumber)] = "page",
                [nameof(IPagination.PageSize)] = "size",
                [nameof(IPagination.Filters)] = "filter",
                [nameof(IPagination.Orders)] = "order",
            };

            foreach (var openApiParameter in operation.Parameters)
                if (dic.ContainsKey(openApiParameter.Name))
                    openApiParameter.Name = dic[openApiParameter.Name];
        }
    }
}
