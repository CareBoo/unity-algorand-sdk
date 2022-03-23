using System;

namespace AlgoSdk
{
    [AlgoApiObject]
    public partial struct WalletInfoResponse
        : IEquatable<WalletInfoResponse>
    {
        [AlgoApiField("wallet_handle", null)]
        public WalletHandle WalletHandle;

        [AlgoApiField("error", null)]
        public Optional<bool> Error;

        [AlgoApiField("message", null)]
        public string Message;

        public bool Equals(WalletInfoResponse other)
        {
            return WalletHandle.Equals(other.WalletHandle)
                && Error.Equals(other.Error)
                && Message.Equals(other.Message)
                ;
        }
    }
}
