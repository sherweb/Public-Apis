using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Rest;
using Sherweb.Apis.Authorization;
using Sherweb.Apis.Distributor;
using Sherweb.Apis.Distributor.Factory;
using Sherweb.Apis.Distributor.Models;
using Sherweb.Apis.ServiceProvider;
using Sherweb.Apis.ServiceProvider.Factory;
using Sherweb.Apis.ServiceProvider.Models;

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

            ShowCustomers();
            ShowPayableCharges();
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

        private static void ShowPayableCharges()
        {
            Console.WriteLine();
            Console.WriteLine("PAYABLE CHARGES");
            PayableCharges payableCharges = null;

            try
            {
                payableCharges = _distributorClient.GetPayableCharges(acceptLanguage: _acceptLanguageHeader);
            }
            catch (HttpOperationException exception)
            {
                // ProblemDetails returned by the API are handled and converted to a HttpOperationException in the ProblemDetailsHandler of the API Client
                // https://github.com/sherweb/Public-Apis/blob/7bd9a0ecc37f0fbe3d9085c3e911ade3ca9a0c66/NugetPackagesSourceCode/Sherweb.Apis.Distributor/DelegatingHandlers/OnProblemDetailsHandler.cs
                Console.WriteLine($"{nameof(exception.Message)}={exception.Message}");
                return;
            }

            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine($"{nameof(payableCharges.PeriodFrom)}={payableCharges.PeriodFrom}");
            Console.WriteLine($"{nameof(payableCharges.PeriodTo)}={payableCharges.PeriodTo}");

            foreach (var charge in payableCharges.Charges)
            {
                var customerDisplayName = charge.Tags.SingleOrDefault(x => x.Name == "CustomerDisplayName")?.Value;
                Console.WriteLine("-------------------------------------------------");
                Console.WriteLine($"{nameof(customerDisplayName)}={customerDisplayName}");
                Console.WriteLine($"{nameof(charge.ProductName)}={charge.ProductName}");
                Console.WriteLine($"{nameof(charge.ChargeName)}={charge.ChargeName}");
                Console.WriteLine($"{nameof(charge.Quantity)}={charge.Quantity}");
                Console.WriteLine($"{nameof(charge.SubTotal)}={charge.SubTotal}");
            }
        }

        private static void ShowCustomers()
        {
            Console.WriteLine();
            Console.WriteLine("CUSTOMERS");
            IList<Customer> customers = null; 

            try
            {
                 customers = _serviceProviderClient.GetCustomers(acceptLanguage: _acceptLanguageHeader);
            }
            catch (Exception exception)
            {
                // ProblemDetails returned by the API are handled and converted to a HttpOperationException in the ProblemDetailsHandler of the API Client
                // https://github.com/sherweb/Public-Apis/blob/7bd9a0ecc37f0fbe3d9085c3e911ade3ca9a0c66/NugetPackagesSourceCode/Sherweb.Apis.Distributor/DelegatingHandlers/OnProblemDetailsHandler.cs
                Console.WriteLine($"{nameof(exception.Message)}={exception.Message}");
                return;
            }

            foreach (var customer in customers)
            {
                Console.WriteLine("-----------------------------");
                Console.WriteLine($"{nameof(customer.DisplayName)} => {customer.DisplayName}");
            }
        }
    }
}