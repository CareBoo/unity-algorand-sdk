using System;
using Unity.Collections;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct TransactionParams
        : IEquatable<TransactionParams>
    {
        [AlgoApiKey("consensus-version", null)]
        public FixedString128Bytes ConsensusVersion;

        [AlgoApiKey("fee", null)]
        public ulong Fee;

        [AlgoApiKey("genesis-hash", null)]
        public GenesisHash GenesisHash;

        [AlgoApiKey("genesis-id", null)]
        public FixedString32Bytes GenesisId;

        [AlgoApiKey("last-round", null)]
        public ulong LastRound;

        [AlgoApiKey("min-fee", null)]
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
