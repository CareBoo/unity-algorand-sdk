using System;
using Unity.Collections;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct InitWalletHandleTokenRequest
        : IEquatable<InitWalletHandleTokenRequest>
    {

        [AlgoApiField("wallet_id", null)]
        public FixedString128Bytes WalletId;

        [AlgoApiField("wallet_password", null)]
        public FixedString128Bytes WalletPassword;

        public bool Equals(InitWalletHandleTokenRequest other)
        {
            return WalletId.Equals(other.WalletId)
                && WalletPassword.Equals(other.WalletPassword)
                ;
        }
    }
}
