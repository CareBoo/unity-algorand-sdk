using System;

namespace Algorand.Unity.Kmd
{
    [AlgoApiObject]
    public partial struct RenewWalletHandleTokenResponse
        : IEquatable<RenewWalletHandleTokenResponse>
    {
        [AlgoApiField("error")]
        public Optional<bool> Error;

        [AlgoApiField("message")]
        public string Message;

        [AlgoApiField("wallet_handle")]
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
