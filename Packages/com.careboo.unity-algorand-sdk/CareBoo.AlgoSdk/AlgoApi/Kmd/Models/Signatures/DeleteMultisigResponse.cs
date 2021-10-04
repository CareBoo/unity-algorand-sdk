using System;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct DeleteMultiSigResponse
        : IEquatable<DeleteMultiSigResponse>
    {
        [AlgoApiField("error", null)]
        public Optional<bool> Error;

        [AlgoApiField("message", null)]
        public string Message;

        public bool Equals(DeleteMultiSigResponse other)
        {
            return Error.Equals(other.Error)
                && StringComparer.Equals(Message, other.Message)
                ;
        }
    }
}
