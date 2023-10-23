using System;
using Unity.Collections;

namespace Algorand.Unity.Kmd
{
    [AlgoApiObject]
    [Serializable]
    public partial struct Wallet
        : IEquatable<Wallet>
    {
        [AlgoApiField("driver_name")]
        public FixedString64Bytes DriverName;

        [AlgoApiField("driver_version")]
        public uint DriverVersion;

        [AlgoApiField("id")]
        public FixedString128Bytes Id;

        [AlgoApiField("mnemonic_ux")]
        public Optional<bool> MnemonicUx;

        [AlgoApiField("name")]
        public FixedString64Bytes Name;

        [AlgoApiField("supported_txs")]
        public TransactionType[] SupportedTransactions;

        public bool Equals(Wallet other)
        {
            return Id.Equals(other.Id);
        }
    }
}
