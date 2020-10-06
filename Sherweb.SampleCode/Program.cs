using System;
using Microsoft.Rest;
using Sherweb.Apis.Authorization;
using Sherweb.Apis.Distributor;

namespace Sherweb.SampleCode
{
    public class Program
    {
        static void Main(string[] args)
        {
            //// Get Bearer Token from Authorization API
            var authorizationClient = new AuthorizationService(new Uri("https://api-testing.sherweb.com/auth"));

            var token = authorizationClient.TokenMethod(
                "5d194860-62e5-405e-85a2-3c2d451e4a3a", // ClientId
                "qXsCDWHNzXhrUS3I/u4opco7DBgT8fiMaceIzKb7", // ClientSecret
                "distributor"); // Scope : distributor for Distribution API

            //// Instantiate Distribution API client using Bearer token
            var svcClientCreds = new TokenCredentials(token.AccessToken, "Bearer");
            var distributorClient = new DistributorService(
                new Uri("https://api-testing.sherweb.com/distribution/v1"),
                svcClientCreds,
                new SubscriptionKeyHandler());

            var accountPayable = distributorClient.GetAccountsPayable();
        }
    }
}