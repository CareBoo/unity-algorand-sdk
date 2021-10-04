using System;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct RenewWalletHandleTokenResponse
        : IEquatable<RenewWalletHandleTokenResponse>
    {
        [AlgoApiField("error", null)]
        public Optional<bool> Error;

        [AlgoApiField("message", null)]
        public string Message;

        [AlgoApiField("wallet_handle", null)]
        public WalletHandle WalletHandle;

        public bool Equals(RenewWalletHandleTokenResponse other)
        {
            return Error.Equals(other.Error)
                && Message.Equals(other.Message)
                && WalletHandle.Equals(other.WalletHandle)
                ;
        }
    }
}
