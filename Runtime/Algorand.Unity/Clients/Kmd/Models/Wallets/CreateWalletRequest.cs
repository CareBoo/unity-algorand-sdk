using System;
using Unity.Collections;

namespace Algorand.Unity.Kmd
{
    [AlgoApiObject]
    public partial struct CreateWalletRequest
        : IEquatable<CreateWalletRequest>
    {
        [AlgoApiField("master_derivation_key")]
        public PrivateKey MasterDerivationKey;

        [AlgoApiField("wallet_driver_name")]
        public FixedString128Bytes WalletDriverName;

        [AlgoApiField("wallet_name")]
        public FixedString128Bytes WalletName;

        [AlgoApiField("wallet_password")]
        public FixedString128Bytes WalletPassword;

        public bool Equals(CreateWalletRequest other)
        {
            return MasterDerivationKey.Equals(other.MasterDerivationKey)
                && WalletDriverName.Equals(other.WalletDriverName)
                && WalletName.Equals(other.WalletName)
                && WalletPassword.Equals(other.WalletPassword)
                ;
        }
    }
}
