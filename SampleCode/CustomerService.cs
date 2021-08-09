using System;

using Sherweb.Apis.ServiceProvider;
using Sherweb.Apis.ServiceProvider.Models;

namespace Sherweb.SampleCode
{
    public class CustomerService
    {
        private static IServiceProviderService _serviceProviderClient;

        public CustomerService(IServiceProviderService serviceProviderClient)
        {
            _serviceProviderClient = serviceProviderClient;
        }

        public void ShowCustomers(string acceptLanguageHeader)
        {
            Console.WriteLine();
            Console.WriteLine("CUSTOMERS");
            Customers customers = null;

            try
            {
                customers = _serviceProviderClient.GetCustomers(acceptLanguageHeader);
            }
            catch (Exception exception)
            {
                // ProblemDetails returned by the API are handled and converted to a HttpOperationException in the ProblemDetailsHandler of the API Client
                // https://github.com/sherweb/Public-Apis/blob/7bd9a0ecc37f0fbe3d9085c3e911ade3ca9a0c66/NugetPackagesSourceCode/Sherweb.Apis.Distributor/DelegatingHandlers/OnProblemDetailsHandler.cs
                Console.WriteLine($"{nameof(exception.Message)}={exception.Message}");
                return;
            }

            foreach (var customer in customers.Items)
            {
                Console.WriteLine("-----------------------------");
                Console.WriteLine($"{nameof(customer.DisplayName)} => {customer.DisplayName}");
            }
        }
    }
}