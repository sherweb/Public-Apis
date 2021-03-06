// <auto-generated>
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace Sherweb.Apis.Authorization
{
    using Models;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Extension methods for AuthorizationService.
    /// </summary>
    public static partial class AuthorizationServiceExtensions
    {
            /// <summary>
            /// Token
            /// </summary>
            /// <remarks>
            /// Authenticate and get bearer token.
            /// </remarks>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='clientId'>
            /// The registered client ID.
            /// </param>
            /// <param name='clientSecret'>
            /// The registered client ID secret.
            /// </param>
            /// <param name='scope'>
            /// The scope of the token you want.
            /// </param>
            /// <param name='grantType'>
            /// The grant_type you want.
            /// </param>
            public static Token TokenMethod(this IAuthorizationService operations, string clientId, string clientSecret, string scope, string grantType)
            {
                return operations.TokenMethodAsync(clientId, clientSecret, scope, grantType).GetAwaiter().GetResult();
            }

            /// <summary>
            /// Token
            /// </summary>
            /// <remarks>
            /// Authenticate and get bearer token.
            /// </remarks>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='clientId'>
            /// The registered client ID.
            /// </param>
            /// <param name='clientSecret'>
            /// The registered client ID secret.
            /// </param>
            /// <param name='scope'>
            /// The scope of the token you want.
            /// </param>
            /// <param name='grantType'>
            /// The grant_type you want.
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<Token> TokenMethodAsync(this IAuthorizationService operations, string clientId, string clientSecret, string scope, string grantType, CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.TokenMethodWithHttpMessagesAsync(clientId, clientSecret, scope, grantType, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

    }
}
