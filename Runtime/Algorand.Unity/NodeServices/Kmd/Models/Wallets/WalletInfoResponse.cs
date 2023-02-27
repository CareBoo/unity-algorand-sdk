using System;

namespace Algorand.Unity.Kmd
{
    [AlgoApiObject]
    public partial struct WalletInfoResponse
        : IEquatable<WalletInfoResponse>
    {
        [AlgoApiField("wallet_handle")]
        public WalletHandle WalletHandle;

        [AlgoApiField("error")]
        public Optional<bool> Error;

        [AlgoApiField("message")]
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
