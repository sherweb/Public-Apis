// <auto-generated>
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace Sherweb.Apis.ServiceProvider.Models
{
    using Newtonsoft.Json;
    using System.Linq;

    public partial class SubscriptionsAmendment
    {
        /// <summary>
        /// Initializes a new instance of the SubscriptionsAmendment class.
        /// </summary>
        public SubscriptionsAmendment()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the SubscriptionsAmendment class.
        /// </summary>
        public SubscriptionsAmendment(System.Guid? subscriptionsAmendmentId = default(System.Guid?))
        {
            SubscriptionsAmendmentId = subscriptionsAmendmentId;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "subscriptionsAmendmentId")]
        public System.Guid? SubscriptionsAmendmentId { get; set; }

    }
}