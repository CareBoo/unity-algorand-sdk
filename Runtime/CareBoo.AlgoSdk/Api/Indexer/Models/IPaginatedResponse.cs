using System;
using Unity.Collections;

namespace AlgoSdk
{
    public interface IPaginatedResponse : IIndexerResponse
    {
        /// <summary>
        /// Used for pagination, when making another request provide this token with the
        /// next parameter.
        /// </summary>
        FixedString128Bytes NextToken { get; set; }
    }

    public interface IPaginatedIndexerResponse<T> : IPaginatedResponse
        where T : IEquatable<T>
    {
        /// <summary>
        /// The resulting data.
        /// </summary>
        T[] Results { get; set; }
    }
}
