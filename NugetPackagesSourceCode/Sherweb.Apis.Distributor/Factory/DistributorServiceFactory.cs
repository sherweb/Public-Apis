using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Microsoft.Rest.TransientFaultHandling;
using Sherweb.Apis.Distributor.DelegatingHandlers;

namespace Sherweb.Apis.Distributor.Factory
{
    public class DistributorServiceFactory : IDistributorServiceFactory
    {
        private readonly DistributorServiceConfiguration _configuration;

        private readonly List<DelegatingHandler>_delegatingHandlers;

        public DistributorServiceFactory(
            DistributorServiceConfiguration configuration,
            params DelegatingHandler[] delegatingHandlers)
        {
            this._configuration = configuration;
            this._delegatingHandlers = delegatingHandlers.ToList();
        }

        public IDistributorService Create()
        {
            this._delegatingHandlers.Add(new OnProblemDetailsHandler());

            var client = new DistributorService(
                this._configuration.Uri,
                this._configuration.Credentials,
                this._delegatingHandlers.ToArray());

            client.SetRetryPolicy(new RetryPolicy(new HttpStatusCodeErrorDetectionStrategy(), this._configuration.RetryCount));

            return client;
        }
    }
}