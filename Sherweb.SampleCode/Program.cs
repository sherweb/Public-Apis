using System;
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
            const string clientId = "your clientId";
            const string clientSecret = "your client secret";

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
                new SubscriptionKeyHandler());

            var response = distributorClient.GetPayableCharges();
            if (response is ProblemDetails problemDetails)
            {
                // Handle error
                return;
            }

            var payableCharges = (PayableCharges) response;

            Console.WriteLine($"{nameof(payableCharges.PeriodFrom)}={payableCharges.PeriodFrom}");
            Console.WriteLine($"{nameof(payableCharges.PeriodTo)}={payableCharges.PeriodTo}");

            foreach (var charge in payableCharges.Charges)
            {
                Console.WriteLine("-------------------------------------------------");
                Console.WriteLine($"{nameof(charge.ProductName)}={charge.ProductName}");
                Console.WriteLine($"{nameof(charge.ChargeName)}={charge.ChargeName}");
                Console.WriteLine($"{nameof(charge.Quantity)}={charge.Quantity}");
                Console.WriteLine($"{nameof(charge.SubTotal)}={charge.SubTotal}");
            }
        }
    }
}