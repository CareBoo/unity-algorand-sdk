using System;

namespace AlgoSdk
{
    /// <summary>
    /// A wrapped Block from the <see cref="IAlgodClient.GetBlock"/>
    /// </summary>
    [AlgoApiObject]
    public struct BlockResponse
        : IEquatable<BlockResponse>
    {
        /// <summary>
        /// Block data.
        /// </summary>
        [AlgoApiField("block", "block")]
        public Block BlockHeader;

        /// <summary>
        /// Block certificate object.
        /// </summary>
        [AlgoApiField(null, "cert")]
        public AlgoApiObject Cert;

        public bool Equals(BlockResponse other)
        {
            return BlockHeader.Equals(other.BlockHeader);
        }
    }
}
