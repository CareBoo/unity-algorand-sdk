using System;
using AlgoSdk.MsgPack;
using Unity.Collections;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct TransactionParams
        : IMessagePackObject
        , IEquatable<TransactionParams>
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
            return this.Equals(ref other);
        }
    }
}

namespace AlgoSdk.MsgPack
{
    internal static partial class FieldMaps
    {
        internal static readonly Field<TransactionParams>.Map transactionParamsFields =
            new Field<TransactionParams>.Map()
                .Assign("consensus-version", (ref TransactionParams x) => ref x.ConsensusVersion)
                .Assign("fee", (ref TransactionParams x) => ref x.Fee)
                .Assign("genesis-hash", (ref TransactionParams x) => ref x.GenesisHash)
                .Assign("genesis-id", (ref TransactionParams x) => ref x.GenesisId)
                .Assign("last-round", (ref TransactionParams x) => ref x.LastRound)
                .Assign("min-fee", (ref TransactionParams x) => ref x.MinFee)
                ;
    }
}
