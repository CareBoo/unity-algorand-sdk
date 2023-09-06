using System;
using Algorand.Unity.LowLevel;
using Unity.Collections;
using Unity.Jobs;
using static Algorand.Unity.Crypto.sodium;

namespace Algorand.Unity.Crypto
{
    public static partial class Ed25519
    {
        public struct SecretKeyHandle
            : INativeDisposable
        {
            public const int KeySize = 32 + 32;

            private SecureMemoryHandle handle;

            public readonly IntPtr Ptr => handle.Ptr;

            public bool IsCreated => handle.IsCreated;

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
                    Ptr);
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
                        Ptr);
                }

                return signature;
            }
        }
    }
}