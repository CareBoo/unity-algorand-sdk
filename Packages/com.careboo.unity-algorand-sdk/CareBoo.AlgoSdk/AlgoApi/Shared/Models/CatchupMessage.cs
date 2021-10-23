using System;
using Unity.Collections;

namespace AlgoSdk
{
    /// <summary>
    /// A response from <see cref="IAlgodClient.StartCatchup"/>
    /// </summary>
    [AlgoApiObject]
    public struct CatchupMessage
        : IEquatable<CatchupMessage>
    {
        /// <summary>
        /// Catchup start response string
        /// </summary>
        [AlgoApiField("catchup-message", null)]
        public FixedString512Bytes Message;

        public bool Equals(CatchupMessage other)
        {
            return Message.Equals(other.Message);
        }
    }
}
