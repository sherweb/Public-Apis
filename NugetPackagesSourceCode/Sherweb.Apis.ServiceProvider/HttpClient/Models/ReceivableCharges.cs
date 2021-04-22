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
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// All charges to pay for a billing period.
    /// </summary>
    public partial class ReceivableCharges
    {
        /// <summary>
        /// Initializes a new instance of the ReceivableCharges class.
        /// </summary>
        public ReceivableCharges()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the ReceivableCharges class.
        /// </summary>
        public ReceivableCharges(System.DateTime? periodFrom = default(System.DateTime?), System.DateTime? periodTo = default(System.DateTime?), IList<Charge> charges = default(IList<Charge>))
        {
            PeriodFrom = periodFrom;
            PeriodTo = periodTo;
            Charges = charges;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// </summary>
        [JsonConverter(typeof(DateJsonConverter))]
        [JsonProperty(PropertyName = "periodFrom")]
        public System.DateTime? PeriodFrom { get; set; }

        /// <summary>
        /// </summary>
        [JsonConverter(typeof(DateJsonConverter))]
        [JsonProperty(PropertyName = "periodTo")]
        public System.DateTime? PeriodTo { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "charges")]
        public IList<Charge> Charges { get; set; }

    }
}
