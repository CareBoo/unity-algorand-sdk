using System;
using Unity.Collections;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct TransactionParams
        : IEquatable<TransactionParams>
    {
        [AlgoApiKey("consensus-version")]
        public FixedString128Bytes ConsensusVersion;
        [AlgoApiKey("fee")]
        public ulong Fee;
        [AlgoApiKey("genesis-hash")]
        public GenesisHash GenesisHash;
        [AlgoApiKey("genesis-id")]
        public FixedString32Bytes GenesisId;
        [AlgoApiKey("last-round")]
        public ulong LastRound;
        [AlgoApiKey("min-fee")]
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
