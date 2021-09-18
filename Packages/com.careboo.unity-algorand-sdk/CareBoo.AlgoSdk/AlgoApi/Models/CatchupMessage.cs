using System;
using Unity.Collections;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct CatchupMessage
        : IEquatable<CatchupMessage>
    {
        [AlgoApiKey("catchup-message")]
        public FixedString512Bytes Message;

        public bool Equals(CatchupMessage other)
        {
            return Message.Equals(other.Message);
        }
    }
}
