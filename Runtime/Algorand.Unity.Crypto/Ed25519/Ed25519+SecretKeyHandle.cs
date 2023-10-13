using System;
using System.Runtime.InteropServices;
using Algorand.Unity.LowLevel;
using Unity.Collections;
using Unity.Jobs;
using static Algorand.Unity.Crypto.sodium;

namespace Algorand.Unity.Crypto
{
    public static partial class Ed25519
    {
        public enum GenKeyPairError
        {
            None = 0
        }

        public static unsafe GenKeyPairError GenKeyPair(SecureMemoryHandle seedHandle, out KeyPair kp)
        {
            var pk = new PublicKey();
            var sk = SecretKeyHandle.Create();

            var error = (GenKeyPairError)crypto_sign_ed25519_seed_keypair(
                &pk,
                sk.GetUnsafePtr(),
                seedHandle.GetUnsafePtr()
            );
            kp = new KeyPair(sk, pk);
            return error;
        }

        public static unsafe GenKeyPairError GenKeyPair(ref Seed seed, ref SecretKey secretKey, ref PublicKey publicKey)
        {
            fixed (void* pk = &publicKey)
            fixed (void* sk = &secretKey)
            fixed (void* s = &seed)
            {
                return (GenKeyPairError)crypto_sign_ed25519_seed_keypair(
                    pk,
                    sk,
                    s
                );
            }
        }

        [Serializable]
        [StructLayout(LayoutKind.Explicit, Size = SizeBytes)]
        public unsafe struct SecretKey
        {
            public const int SizeBytes = crypto_sign_ed25519_SECRETKEYBYTES;
            public const int SizeUlongs = SizeBytes / sizeof(ulong);
            public const int SizeChars = SizeBytes / sizeof(char);

            [FieldOffset(0)] public fixed ulong ulongs[SizeUlongs];
            [FieldOffset(0), NonSerialized] public fixed char chars[SizeChars];
            [FieldOffset(0), NonSerialized] public fixed byte bytes[SizeBytes];
        }

        public struct SecretKeyHandle
            : INativeDisposable
        {
            public const int KeySize = 32 + 32;

            private SecureMemoryHandle handle;

            public readonly IntPtr Ptr => handle.Ptr;

            public bool IsCreated => handle.IsCreated;

            public readonly unsafe void* GetUnsafePtr() => handle.GetUnsafePtr();

            public JobHandle Dispose(JobHandle inputDeps)
            {
                return handle.Dispose(inputDeps);
            }

            public void Dispose()
            {
                handle.Dispose();
            }

            public static SecretKeyHandle Create()
            {
                return SecureMemoryHandle.Create((UIntPtr)KeySize);
            }

            public static implicit operator SecureMemoryHandle(SecretKeyHandle secretKeyHandle)
            {
                return secretKeyHandle.handle;
            }

            public static implicit operator SecretKeyHandle(SecureMemoryHandle secureMemoryHandle)
            {
                return new SecretKeyHandle { handle = secureMemoryHandle };
            }

            public readonly unsafe Signature Sign<TMessage>(TMessage message)
                where TMessage : IByteArray
            {
                var signature = new Signature();
                crypto_sign_ed25519_detached(
                    &signature,
                    out _,
                    message.GetUnsafePtr(),
                    (ulong)message.Length,
                    GetUnsafePtr());
                return signature;
            }

            public readonly unsafe Signature Sign(ReadOnlySpan<byte> message)
            {
                var signature = new Signature();
                fixed (byte* mPtr = &message.GetPinnableReference())
                {
                    crypto_sign_ed25519_detached(
                        &signature,
                        out _,
                        mPtr,
                        (ulong)message.Length,
                        GetUnsafePtr());
                }

                return signature;
            }
        }
    }
}
