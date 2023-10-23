using System;
using Unity.Collections;

namespace Algorand.Unity.Kmd
{
    [AlgoApiObject]
    public partial struct RenameWalletRequest
        : IEquatable<RenameWalletRequest>
    {
        [AlgoApiField("wallet_id")]
        public FixedString128Bytes WalletId;

        [AlgoApiField("wallet_name")]
        public FixedString128Bytes WalletName;

        [AlgoApiField("wallet_password")]
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
