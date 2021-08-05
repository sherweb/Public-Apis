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

            Subscription = 2,

            Customer=3
        }

        public enum SubscriptionOption
        {
           GetSubscriptions = 1,

            GetSubscriptionsAmendmentStatus = 2
        }

        public enum CustomerOption
        {
            GetCustomers = 1
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