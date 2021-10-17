using System;
using Unity.Collections;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct CreateWalletRequest
        : IEquatable<CreateWalletRequest>
    {
        [AlgoApiField("master_derivation_key", null)]
        public PrivateKey MasterDerivationKey;

        [AlgoApiField("wallet_driver_name", null)]
        public FixedString128Bytes WalletDriverName;

        [AlgoApiField("wallet_name", null)]
        public FixedString128Bytes WalletName;

        [AlgoApiField("wallet_password", null)]
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
