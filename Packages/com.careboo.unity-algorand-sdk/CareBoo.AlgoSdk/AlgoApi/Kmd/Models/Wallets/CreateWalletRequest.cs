using System;
using Unity.Collections;

namespace AlgoSdk
{
    public struct CreateWalletRequest
        : IEquatable<CreateWalletRequest>
    {
        public PrivateKey MasterDerivationKey;

        public FixedString64Bytes WalletDriverName;

        public FixedString64Bytes WalletName;

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
