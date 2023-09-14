using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace Algorand.Unity.Crypto
{
    [StructLayout(LayoutKind.Explicit, Size = SizeBytes)]
    public unsafe struct PwHash
        : INativeList<byte>
        , IUTF8Bytes
    {
        public const int SizeBytes = sodium.crypto_pwhash_STRBYTES + sizeof(ushort);
        public const int utf8MaxLengthInBytes = sodium.crypto_pwhash_STRBYTES - 1;

        [FieldOffset(0)] internal ushort utf8LengthInBytes;
        [FieldOffset(2)] internal fixed byte bytes[sodium.crypto_pwhash_STRBYTES];

        /// <summary>
        /// Returns true if this string is empty (has no characters).
        /// </summary>
        /// <value>True if this string is empty (has no characters).</value>
        public bool IsEmpty => utf8LengthInBytes == 0;

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

        /// <summary>
        /// The number of bytes this string has for storing UTF-8 characters.
        /// </summary>
        /// <value>The number of bytes this string has for storing UTF-8 characters.</value>
        /// <remarks>
        /// Does not include the null-terminator byte.
        ///
        /// A setter is included for conformity with <see cref="INativeList{T}"/>, but <see cref="Capacity"/> is fixed at 125.
        /// Setting the value to anything other than 125 throws an exception.
        ///
        /// In UTF-8 encoding, each Unicode code point (character) requires 1 to 4 bytes,
        /// so the number of characters that can be stored may be less than the capacity.
        /// </remarks>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if attempting to set the capacity to anything other than 125.</exception>
        public int Capacity
        {
            get
            {
                return utf8MaxLengthInBytes;
            }
            set
            {
                CheckCapacityInRange(value);
            }
        }

        /// <summary>
        /// The current length in bytes of this string's content.
        /// </summary>
        /// <remarks>
        /// The length value does not include the null-terminator byte.
        /// </remarks>
        /// <param name="value">The new length in bytes of the string's content.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if the new length is out of bounds.</exception>
        /// <value>
        /// The current length in bytes of this string's content.
        /// </value>
        public int Length
        {
            get
            {
                return utf8LengthInBytes;
            }
            set
            {
                CheckLengthInRange(value);
                utf8LengthInBytes = (ushort)value;
                unsafe
                {
                    GetUnsafePtr()[utf8LengthInBytes] = 0;
                }
            }
        }

        public void Clear()
        {
            Length = 0;
        }

        /// <summary>
        /// Returns the reference to a byte (not character) at an index.
        /// </summary>
        /// <param name="index">A byte index.</param>
        /// <returns>A reference to the byte at the index.</returns>
        /// <exception cref="IndexOutOfRangeException">Thrown if the index is out of bounds.</exception>
        public ref byte ElementAt(int index)
        {
            unsafe
            {
                CheckIndexInRange(index);
                return ref GetUnsafePtr()[index];
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe byte* GetUnsafePtr()
        {
            return (byte*) UnsafeUtility.AddressOf(ref bytes[0]);
        }

        /// <summary>
        /// Attempts to set the length in bytes. Does nothing if the new length is invalid.
        /// </summary>
        /// <param name="newLength">The desired length.</param>
        /// <param name="clearOptions">Whether added or removed bytes should be cleared (zeroed). (Increasing the length adds bytes; decreasing the length removes bytes.)</param>
        /// <returns>True if the new length is valid.</returns>
        public bool TryResize(int newLength, NativeArrayOptions clearOptions = NativeArrayOptions.ClearMemory)
        {
            if (newLength < 0 || newLength > utf8MaxLengthInBytes)
                return false;
            if (newLength == utf8LengthInBytes)
                return true;
            unsafe
            {
                if (clearOptions == NativeArrayOptions.ClearMemory)
                {
                    if (newLength > utf8LengthInBytes)
                        UnsafeUtility.MemClear(GetUnsafePtr() + utf8LengthInBytes, newLength - utf8LengthInBytes);
                    else
                        UnsafeUtility.MemClear(GetUnsafePtr() + newLength, utf8LengthInBytes - newLength);
                }
                utf8LengthInBytes = (ushort)newLength;
                // always null terminate
                GetUnsafePtr()[utf8LengthInBytes] = 0;
            }
            return true;
        }

        [Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
        void CheckIndexInRange(int index)
        {
            if (index < 0)
                throw new IndexOutOfRangeException($"Index {index} must be positive.");
            if (index >= utf8LengthInBytes)
                throw new IndexOutOfRangeException($"Index {index} is out of range in FixedString64Bytes of '{utf8LengthInBytes}' Length.");
        }

        [Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
        void CheckLengthInRange(int length)
        {
            if (length < 0)
                throw new ArgumentOutOfRangeException($"Length {length} must be positive.");
            if (length > utf8MaxLengthInBytes)
                throw new ArgumentOutOfRangeException($"Length {length} is out of range in FixedString64Bytes of '{utf8MaxLengthInBytes}' Capacity.");
        }

        [Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
        void CheckCapacityInRange(int capacity)
        {
            if (capacity > utf8MaxLengthInBytes)
                throw new ArgumentOutOfRangeException($"Capacity {capacity} must be lower than {utf8MaxLengthInBytes}.");
        }

        [Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
        static void CheckCopyError(CopyError error, String source)
        {
            if (error != CopyError.None)
                throw new ArgumentException($"FixedString64Bytes: {error} while copying \"{source}\"");
        }

        [Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
        static void CheckFormatError(FormatError error)
        {
            if (error != FormatError.None)
                throw new ArgumentException("Source is too long to fit into fixed string of this size");
        }
    }
}
