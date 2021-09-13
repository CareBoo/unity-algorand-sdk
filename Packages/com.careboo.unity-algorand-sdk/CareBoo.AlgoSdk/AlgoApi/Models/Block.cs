using System;
using AlgoSdk.Crypto;
using AlgoSdk.MsgPack;
using Unity.Collections;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct Block
        : IMessagePackObject
        , IEquatable<Block>
    {
        [AlgoApiKey("block")]
        public Header HeaderData;

        public bool Equals(Block other)
        {
            return this.Equals(ref other);
        }

        [AlgoApiObject]
        public struct Header
            : IMessagePackObject
            , IEquatable<Header>
        {
            [AlgoApiKey("earn")]
            public ulong Earn;

            [AlgoApiKey("fees")]
            public Address Fees;

            [AlgoApiKey("frac")]
            public ulong Fraction;

            [AlgoApiKey("gen")]
            public FixedString64Bytes GenesisId;

            [AlgoApiKey("gh")]
            public GenesisHash GenesisHash;

            [AlgoApiKey("prev")]
            public FixedString128Bytes PreviousBlock;

            [AlgoApiKey("proto")]
            public FixedString128Bytes Proto;

            [AlgoApiKey("rate")]
            public ulong Rate;

            [AlgoApiKey("rnd")]
            public ulong Round;

            [AlgoApiKey("rwcalr")]
            public ulong RwCalr;

            [AlgoApiKey("rwd")]
            public Address Rwd;

            [AlgoApiKey("seed")]
            public Sha512_256_Hash Seed;

            [AlgoApiKey("tc")]
            public ulong Tc;

            [AlgoApiKey("ts")]
            public ulong Ts;

            [AlgoApiKey("txn")]
            public Sha512_256_Hash Txn;

            [AlgoApiKey("txns")]
            public BlockTransaction[] Txns;

            public bool Equals(Header other)
            {
                return this.Equals(ref other);
            }
        }
    }
}

namespace AlgoSdk.MsgPack
{
    internal static partial class FieldMaps
    {
        internal static readonly Field<Block>.Map blockFields =
            new Field<Block>.Map()
                .Assign("block", (ref Block x) => ref x.HeaderData)
                ;

        internal static readonly Field<Block.Header>.Map block_headerFields =
            new Field<Block.Header>.Map()
                .Assign("earn", (ref Block.Header x) => ref x.Earn)
                .Assign("fees", (ref Block.Header x) => ref x.Fees)
                .Assign("frac", (ref Block.Header x) => ref x.Fraction)
                .Assign("gen", (ref Block.Header x) => ref x.GenesisId)
                .Assign("gh", (ref Block.Header x) => ref x.GenesisHash)
                .Assign("prev", (ref Block.Header x) => ref x.PreviousBlock)
                .Assign("proto", (ref Block.Header x) => ref x.Proto)
                .Assign("rate", (ref Block.Header x) => ref x.Rate)
                .Assign("rnd", (ref Block.Header x) => ref x.Round)
                .Assign("rwcalr", (ref Block.Header x) => ref x.RwCalr)
                .Assign("rwd", (ref Block.Header x) => ref x.Rwd)
                .Assign("seed", (ref Block.Header x) => ref x.Seed)
                .Assign("tc", (ref Block.Header x) => ref x.Tc)
                .Assign("ts", (ref Block.Header x) => ref x.Ts)
                .Assign("txn", (ref Block.Header x) => ref x.Txn)
                .Assign("txns", (ref Block.Header x) => ref x.Txns, ArrayComparer<BlockTransaction>.Instance)
                ;
    }
}
