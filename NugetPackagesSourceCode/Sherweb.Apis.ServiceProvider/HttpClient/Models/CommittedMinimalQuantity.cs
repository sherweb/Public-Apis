// <auto-generated>
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace Sherweb.Apis.ServiceProvider.Models
{
    using Microsoft.Rest;
    using Microsoft.Rest.Serialization;
    using Newtonsoft.Json;
    using System.Linq;

    /// <summary>
    /// The date until which the subscription quantity may be decreased down to
    /// this minimal quantity
    /// </summary>
    public partial class CommittedMinimalQuantity
    {
        /// <summary>
        /// Initializes a new instance of the CommittedMinimalQuantity class.
        /// </summary>
        public CommittedMinimalQuantity()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the CommittedMinimalQuantity class.
        /// </summary>
        /// <param name="committedUntil">Format: yyyy-MM-ddTHH:mm:ss.fffffffK
        /// (UTC). Example : 2024-11-21T20:27:05.7613888</param>
        public CommittedMinimalQuantity(System.DateTime committedUntil, int quantity)
        {
            CommittedUntil = committedUntil;
            Quantity = quantity;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// Gets or sets format: yyyy-MM-ddTHH:mm:ss.fffffffK (UTC). Example :
        /// 2024-11-21T20:27:05.7613888
        /// </summary>
        [JsonConverter(typeof(DateJsonConverter))]
        [JsonProperty(PropertyName = "committedUntil")]
        public System.DateTime CommittedUntil { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "quantity")]
        public int Quantity { get; set; }

        /// <summary>
        /// Validate the object.
        /// </summary>
        /// <exception cref="ValidationException">
        /// Thrown if validation fails
        /// </exception>
        public virtual void Validate()
        {
            //Nothing to validate
        }
    }
}
