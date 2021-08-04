using System;
using System.Linq;
using Microsoft.Rest;
using Sherweb.Apis.Distributor;
using Sherweb.Apis.Distributor.Models;
using Sherweb.Apis.ServiceProvider;

namespace Sherweb.SampleCode
{
    public class DistributionService
    {
        private static IDistributorService _distributorClient;

        public DistributionService(IDistributorService distributorService)
        {
            _distributorClient = distributorService;
        }

        public void ShowPayableCharges(string acceptLanguageHeader)
        {
            Console.WriteLine();
            Console.WriteLine("PAYABLE CHARGES");
            PayableCharges payableCharges = null;

            try
            {
                payableCharges = _distributorClient.GetPayableCharges(acceptLanguage: acceptLanguageHeader);
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
    }
}
