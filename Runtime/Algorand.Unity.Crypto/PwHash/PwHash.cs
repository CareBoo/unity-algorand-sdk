using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Collections.LowLevel.Unsafe;

namespace Algorand.Unity.Crypto
{
    [StructLayout(LayoutKind.Sequential, Size = SizeBytes)]
    public partial struct PwHash
    {
        public const uint MinLength = sodium.crypto_pwhash_PASSWD_MIN;
        public const uint MaxLength = sodium.crypto_pwhash_PASSWD_MAX;

        public const int SizeBytes = sizeof(ulong)
            + sizeof(ulong)
            + sodium.crypto_pwhash_STRBYTES;

        internal OpsLimit opsLimit;
        internal MemLimit memLimit;
        internal unsafe fixed byte bytes[sodium.crypto_pwhash_STRBYTES];

        /// <summary>
        /// Returns true if this string is empty (has no characters).
        /// </summary>
        /// <value>True if this string is empty (has no characters).</value>
        public bool IsEmpty => this[0] == 0;

        public bool IsCreated => opsLimit >= OpsLimit.Min && memLimit >= MemLimit.Min;

        public static PwHash Interactive => new PwHash(OpsLimit.Interactive, MemLimit.Interactive);

        public static PwHash Sensitive => new PwHash(OpsLimit.Sensitive, MemLimit.Sensitive);

        public static PwHash Moderate => new PwHash(OpsLimit.Moderate, MemLimit.Moderate);

        public PwHash(OpsLimit opsLimit, MemLimit memLimit)
        {
            this.opsLimit = opsLimit;
            this.memLimit = memLimit;
        }

        /// <summary>
        /// Returns the byte (not character) at an index.
        /// </summary>
        /// <param name="index">A byte index.</param>
        /// <value>The byte at the index.</value>
        /// <exception cref="IndexOutOfRangeException">Thrown if the index is out of bounds.</exception>
        public byte this[int index]
        {
            get
            {
                unsafe
                {
                    CheckIndexInRange(index);
                    return GetUnsafePtr()[index];
                }
            }

            set
            {
                unsafe
                {
                    CheckIndexInRange(index);
                    GetUnsafePtr()[index] = value;
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe byte* GetUnsafePtr()
        {
            return (byte*)UnsafeUtility.AddressOf(ref bytes[0]);
        }

        [Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
        void CheckIndexInRange(int index)
        {
            if (index < 0)
                throw new IndexOutOfRangeException($"Index {index} must be positive.");
            if (index >= SizeBytes)
                throw new IndexOutOfRangeException($"Index {index} is out of range in PwHash of '{SizeBytes}' Length.");
        }
    }
}
