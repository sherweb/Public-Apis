using System;
using System.Collections.Generic;
using Sherweb.Apis.ServiceProvider;
using Sherweb.Apis.ServiceProvider.Models;

namespace Sherweb.SampleCode
{
    public class SubscriptionService
    {
        private static IServiceProviderService _serviceProviderClient;

        public SubscriptionService(IServiceProviderService serviceProviderClient)
        {
            _serviceProviderClient = serviceProviderClient;
        }

        public void GetSubscriptions(Guid customerId, string acceptLanguageHeader)
        {
            Console.WriteLine();
            Console.WriteLine("CUSTOMER SUBSCRIPTIONS");
            Subscriptions subscriptions = null;

            try
            {
                subscriptions = _serviceProviderClient.GetCustomerSubscriptions(customerId, acceptLanguage: acceptLanguageHeader);
            }
            catch (Exception exception)
            {
                // ProblemDetails returned by the API are handled and converted to a HttpOperationException in the ProblemDetailsHandler of the API Client
                // https://github.com/sherweb/Public-Apis/blob/7bd9a0ecc37f0fbe3d9085c3e911ade3ca9a0c66/NugetPackagesSourceCode/Sherweb.Apis.Distributor/DelegatingHandlers/OnProblemDetailsHandler.cs
                Console.WriteLine($"{nameof(exception.Message)}={exception.Message}");
                return;
            }

            foreach (var subscription in subscriptions.Items)
            {
                Console.WriteLine("-----------------------------");
                Console.WriteLine($"{nameof(subscription.Id)}: {subscription.Id},\n" +
                                  $"{nameof(subscription.ProductName)}: {subscription.ProductName},\n" +
                                  $"{nameof(subscription.Sku)}: {subscription.Sku},\n" +
                                  $"{nameof(subscription.Quantity)}: {subscription.Quantity}.");
            }
        }

        public void AmendSubscriptions(Guid customerId, List<SubscriptionsAmendmentParameters> body, string acceptLanguageHeader)
        {
            Console.WriteLine();
            Console.WriteLine("AMEND SUBSCRIPTION");
            SubscriptionsAmendment subscriptionsAmendment = null;

            try
            {
                subscriptionsAmendment = _serviceProviderClient.CreateSubscriptionsAmendment(customerId, body, acceptLanguage: acceptLanguageHeader);
            }
            catch (Exception exception)
            {
                // ProblemDetails returned by the API are handled and converted to a HttpOperationException in the ProblemDetailsHandler of the API Client
                // https://github.com/sherweb/Public-Apis/blob/7bd9a0ecc37f0fbe3d9085c3e911ade3ca9a0c66/NugetPackagesSourceCode/Sherweb.Apis.Distributor/DelegatingHandlers/OnProblemDetailsHandler.cs
                Console.WriteLine($"{nameof(exception.Message)}={exception.Message}");
                return;
            }

            Console.WriteLine("-----------------------------");
            Console.WriteLine($"subscriptionsAmendmentId: {subscriptionsAmendment.SubscriptionsAmendmentId}");
        }

        public void GetAmendmentStatus(Guid subscriptionsAmendmentId, string acceptLanguageHeader)
        {
            Console.WriteLine();
            Console.WriteLine("AMEND SUBSCRIPTION STATUS");
            string subscriptionsAmendmentStatus = null;

            try
            {
                subscriptionsAmendmentStatus = _serviceProviderClient.GetSubscriptionsAmendmentStatus(subscriptionsAmendmentId, acceptLanguage: acceptLanguageHeader);
            }
            catch (Exception exception)
            {
                // ProblemDetails returned by the API are handled and converted to a HttpOperationException in the ProblemDetailsHandler of the API Client
                // https://github.com/sherweb/Public-Apis/blob/7bd9a0ecc37f0fbe3d9085c3e911ade3ca9a0c66/NugetPackagesSourceCode/Sherweb.Apis.Distributor/DelegatingHandlers/OnProblemDetailsHandler.cs
                Console.WriteLine($"{nameof(exception.Message)}={exception.Message}");
                return;
            }

            Console.WriteLine("-----------------------------");
            Console.WriteLine($"status => {subscriptionsAmendmentStatus}");
        }
    }
}
