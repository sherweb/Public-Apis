using System;
using Microsoft.Rest;

namespace Sherweb.Apis.ServiceProvider.Factory
{
    public class ServiceProviderServiceConfiguration
    {
        public Uri Uri { get; set; }

        public TokenCredentials Credentials { get; set; }

        public int RetryCount { get; set; }
    }
}
