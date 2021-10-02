using System;
using AlgoSdk.Crypto;
using Unity.Collections;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct BlockResponse
        : IEquatable<BlockResponse>
    {
        [AlgoApiField(null, "block")]
        public Header HeaderData;

        public bool Equals(BlockResponse other)
        {
            return HeaderData.Equals(other.HeaderData);
        }

        [AlgoApiObject]
        public struct Header
            : IEquatable<Header>
        {
            [AlgoApiField(null, "earn")]
            public ulong Earn;

            [AlgoApiField(null, "fees")]
            public Address Fees;

            [AlgoApiField(null, "frac")]
            public ulong Fraction;

            [AlgoApiField(null, "gen")]
            public FixedString64Bytes GenesisId;

            [AlgoApiField(null, "gh")]
            public GenesisHash GenesisHash;

            [AlgoApiField(null, "prev")]
            public FixedString128Bytes PreviousBlock;

            [AlgoApiField(null, "proto")]
            public FixedString128Bytes Proto;

            [AlgoApiField(null, "rate")]
            public ulong Rate;

            [AlgoApiField(null, "rnd")]
            public ulong Round;

            [AlgoApiField(null, "rwcalr")]
            public ulong RwCalr;

            [AlgoApiField(null, "rwd")]
            public Address Rwd;

            [AlgoApiField(null, "seed")]
            public Sha512_256_Hash Seed;

            [AlgoApiField(null, "tc")]
            public ulong Tc;

            [AlgoApiField(null, "ts")]
            public ulong Ts;

            [AlgoApiField(null, "txn")]
            public Sha512_256_Hash Txn;

            [AlgoApiField(null, "txns")]
            public BlockTransaction[] Txns;

            public bool Equals(Header other)
            {
                return GenesisId.Equals(other.GenesisId)
                    && GenesisHash.Equals(other.GenesisHash)
                    && PreviousBlock.Equals(other.PreviousBlock)
                    && Round.Equals(other.Round)
                    && Proto.Equals(other.Proto)
                    && Seed.Equals(other.Seed)
                    ;
            }
        }
    }
}
