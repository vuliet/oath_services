using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using OpenIddict.Abstractions;
using OpenIddict.EntityFrameworkCore.Models;

namespace Auth.Host.HostedServices
{
    public class SeedDataHostedService : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<SeedDataHostedService> _logger;

        public SeedDataHostedService(IServiceProvider serviceProvider, ILogger<SeedDataHostedService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();

            var manager = scope.ServiceProvider.GetRequiredService<IOpenIddictApplicationManager>();

            // TODO: List all applications in the database
            //TODO: Count applications IAsyncEnumerable
            var applications = manager.ListAsync<List<OpenIddictEntityFrameworkCoreApplication>>(
                query: x => x.Cast<List<OpenIddictEntityFrameworkCoreApplication>>(),
                cancellationToken: cancellationToken);

            await UpsertClientApplication(manager, new OpenIddictApplicationDescriptor
            {
                ClientId = "test-client",
                ClientSecret = "123456",
                DisplayName = "Client A",
                RedirectUris =
            {
                new Uri("https://localhost:44312/oauth2-redirect.html")
            },
                Permissions =
            {
                OpenIddictConstants.Permissions.Endpoints.Authorization,
                OpenIddictConstants.Permissions.Endpoints.Token,
                OpenIddictConstants.Permissions.Endpoints.Logout,

                OpenIddictConstants.Permissions.GrantTypes.AuthorizationCode,
                OpenIddictConstants.Permissions.GrantTypes.ClientCredentials,
                OpenIddictConstants.Permissions.GrantTypes.RefreshToken,
                OpenIddictConstants.Permissions.GrantTypes.Password,

                OpenIddictConstants.Permissions.Prefixes.Scope + "openid",
                OpenIddictConstants.Permissions.Scopes.Profile,
                OpenIddictConstants.Permissions.Prefixes.Scope + "offline_access",
                OpenIddictConstants.Permissions.ResponseTypes.Code
            },
                Requirements =
            {
                OpenIddictConstants.Requirements.Features.ProofKeyForCodeExchange
            },
                Type = OpenIddictConstants.ClientTypes.Confidential,
            }, cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;

        private static async Task UpsertClientApplication(IOpenIddictApplicationManager manager, OpenIddictApplicationDescriptor openIddictApplicationDescriptor, CancellationToken cancellationToken)
        {
            var client = await manager.FindByClientIdAsync(openIddictApplicationDescriptor.ClientId, cancellationToken);

            if (client is null)
            {
                await manager.CreateAsync(openIddictApplicationDescriptor, cancellationToken);
            }
            else
            {
                await manager.UpdateAsync(client, openIddictApplicationDescriptor, cancellationToken);
            }
        }
    }
}