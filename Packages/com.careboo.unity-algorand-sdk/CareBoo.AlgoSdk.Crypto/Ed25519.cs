using System;
using System.Runtime.InteropServices;
using AlgoSdk.LowLevel;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine.Assertions;
using static AlgoSdk.Crypto.sodium;

namespace AlgoSdk.Crypto
{
    public unsafe static class Ed25519
    {
        public struct SecretKeyHandle : INativeDisposable
        {
            public const int KeySize = (32 + 32);
            SecureMemoryHandle handle;

            public static SecretKeyHandle Create()
            {
                return SecureMemoryHandle.Create((UIntPtr)KeySize);
            }

            public JobHandle Dispose(JobHandle inputDeps)
            {
                return handle.Dispose(inputDeps);
            }

            public void Dispose()
            {
                handle.Dispose();
            }

            public static implicit operator SecureMemoryHandle(SecretKeyHandle secretKeyHandle)
            {
                return secretKeyHandle.handle;
            }

            public static implicit operator SecretKeyHandle(SecureMemoryHandle secureMemoryHandle)
            {
                return new SecretKeyHandle() { handle = secureMemoryHandle };
            }

            public Signature Sign<TMessage>(in TMessage message)
                where TMessage : IByteArray
            {
                var signature = new Signature();
                crypto_sign_ed25519_detached(&signature, out var signatureLength, (byte*)message.Buffer, (ulong)message.Length, this);
                Assert.AreEqual(signatureLength, (ulong)signature.Length);
                return signature;
            }

            public Seed ToSeed()
            {
                var seed = new Seed();
                crypto_sign_ed25519_sk_to_seed(&seed, this);
                return seed;
            }
        }

        [StructLayout(LayoutKind.Explicit, Size = Size)]
        public struct Seed
        : IByteArray
        , IEquatable<Seed>
        {
            [FieldOffset(0)] internal FixedBytes16 offset0000;
            [FieldOffset(16)] internal FixedBytes16 offset0016;

            public const int Size = 32;

            public unsafe IntPtr Buffer
            {
                get
                {
                    fixed (byte* b = &offset0000.byte0000)
                        return (IntPtr)b;
                }
            }

            public int Length => Size;

            public byte this[int index]
            {
                get => this.GetByteAt(index);
                set => this.SetByteAt(index, value);
            }

            public bool Equals(Seed other)
            {
                for (var i = 0; i < Length; i++)
                    if (this[i] != other[i])
                        return false;
                return true;
            }

            public PublicKey ToPublicKey()
            {
                var (sk, pk) = ToKeys();
                sk.Dispose();
                return pk;
            }

            public (SecretKeyHandle, PublicKey) ToKeys()
            {
                var pk = new PublicKey();
                var sk = SecretKeyHandle.Create();
                fixed (Seed* seedPtr = &this)
                {
                    int error = crypto_sign_ed25519_seed_keypair(&pk, sk, seedPtr);
                }
                return (sk, pk);
            }
        }

        [StructLayout(LayoutKind.Explicit, Size = SizeBytes)]
        public struct PublicKey
        : IByteArray
        , IEquatable<PublicKey>
        {
            [FieldOffset(0)] internal FixedBytes16 offset0000;
            [FieldOffset(16)] internal FixedBytes16 offset0016;

            public const int SizeBytes = 32;

            public unsafe IntPtr Buffer
            {
                get
                {
                    fixed (byte* b = &offset0000.byte0000)
                        return (IntPtr)b;
                }
            }

            public int Length => 32;

            public byte this[int index]
            {
                get => this.GetByteAt(index);
                set => this.SetByteAt(index, value);
            }

            public bool Equals(PublicKey other)
            {
                for (var i = 0; i < Length; i++)
                    if (this[i] != other[i])
                        return false;
                return true;
            }

            public override bool Equals(object obj)
            {
                return ByteArray.Equals(in this, obj);
            }

            public override int GetHashCode()
            {
                return ByteArray.GetHashCode(in this);
            }

            public static bool operator ==(in PublicKey x, in PublicKey y)
            {
                return ByteArray.Equals(in x, in y);
            }

            public static bool operator !=(in PublicKey x, in PublicKey y)
            {
                return !ByteArray.Equals(in x, in y);
            }
        }

        [StructLayout(LayoutKind.Explicit, Size = SizeBytes)]
        public struct Signature
            : IByteArray
            , IEquatable<Signature>
        {
            public const int SizeBytes = 64;
            [FieldOffset(0)] internal FixedBytes64 buffer;

            public byte this[int index]
            {
                get => this.GetByteAt(index);
                set => this.SetByteAt(index, value);
            }

            public unsafe IntPtr Buffer
            {
                get
                {
                    fixed (byte* b = &buffer.offset0000.offset0000.byte0000)
                        return (IntPtr)b;
                }
            }

            public int Length => SizeBytes;

            public bool Verify<TMessage>(TMessage message, PublicKey pk)
                where TMessage : IByteArray
            {
                fixed (Signature* s = &this)
                {
                    var error = crypto_sign_ed25519_verify_detached(s, (byte*)message.Buffer, (ulong)message.Length, &pk);
                    return error == 0;
                }
            }

            public bool Equals(Signature other)
            {
                return ByteArray.Equals(in this, in other);
            }

            public override bool Equals(object obj)
            {
                return ByteArray.Equals(in this, obj);
            }

            public override int GetHashCode()
            {
                return ByteArray.GetHashCode(in this);
            }

            public static bool operator ==(in Signature x, in Signature y)
            {
                return ByteArray.Equals(in x, in y);
            }

            public static bool operator !=(in Signature x, in Signature y)
            {
                return !ByteArray.Equals(in x, in y);
            }
        }
    }
}
