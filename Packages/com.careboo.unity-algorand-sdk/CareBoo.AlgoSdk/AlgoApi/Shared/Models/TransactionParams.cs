using System;
using Unity.Collections;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct TransactionParams
        : IEquatable<TransactionParams>
    {
        [AlgoApiField("consensus-version", null)]
        public string ConsensusVersion;

        [AlgoApiField("fee", null)]
        public ulong Fee;

        [AlgoApiField("genesis-hash", null)]
        public GenesisHash GenesisHash;

        [AlgoApiField("genesis-id", null)]
        public FixedString32Bytes GenesisId;

        [AlgoApiField("min-fee", null)]
        public ulong MinFee;

        [AlgoApiField("last-round", null)]
        public ulong PreviousRound
        {
            get => prevRound;
            set
            {
                prevRound = value;
                FirstValidRound = value;
                LastValidRound = value + 1000;
            }
        }

        public bool FlatFee;

        public ulong FirstValidRound;

        public ulong LastValidRound;

        ulong prevRound;

        public bool Equals(TransactionParams other)
        {
            return ConsensusVersion.Equals(other.ConsensusVersion)
                && Fee.Equals(other.Fee)
                && GenesisHash.Equals(other.GenesisHash)
                && GenesisId.Equals(other.GenesisId)
                && MinFee.Equals(other.MinFee)
                && FirstValidRound.Equals(other.FirstValidRound)
                && LastValidRound.Equals(other.LastValidRound)
                && FlatFee.Equals(other.FlatFee)
                ;
        }
    }
}
