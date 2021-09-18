using System;
using AlgoSdk.Crypto;
using Unity.Collections;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct Block
        : IEquatable<Block>
    {
        [AlgoApiKey("block")]
        public Header HeaderData;

        public bool Equals(Block other)
        {
            return HeaderData.Equals(other.HeaderData);
        }

        [AlgoApiObject]
        public struct Header
            : IEquatable<Header>
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
