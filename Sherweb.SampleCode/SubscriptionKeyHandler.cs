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
            request.Headers.Add("Ocp-Apim-Subscription-Key", "fab21255feb049b2aabcd80b3f584885");

            return base.SendAsync(request, cancellationToken);
        }
    }
}