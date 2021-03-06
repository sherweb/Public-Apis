using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Rest;
using Microsoft.Rest.Serialization;

namespace Sherweb.Apis.Distributor.DelegatingHandlers
{
    public class OnProblemDetailsHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            var response = await base.SendAsync(request, cancellationToken).ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
            {
                ProblemDetails problemDetails;

                try
                {
                    problemDetails = SafeJsonConvert.DeserializeObject<ProblemDetails>(response.Content.AsString());
                }
                catch
                {
                    return response;
                }

                var formattedMessage = FormatExceptionMessage(problemDetails);

                var exception = new HttpOperationException(formattedMessage)
                {
                    Request = new HttpRequestMessageWrapper(request, request.Content.AsString()),
                    Response = new HttpResponseMessageWrapper(response, response.Content.AsString())
                };

                AddDataToException(ref exception, problemDetails);

                request.Dispose();
                response.Dispose();

                throw exception;
            }

            return response;
        }

        private static void AddDataToException(ref HttpOperationException exception, ProblemDetails problemDetails)
        {
            exception.Data.Add("Type", problemDetails.Type);

            if (problemDetails.Extensions != null)
            {
                foreach (var extension in problemDetails.Extensions)
                {
                    exception.Data.Add("Extension_" + extension.Key, extension.Value);
                }
            }
        }

        private static string FormatExceptionMessage(ProblemDetails problemDetails)
        {
            return
                $"{nameof(problemDetails.Status)}={problemDetails.Status} "
                + $"{nameof(problemDetails.Title)}={problemDetails.Title} "
                + $"{nameof(problemDetails.Instance)}={problemDetails.Instance} "
                + $"{nameof(problemDetails.Detail)}={problemDetails.Detail}";
        }
    }
}