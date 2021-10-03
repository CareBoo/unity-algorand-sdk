using System;=

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct APIV1DELETEKeyResponse
        : IEquatable<APIV1DELETEKeyResponse>
    {
        [AlgoApiField("error", null)]
        public Optional<bool> Error;

        [AlgoApiField("message", null)]
        public string Message;

        public bool Equals(APIV1DELETEKeyResponse other)
        {
            return Error.Equals(other.Error)
                && StringComparer.Equals(Message, other.Message)
                ;
        }
    }
}
