using System;
using Unity.Collections;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct RenameWalletRequest
        : IEquatable<RenameWalletRequest>
    {
        [AlgoApiField("wallet_id", null)]
        public FixedString128Bytes WalletId;

        [AlgoApiField("wallet_name", null)]
        public FixedString128Bytes WalletName;

        [AlgoApiField("wallet_password", null)]
        public FixedString128Bytes WalletPassword;

        public bool Equals(RenameWalletRequest other)
        {
            return WalletId.Equals(other.WalletId)
                && WalletName.Equals(other.WalletName)
                && WalletPassword.Equals(other.WalletPassword)
                ;
        }
    }
}
