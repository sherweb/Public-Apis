using System;


namespace Sherweb.SampleCode
{
    public class MainMenu
    {
        public enum DistributorOption
        {
            GetPayableCharges = 1
        }

        public enum MenuOptions
        {
            Distributor = 1,

            ServiceProvider = 2
        }

        public enum ServiceProviderOption
        {
            GetCustomers = 1,

            GetSubscriptions = 2,

            GetSubscriptionsAmendmentStatus = 3
        }

        public void PrintMenu(Enum e)
        {
            Console.WriteLine("Choose option:");
            foreach (var enumValue in Enum.GetValues(e.GetType()))
            {
                Console.WriteLine((int)enumValue + " - " + enumValue);
            }

            Console.WriteLine(e.GetType().Name == nameof(MainMenu) ? "0 - Exit" : "0 - Go back");
            Console.WriteLine("Select an option: ");
        }
    }
}