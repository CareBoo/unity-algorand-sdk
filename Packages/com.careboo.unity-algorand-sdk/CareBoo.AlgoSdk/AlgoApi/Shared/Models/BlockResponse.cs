using System;
using AlgoSdk.Crypto;
using Unity.Collections;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct BlockResponse
        : IEquatable<BlockResponse>
    {
        [AlgoApiField("block", "block")]
        public Block BlockHeader;

        public bool Equals(BlockResponse other)
        {
            return BlockHeader.Equals(other.BlockHeader);
        }
    }
}
