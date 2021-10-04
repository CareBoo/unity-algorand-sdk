using System;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct DeleteKeyResponse
        : IEquatable<DeleteKeyResponse>
    {
        [AlgoApiField("error", null)]
        public Optional<bool> Error;

        [AlgoApiField("message", null)]
        public string Message;

        public bool Equals(DeleteKeyResponse other)
        {
            return Error.Equals(other.Error)
                && StringComparer.Equals(Message, other.Message)
                ;
        }
    }
}
