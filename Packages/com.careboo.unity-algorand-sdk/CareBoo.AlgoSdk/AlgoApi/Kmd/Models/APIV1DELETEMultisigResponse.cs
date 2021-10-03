using System;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct APIV1DELETEMultisigResponse
        : IEquatable<APIV1DELETEMultisigResponse>
    {
        [AlgoApiField("error", null)]
        public Optional<bool> Error;

        [AlgoApiField("message", null)]
        public string Message;

        public bool Equals(APIV1DELETEMultisigResponse other)
        {
            return Error.Equals(other.Error)
                && StringComparer.Equals(Message, other.Message)
                ;
        }
    }
}
