using System;
using Unity.Collections;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct TransactionParams
        : IEquatable<TransactionParams>
    {
        [AlgoApiField("consensus-version", null)]
        public FixedString128Bytes ConsensusVersion;

        [AlgoApiField("fee", null)]
        public ulong Fee;

        [AlgoApiField("genesis-hash", null)]
        public GenesisHash GenesisHash;

        [AlgoApiField("genesis-id", null)]
        public FixedString32Bytes GenesisId;

        [AlgoApiField("last-round", null)]
        public ulong LastRound;

        [AlgoApiField("min-fee", null)]
        public ulong MinFee;

        public bool Equals(TransactionParams other)
        {
            return ConsensusVersion.Equals(other.ConsensusVersion)
                && Fee.Equals(other.Fee)
                && GenesisHash.Equals(other.GenesisHash)
                && GenesisId.Equals(other.GenesisId)
                && LastRound.Equals(other.LastRound)
                && MinFee.Equals(other.MinFee)
                ;
        }
    }
}
