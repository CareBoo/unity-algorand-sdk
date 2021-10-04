using System;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct DeleteMultisigResponse
        : IEquatable<DeleteMultisigResponse>
    {
        [AlgoApiField("error", null)]
        public Optional<bool> Error;

        [AlgoApiField("message", null)]
        public string Message;

        public bool Equals(DeleteMultisigResponse other)
        {
            return Error.Equals(other.Error)
                && StringComparer.Equals(Message, other.Message)
                ;
        }
    }
}
