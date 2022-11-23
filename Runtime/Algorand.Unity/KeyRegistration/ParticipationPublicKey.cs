using System;
using System.Runtime.InteropServices;
using Algorand.Unity.Crypto;
using Algorand.Unity.LowLevel;
using UnityEngine;

namespace Algorand.Unity
{
    /// <summary>
    /// Participation Public Key used for account registration.
    /// </summary>
    [Serializable]
    [AlgoApiFormatter(typeof(WrappedValueFormatter<ParticipationPublicKey, Ed25519.PublicKey>))]
    [StructLayout(LayoutKind.Explicit, Size = SizeBytes)]
    public partial struct ParticipationPublicKey
        : IWrappedValue<Ed25519.PublicKey>
        , IByteArray
        , IEquatable<ParticipationPublicKey>
    {
        public const int SizeBytes = Ed25519.PublicKey.SizeBytes;

        Ed25519.PublicKey IWrappedValue<Ed25519.PublicKey>.WrappedValue
        {
            get => publicKey;
            set => publicKey = value;
        }

        [FieldOffset(0)] private byte buffer;

        [FieldOffset(0), SerializeField] private Ed25519.PublicKey publicKey;

        /// <inheritdoc />
        public unsafe void* GetUnsafePtr()
        {
            fixed (byte* b = &buffer)
                return b;
        }

        public bool Equals(ParticipationPublicKey other)
        {
            return ByteArray.Equals(this, other);
        }

        /// <inheritdoc />
        public int Length => SizeBytes;

        /// <inheritdoc />
        public byte this[int index]
        {
            get => this.GetByteAt(index);
            set => this.SetByteAt(index, value);
        }

        public static implicit operator Ed25519.PublicKey(ParticipationPublicKey partPk)
        {
            return partPk.publicKey;
        }

        public static implicit operator ParticipationPublicKey(Ed25519.PublicKey pk)
        {
            return new ParticipationPublicKey { publicKey = pk };
        }
    }
}
