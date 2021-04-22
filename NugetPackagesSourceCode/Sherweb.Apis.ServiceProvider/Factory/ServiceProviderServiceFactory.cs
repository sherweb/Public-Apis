using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Microsoft.Rest.TransientFaultHandling;
using Sherweb.Apis.ServiceProvider.DelegatingHandlers;

namespace Sherweb.Apis.ServiceProvider.Factory
{
    public class ServiceProviderServiceFactory : IServiceProviderServiceFactory
    {
        private readonly ServiceProviderServiceConfiguration _configuration;

        private readonly List<DelegatingHandler> _delegatingHandlers;

        public ServiceProviderServiceFactory(
            ServiceProviderServiceConfiguration configuration,
            params DelegatingHandler[] delegatingHandlers)
        {
            this._configuration = configuration;
            this._delegatingHandlers = delegatingHandlers.ToList();
        }

        public IServiceProviderService Create()
        {
            this._delegatingHandlers.Add(new OnProblemDetailsHandler());

            var client = new ServiceProviderService(
                this._configuration.Uri,
                this._configuration.Credentials,
                this._delegatingHandlers.ToArray());

            client.SetRetryPolicy(new RetryPolicy(new HttpStatusCodeErrorDetectionStrategy(), this._configuration.RetryCount));

            return client;
        }
    }
}
