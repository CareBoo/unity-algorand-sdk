using Unity.Collections;

namespace AlgoSdk
{
    public interface IIndexerResponse
    {
        /// <summary>
        /// Round at which the results were computed.
        /// </summary>
        ulong CurrentRound { get; }
    }

    public interface IPaginatedResponse : IIndexerResponse
    {

        /// <summary>
        /// Used for pagination, when making another request provide this token with the
        /// next parameter.
        /// </summary>
        FixedString128Bytes NextToken { get; }
    }
}
