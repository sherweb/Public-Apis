using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Sherweb.SampleCode
{
    public class SubscriptionKeyHandler : DelegatingHandler
    {
        private string SubscriptionKey { get; set; }

        public SubscriptionKeyHandler(string subscriptionKey)
        {
            SubscriptionKey = subscriptionKey;
        }

        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            request.Headers.Add("Ocp-Apim-Subscription-Key", this.SubscriptionKey);

            return base.SendAsync(request, cancellationToken);
        }
    }
}