using System;
using System.Collections;
using System.Collections.Generic;
using Algorand.Unity.Crypto;
using UnityEngine;

namespace Algorand.Unity
{
    public interface ITxnCommitments
    {
        /// <summary>
        /// Root of transaction merkle tree using SHA512_256 hash function.
        /// This commitment is computed based on the PaysetCommit type specified in the block's consensus protocol.
        /// </summary>
        Digest NativeSha512_256Commitment { get; set; }

        /// <summary>
        /// Root of transaction vector commitment merkle tree using SHA256 hash function.
        /// </summary>
        Digest Sha256Commitment { get; set; }
    }

    /// <summary>
    /// TxnCommitments represents the commitments computed from the transactions in the block.
    /// It contains multiple commitments based on different algorithms and hash functions, to support different use cases.
    /// </summary>
    [Serializable]
    [AlgoApiObject]
    public partial struct TxnCommitments
        : IEquatable<TxnCommitments>
    {
        [SerializeField]
        private Digest nativeSha512_256Commitment;

        [SerializeField]
        private Digest sha256Commitment;

        public Digest NativeSha512_256Commitment
        {
            get => nativeSha512_256Commitment;
            set => nativeSha512_256Commitment = value;
        }

        public Digest Sha256Commitment
        {
            get => sha256Commitment;
            set => sha256Commitment = value;
        }

        public bool Equals(TxnCommitments other)
        {
            return NativeSha512_256Commitment.Equals(other.NativeSha512_256Commitment)
                && Sha256Commitment.Equals(other.Sha256Commitment)
                ;
        }
    }
}
