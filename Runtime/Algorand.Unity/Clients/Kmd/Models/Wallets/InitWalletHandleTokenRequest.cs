using System;
using Unity.Collections;

namespace Algorand.Unity.Kmd
{
    [AlgoApiObject]
    public partial struct InitWalletHandleTokenRequest
        : IEquatable<InitWalletHandleTokenRequest>
    {
        [AlgoApiField("wallet_id")]
        public FixedString128Bytes WalletId;

        [AlgoApiField("wallet_password")]
        public FixedString128Bytes WalletPassword;

        public bool Equals(InitWalletHandleTokenRequest other)
        {
            return WalletId.Equals(other.WalletId)
                && WalletPassword.Equals(other.WalletPassword)
                ;
        }
    }
}
