using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OpenIddict.Abstractions;
using OpenIddict.Core;
using OpenIddict.EntityFrameworkCore.Models;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace Auth.Infrastructure.ExternalServices
{

    //TODO: Có cần inject cái này vào service không? Inject như thế nào?
    public class OpenIddictManagerProvider : OpenIddictApplicationManager<OpenIddictEntityFrameworkCoreApplication>
    {
        private readonly IServiceProvider _serviceProvider;

        public OpenIddictManagerProvider(IOpenIddictApplicationCache<OpenIddictEntityFrameworkCoreApplication> cache, ILogger<OpenIddictApplicationManager<OpenIddictEntityFrameworkCoreApplication>> logger, IOptionsMonitor<OpenIddictCoreOptions> options, IOpenIddictApplicationStoreResolver resolver, IServiceProvider serviceProvider) : base(cache, logger, options, resolver)
        {
            _serviceProvider = serviceProvider;
        }

        protected override ValueTask<string> ObfuscateClientSecretAsync(string secret, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(secret))
            {
                throw new ArgumentNullException(nameof(secret));
            }

            return new ValueTask<string>(secret);
        }

        // Override ValidateClientSecretAsync method
        public override async ValueTask<bool> ValidateClientSecretAsync(
            OpenIddictEntityFrameworkCoreApplication application,
            string secret,
            CancellationToken cancellationToken = default)
        {
            if (application is null)
            {
                throw new ArgumentNullException(nameof(application));
            }
            if (string.IsNullOrEmpty(secret))
            {
                throw new ArgumentException(nameof(secret));
            }

            if (await HasClientTypeAsync(application, ClientTypes.Public, cancellationToken))
            {
                Logger.LogWarning("Dont have client type");

                return false;
            }

            var value = await Store.GetClientSecretAsync(application, cancellationToken);
            if (string.IsNullOrEmpty(value))
            {
                Logger.LogError("Dont have client secret", await GetClientIdAsync(application, cancellationToken));

                return false;
            }

            if (value != secret)
            {
                Logger.LogError("Dont have client secret", await GetClientIdAsync(application, cancellationToken));
                return false;
            }

            return true;
        }
    }
}