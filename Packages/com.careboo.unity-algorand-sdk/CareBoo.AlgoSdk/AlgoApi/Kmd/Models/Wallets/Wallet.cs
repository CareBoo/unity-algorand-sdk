using System;
using Unity.Collections;

namespace AlgoSdk
{
    [AlgoApiObject]
    [Serializable]
    public struct Wallet
        : IEquatable<Wallet>
    {
        [AlgoApiField("driver_name", null)]
        public FixedString64Bytes DriverName;

        [AlgoApiField("driver_version", null)]
        public uint DriverVersion;

        [AlgoApiField("id", null)]
        public FixedString128Bytes Id;

        [AlgoApiField("mnemonic_ux", null)]
        public Optional<bool> MnemonicUx;

        [AlgoApiField("name", null)]
        public FixedString64Bytes Name;

        [AlgoApiField("supported_txs", null)]
        public TransactionType[] SupportedTransactions;

        public bool Equals(Wallet other)
        {
            return Id.Equals(other.Id);
        }
    }
}
