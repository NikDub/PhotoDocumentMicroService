using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Azure;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PhotoDocumentMicroService.Application.Service;
using PhotoDocumentMicroService.Application.Service.Abstractions;
using PhotoDocumentMicroService.Infrastructure.Repository;
using PhotoDocumentMicroService.Infrastructure.Repository.Abstractions;

namespace PhotoDocumentMicroService.Extensions;

public static class ServiceExtensions
{
    public static void ConfigureJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.Authority = configuration.GetValue<string>("Routes:AuthorityRoute") ??
                                    throw new NotImplementedException();
                options.Audience = configuration.GetValue<string>("Routes:Scopes") ??
                                   throw new NotImplementedException();
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = true,
                    ValidAudience = configuration.GetValue<string>("Routes:Scopes") ??
                                    throw new NotImplementedException(),
                    ValidateIssuer = true,
                    ValidIssuer = configuration.GetValue<string>("Routes:AuthorityRoute") ??
                                  throw new NotImplementedException(),
                    ValidateLifetime = true
                };
            });
    }

    public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(typeof(MappingProfile));
        services.AddAzureClients(clientBuilder =>
        {
            clientBuilder.AddBlobServiceClient(configuration.GetValue<string>("StorageConnectionString:blob") ??
                                               throw new NotImplementedException());
            clientBuilder.AddTableServiceClient(configuration.GetValue<string>("StorageConnectionString:blob") ??
                                               throw new NotImplementedException());
            clientBuilder.AddQueueServiceClient(configuration.GetValue<string>("StorageConnectionString:queue") ??
                                                throw new NotImplementedException());
        });
        services.AddScoped<IDocumentService, DocumentService>();
        services.AddScoped<IBlobRepository, BlobRepository>();
        services.AddScoped<IDocumentRepository, DocumentRepository>();
    }

    public static void ConfigureSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(setup =>
        {
            setup.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Place to add JWT with Bearer",
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });

            setup.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Name = "Bearer"
                    },
                    new List<string>()
                }
            });
        });
    }
}