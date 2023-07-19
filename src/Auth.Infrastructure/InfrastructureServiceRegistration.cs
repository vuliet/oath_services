using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Auth.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.AspNetCore.Identity;
using Auth.Domain.Entities;
using static OpenIddict.Abstractions.OpenIddictConstants;
using Auth.Infrastructure.ExternalServices;
using OpenIddict.Abstractions;

namespace Auth.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AuthContext>(options =>
            {
                options.UseMySQL(configuration.GetConnectionString("AuthConnectionString"));
            });


            services.AddIdentity<User, IdentityRole>()
                    .AddEntityFrameworkStores<AuthContext>();
            //.AddDefaultTokenProviders();

            // services.AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>));
            services.AddScoped<IUserRepository, UserRepository>();
            // services.AddScoped<IDbContextTransaction>(_ => null);


            services.AddOpenIddict()
                    .AddCore(options =>
                    {
                        options.UseEntityFrameworkCore()
                                .UseDbContext<AuthContext>();

                        options.ReplaceApplicationManager(typeof(OpenIddictManagerProvider));
                    })
                    .AddServer(options =>
                    {
                        // Enable the authorization, logout, token and userinfo endpoints.
                        options
                            .SetTokenEndpointUris("connect/token")
                            .SetAuthorizationEndpointUris("connect/authorize")
                            .SetLogoutEndpointUris("connect/logout")
                            .SetUserinfoEndpointUris("connect/userinfo");

                        options.AllowAuthorizationCodeFlow()
                                .AllowHybridFlow()
                                .AllowClientCredentialsFlow()
                                .AllowPasswordFlow()
                                .AllowRefreshTokenFlow();

                        // Custom auth flows are also supported
                        //options.AllowCustomFlow("custom_flow_name");

                        options.RegisterScopes(Scopes.OpenId, Scopes.Profile, Scopes.OfflineAccess);

                        // options.AddEncryptionCertificate(AppSettings.IdentityServer.EncryptionCertificate.FindCertificate())
                        //        .AddSigningCertificate(AppSettings.IdentityServer.SigningCertificate.FindCertificate());

                        //TODO: Add Certificate for dev OpenIddict

                        options.AddEphemeralEncryptionKey()
                                 .AddEphemeralSigningKey();

                        options.DisableAccessTokenEncryption();

                        options
                            .UseAspNetCore()
                            .EnableTokenEndpointPassthrough()
                            .EnableAuthorizationEndpointPassthrough()
                            .EnableLogoutEndpointPassthrough()
                            .EnableUserinfoEndpointPassthrough();

                        options.UseAspNetCore().DisableTransportSecurityRequirement();
                    })
                    .AddValidation(options =>
                    {
                        options.UseLocalServer();
                        options.UseAspNetCore();
                    });

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = OpenIddictConstants.Schemes.Bearer;
                options.DefaultChallengeScheme = OpenIddictConstants.Schemes.Bearer;
            });

            return services;
        }
    }
}
