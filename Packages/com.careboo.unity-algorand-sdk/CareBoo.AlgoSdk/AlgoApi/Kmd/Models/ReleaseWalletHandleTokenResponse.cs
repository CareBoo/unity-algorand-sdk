using System;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct ReleaseWalletHandleTokenResponse
        : IEquatable<ReleaseWalletHandleTokenResponse>
    {
        [AlgoApiField("error", null)]
        public Optional<bool> Error;

        [AlgoApiField("message", null)]
        public string Message;

        public bool Equals(ReleaseWalletHandleTokenResponse other)
        {
            return Error.Equals(other.Error)
                && Message.Equals(other.Message)
                ;
        }
    }
}
