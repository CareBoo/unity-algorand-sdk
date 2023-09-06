using System;
using System.Runtime.InteropServices;
using Algorand.Unity.LowLevel;
using static Algorand.Unity.Crypto.sodium;

namespace Algorand.Unity.Crypto
{
    public static partial class Ed25519
    {
        [StructLayout(LayoutKind.Explicit, Size = SizeBytes)]
        public struct Signature
            : IByteArray
                , IEquatable<Signature>
        {
            public const int SizeBytes = 64;

            [FieldOffset(0)]
            internal FixedBytes64 buffer;

            public byte this[int index]
            {
                get => this.GetByteAt(index);
                set => this.SetByteAt(index, value);
            }

            public unsafe void* GetUnsafePtr()
            {
                fixed (byte* b = &buffer.offset0000.offset0000.byte0000)
                {
                    return b;
                }
            }

            public int Length => SizeBytes;

            public bool Equals(Signature other)
            {
                return ByteArray.Equals(this, other);
            }

            public bool Verify<TMessage>(TMessage message, PublicKey pk)
                where TMessage : IByteArray
            {
                unsafe
                {
                    fixed (Signature* s = &this)
                    {
                        var error = crypto_sign_ed25519_verify_detached(
                            s,
                            message.GetUnsafePtr(),
                            (ulong)message.Length,
                            &pk);
                        return error == 0;
                    }
                }
            }

            public bool Verify(ReadOnlySpan<byte> message, PublicKey pk)
            {
                unsafe
                {
                    fixed (byte* m = message)
                    fixed (Signature* s = &this)
                    {
                        var error = crypto_sign_ed25519_verify_detached(
                            s,
                            m,
                            (ulong)message.Length,
                            &pk);
                        return error == 0;
                    }
                }
            }

            public override bool Equals(object obj)
            {
                return ByteArray.Equals(this, obj);
            }

            public override int GetHashCode()
            {
                return ByteArray.GetHashCode(this);
            }

            public static bool operator ==(in Signature x, in Signature y)
            {
                return ByteArray.Equals(x, y);
            }

            public static bool operator !=(in Signature x, in Signature y)
            {
                return !ByteArray.Equals(x, y);
            }
        }
    }
}