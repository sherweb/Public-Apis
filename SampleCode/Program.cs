using System;
using Microsoft.Rest;
using Sherweb.Apis.Authorization;
using Sherweb.Apis.Distributor;
using Sherweb.Apis.Distributor.Factory;
using Sherweb.Apis.ServiceProvider;
using Sherweb.Apis.ServiceProvider.Factory;

namespace Sherweb.SampleCode
{
    public class Program
    {
        private static string _baseUrl;

        private static string _clientId;

        private static string _clientSecret;

        private static string _subscriptionKey;

        private static string _acceptLanguageHeader;

        private static AuthorizationService _authorizationClient;

        private static IDistributorService _distributorClient;

        private static IServiceProviderService _serviceProviderClient;

        private static SubscriptionService _subscriptionService;

        private static CustomerService _customerService;

        private static DistributionService _distributionService;

        static void Main(string[] args)
        {
            _baseUrl = "https://api.sherweb.com";
            
            _clientId = "your client id";
            _clientSecret = "your client secret";
            _subscriptionKey = "your subscription key";

            // Optional. This should follow [RFC 7231, section 5.3.5: Accept-Language]: https://tools.ietf.org/html/rfc7231#section-5.3.5
            // Example: en, en-CA;q=0.8, fr-CA;q=0.7
            _acceptLanguageHeader = null;

            // Get Bearer Token from Authorization API
            _authorizationClient = new AuthorizationService(new Uri($"{_baseUrl}/auth"));

            _distributorClient = BuildDistributorClient();
            _serviceProviderClient = BuildServiceProviderClient();
            _subscriptionService = new SubscriptionService(_serviceProviderClient);
            _customerService = new CustomerService(_serviceProviderClient);
            _distributionService = new DistributionService(_distributorClient);
        }

        private static IDistributorService BuildDistributorClient()
        {
            var apiUrl = $"{_baseUrl}/distributor/v1";

            var token = _authorizationClient.TokenMethod(
                _clientId,
                _clientSecret,
                "distributor", // Scope : distributor for Distributor API
                "client_credentials");

            // Instantiate Distributor API client using Bearer token
            var credentials = new TokenCredentials(token.AccessToken, "Bearer");

            var distributorConfiguration = new DistributorServiceConfiguration
            {
                Uri = new Uri(apiUrl),
                Credentials = credentials,
                RetryCount = 0
            };

            var distributorServiceFactory = new DistributorServiceFactory(distributorConfiguration,
                new SubscriptionKeyHandler(_subscriptionKey));

            return distributorServiceFactory.Create();
        }

        private static IServiceProviderService BuildServiceProviderClient()
        {
            var apiUrl = $"{_baseUrl}/service-provider/v1";

            var token = _authorizationClient.TokenMethod(
                _clientId,
                _clientSecret,
                "service-provider",
                "client_credentials");

            var credentials = new TokenCredentials(token.AccessToken, "Bearer");

            var serviceProviderConfiguration = new ServiceProviderServiceConfiguration
            {
                Uri = new Uri(apiUrl),
                Credentials = credentials,
                RetryCount = 0
            };

            var serviceProviderServiceFactory = new ServiceProviderServiceFactory(
                serviceProviderConfiguration,
                new SubscriptionKeyHandler(_subscriptionKey));

            return serviceProviderServiceFactory.Create();
        }
    }
}
