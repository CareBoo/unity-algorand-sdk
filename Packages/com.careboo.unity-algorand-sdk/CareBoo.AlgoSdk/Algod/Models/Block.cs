using System;
using AlgoSdk.Crypto;
using AlgoSdk.MsgPack;
using Unity.Collections;

namespace AlgoSdk
{
    public struct Block
        : IMessagePackObject
        , IEquatable<Block>
    {
        public Header HeaderData;

        public bool Equals(Block other)
        {
            return this.Equals(ref other);
        }

        public struct Header
            : IMessagePackObject
            , IEquatable<Header>
        {
            public ulong Earn;
            public Address Fees;
            public ulong Fraction;
            public FixedString64Bytes GenesisId;
            public GenesisHash GenesisHash;
            public FixedString128Bytes PreviousBlock;
            public FixedString128Bytes Proto;
            public ulong Rate;
            public ulong Round;
            public ulong RwCalr;
            public Address Rwd;
            public Sha512_256_Hash Seed;
            public ulong Tc;
            public ulong Ts;
            public Sha512_256_Hash Txn;
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
