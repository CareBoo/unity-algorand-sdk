using System;
using System.Runtime.InteropServices;
using Algorand.Unity.Formatters;
using Algorand.Unity.LowLevel;
using Unity.Collections;
using UnityEngine;

namespace Algorand.Unity
{
    /// <summary>
    /// 32 byte public key required for <see cref="KeyRegTxn.SelectionParticipationKey"/>
    /// </summary>
    [AlgoApiFormatter(typeof(ByteArrayFormatter<VrfPubKey>))]
    [StructLayout(LayoutKind.Explicit, Size = SizeBytes)]
    [Serializable]
    public partial struct VrfPubKey
        : IEquatable<VrfPubKey>
        , IByteArray
    {
        [FieldOffset(0), SerializeField] internal FixedBytes16 offset0000;

        [FieldOffset(16), SerializeField] internal FixedBytes16 offset0016;

        public const int SizeBytes = 256 / 8;

        public byte this[int index]
        {
            get => this.GetByteAt(index);
            set => this.SetByteAt(index, value);
        }

        public int Length => SizeBytes;

        public bool Equals(VrfPubKey other)
        {
            return ByteArray.Equals(this, other);
        }

        public unsafe void* GetUnsafePtr()
        {
            fixed (byte* b = &offset0000.byte0000)
                return b;
        }
    }
}
