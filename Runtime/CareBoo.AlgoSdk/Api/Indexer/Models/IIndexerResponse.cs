using System;

namespace AlgoSdk
{
    public interface IIndexerResponse
    {
        /// <summary>
        /// Round at which the results were computed.
        /// </summary>
        ulong CurrentRound { get; set; }
    }

    public interface IIndexerResponse<T> : IIndexerResponse
        where T : IEquatable<T>
    {
        /// <summary>
        /// The resulting data.
        /// </summary>
        T Result { get; set; }
    }
}
