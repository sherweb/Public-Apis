// <auto-generated>
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace Sherweb.Apis.Distributor
{
    using Models;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Extension methods for DistributorService.
    /// </summary>
    public static partial class DistributorServiceExtensions
    {
            /// <summary>
            /// GetAccountsPayable
            /// </summary>
            /// <remarks>
            /// Get your accounts payable data for a specific billing period.
            /// </remarks>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='date'>
            /// Specify a date within the desired billing period. Default: Today.
            /// </param>
            public static AccountsPayable GetAccountsPayable(this IDistributorService operations, System.DateTime? date = default(System.DateTime?))
            {
                return operations.GetAccountsPayableAsync(date).GetAwaiter().GetResult();
            }

            /// <summary>
            /// GetAccountsPayable
            /// </summary>
            /// <remarks>
            /// Get your accounts payable data for a specific billing period.
            /// </remarks>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='date'>
            /// Specify a date within the desired billing period. Default: Today.
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<AccountsPayable> GetAccountsPayableAsync(this IDistributorService operations, System.DateTime? date = default(System.DateTime?), CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.GetAccountsPayableWithHttpMessagesAsync(date, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

    }
}
