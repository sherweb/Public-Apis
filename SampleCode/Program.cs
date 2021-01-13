using System;
using System.Linq;
using Microsoft.Rest;
using Sherweb.Apis.Authorization;
using Sherweb.Apis.Distributor;
using Sherweb.Apis.Distributor.Models;

namespace Sherweb.SampleCode
{
    public class Program
    {
        static void Main(string[] args)
        {
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
            var svcClientCreds = new TokenCredentials(token.AccessToken, "Bearer");
            var distributorClient = new DistributorService(
                new Uri("https://api.sherweb.com/distributor/v1"),
                svcClientCreds,
                new SubscriptionKeyHandler(subscriptionKey));

            var response = distributorClient.GetPayableCharges(acceptLanguage: acceptLanguageHeader);
            if (response is ProblemDetails problemDetails)
            {
                Console.WriteLine($"{nameof(problemDetails.Instance)}={problemDetails.Instance}");
                Console.WriteLine($"{nameof(problemDetails.Title)}={problemDetails.Title}");
                Console.WriteLine($"{nameof(problemDetails.Status)}={problemDetails.Status}");
                Console.WriteLine($"{nameof(problemDetails.Type)}={problemDetails.Type}");
                Console.WriteLine($"{nameof(problemDetails.Detail)}={problemDetails.Detail}");

                if (problemDetails.Extensions != null)
                {
                    foreach (var extension in problemDetails.Extensions)
                    {
                        Console.WriteLine($"{nameof(extension.Key)}={extension.Key}");
                        Console.WriteLine($"{nameof(extension.Value)}={extension.Value}");
                        Console.WriteLine("-------------------------------------------------");
                    }
                }

                return;
            }

            var payableCharges = (PayableCharges) response;

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