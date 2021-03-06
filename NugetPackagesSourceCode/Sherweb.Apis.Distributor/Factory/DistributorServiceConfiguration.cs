using System;
using Microsoft.Rest;

namespace Sherweb.Apis.Distributor.Factory
{
    public class DistributorServiceConfiguration
    {
        public Uri Uri { get; set; }

        public TokenCredentials Credentials { get; set; }

        public int RetryCount { get; set; }
    }
}