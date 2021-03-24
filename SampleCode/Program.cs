using System;
using System.Linq;
using Microsoft.Rest;
using Sherweb.Apis.Authorization;
using Sherweb.Apis.Distributor;
using Sherweb.Apis.Distributor.Factory;
using Sherweb.Apis.Distributor.Models;

namespace Sherweb.SampleCode
{
    public class Program
    {
        static void Main(string[] args)
        {
            const string baseUrl = "https://api.sherweb.com/distributor/v1";
            const string clientId = "your client id";
            const string clientSecret = "your client secret";
            const string subscriptionKey = "your subscription key";

            // Optional. This should follow [RFC 7231, section 5.3.5: Accept-Language]: https://tools.ietf.org/html/rfc7231#section-5.3.5
            // Example: en, en-CA;q=0.8, fr-CA;q=0.7
            const string acceptLanguageHeader = null;

            // Get Bearer Token from Authorization API
            var authorizationClient = new AuthorizationService(new Uri("https://api.sherweb.com/auth"));

            var token = authorizationClient.TokenMethod(
                clientId,
                clientSecret,
                "distributor", // Scope : distributor for Distributor API
                "client_credentials");

            // Instantiate Distributor API client using Bearer token
            var credentials = new TokenCredentials(token.AccessToken, "Bearer");

            var distributorConfiguration = new DistributorServiceConfiguration
            {
                Uri = new Uri(baseUrl),
                Credentials = credentials,
                RetryCount = 0
            };

            var distributorServiceFactory = new DistributorServiceFactory(distributorConfiguration,
                new SubscriptionKeyHandler(subscriptionKey));

            var distributorClient = distributorServiceFactory.Create();
            
            PayableCharges payableCharges = null;
            
            try
            {
                payableCharges = distributorClient.GetPayableCharges(acceptLanguage: acceptLanguageHeader);
            }
            catch (HttpOperationException exception)
            {
                // ProblemDetails returned by the API are handled and converted to a HttpOperationException in the ProblemDetailsHandler of the API Client
                // https://github.com/sherweb/Public-Apis/blob/7bd9a0ecc37f0fbe3d9085c3e911ade3ca9a0c66/NugetPackagesSourceCode/Sherweb.Apis.Distributor/DelegatingHandlers/OnProblemDetailsHandler.cs
                Console.WriteLine($"{nameof(exception.Message)}={exception.Message}");
                return;
            }
            
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
    }
}