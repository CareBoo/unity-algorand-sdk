using System;
using AlgoSdk.Crypto;
using Unity.Collections;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct BlockResponse
        : IEquatable<BlockResponse>
    {
        [AlgoApiKey(null, "block")]
        public Header HeaderData;

        public bool Equals(BlockResponse other)
        {
            return HeaderData.Equals(other.HeaderData);
        }

        [AlgoApiObject]
        public struct Header
            : IEquatable<Header>
        {
            [AlgoApiKey(null, "earn")]
            public ulong Earn;

            [AlgoApiKey(null, "fees")]
            public Address Fees;

            [AlgoApiKey(null, "frac")]
            public ulong Fraction;

            [AlgoApiKey(null, "gen")]
            public FixedString64Bytes GenesisId;

            [AlgoApiKey(null, "gh")]
            public GenesisHash GenesisHash;

            [AlgoApiKey(null, "prev")]
            public FixedString128Bytes PreviousBlock;

            [AlgoApiKey(null, "proto")]
            public FixedString128Bytes Proto;

            [AlgoApiKey(null, "rate")]
            public ulong Rate;

            [AlgoApiKey(null, "rnd")]
            public ulong Round;

            [AlgoApiKey(null, "rwcalr")]
            public ulong RwCalr;

            [AlgoApiKey(null, "rwd")]
            public Address Rwd;

            [AlgoApiKey(null, "seed")]
            public Sha512_256_Hash Seed;

            [AlgoApiKey(null, "tc")]
            public ulong Tc;

            [AlgoApiKey(null, "ts")]
            public ulong Ts;

            [AlgoApiKey(null, "txn")]
            public Sha512_256_Hash Txn;

            [AlgoApiKey(null, "txns")]
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
