using System;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct BlockResponse
        : IEquatable<BlockResponse>
    {
        [AlgoApiField("block", "block")]
        public Block BlockHeader;

        [AlgoApiField(null, "cert")]
        public AlgoApiObject Cert;

        public bool Equals(BlockResponse other)
        {
            return BlockHeader.Equals(other.BlockHeader);
        }
    }
}
