using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Sherweb.SampleCode
{
    public class SubscriptionKeyHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            request.Headers.Add("Ocp-Apim-Subscription-Key", "your subscription key");

            return base.SendAsync(request, cancellationToken);
        }
    }
}