using System;
using System.Runtime.InteropServices;
using AlgoSdk.LowLevel;
using Unity.Collections;
using Unity.Jobs;
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
                var (sk, pk) = CreateKey(in this);
                sk.Dispose();
                return pk;
            }
        }

        [StructLayout(LayoutKind.Explicit, Size = Size)]
        public struct PublicKey
        : IByteArray
        , IEquatable<PublicKey>
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
        }

        public static (SecretKeyHandle sk, PublicKey pk) CreateKey(in Seed seed)
        {
            var pk = new PublicKey();
            var sk = SecretKeyHandle.Create();
            fixed (Seed* seedPtr = &seed)
            {
                int error = crypto_sign_ed25519_seed_keypair(&pk, sk, seedPtr);
            }
            return (sk, pk);
        }
    }
}
