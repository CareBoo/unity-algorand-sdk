using System;
using UnityEngine;

namespace AlgoSdk
{
    /// <summary>
    /// A wrapped Block from the <see cref="IAlgodClient.GetBlock"/>
    /// </summary>
    [AlgoApiObject]
    [Serializable]
    public partial struct BlockResponse
        : IEquatable<BlockResponse>
    {
        /// <summary>
        /// Block data.
        /// </summary>
        [AlgoApiField("block", "block")]
        [Tooltip("Block data.")]
        public Block BlockHeader;

        /// <summary>
        /// Block certificate object.
        /// </summary>
        [AlgoApiField(null, "cert")]
        [Tooltip("Block certificate object.")]
        public AlgoApiObject Cert;

        public bool Equals(BlockResponse other)
        {
            return BlockHeader.Equals(other.BlockHeader);
        }
    }
}
