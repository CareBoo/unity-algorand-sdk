using System;
using System.Runtime.InteropServices;
using AlgoSdk.LowLevel;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;
using static AlgoSdk.Crypto.sodium;

namespace AlgoSdk.Crypto
{
    public unsafe static class Ed25519
    {
        public struct KeyPair : INativeDisposable
        {
            public readonly SecretKeyHandle SecretKey;
            public readonly PublicKey PublicKey;

            public KeyPair(SecretKeyHandle secretKey, PublicKey publicKey)
            {
                SecretKey = secretKey;
                PublicKey = publicKey;
            }

            public void Deconstruct(out SecretKeyHandle secretKey, out PublicKey publicKey)
            {
                secretKey = SecretKey;
                publicKey = PublicKey;
            }

            public JobHandle Dispose(JobHandle inputDeps)
            {
                return SecretKey.Dispose(inputDeps);
            }

            public void Dispose()
            {
                SecretKey.Dispose();
            }
        }

        public struct SecretKeyHandle
            : INativeDisposable
        {
            public const int KeySize = (32 + 32);

            private SecureMemoryHandle handle;

            public IntPtr Ptr => handle.Ptr;

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

            public Signature Sign<TMessage>(TMessage message)
                where TMessage : IByteArray
            {
                var signature = new Signature();
#if (UNITY_WEBGL && !UNITY_EDITOR)
                crypto_sign_ed25519_detached(
                    &signature, 
                    message.GetUnsafePtr(), 
                    (UIntPtr)message.Length,
                    Ptr);
#else
                var errorCode = crypto_sign_ed25519_detached(
                    &signature,
                    out _,
                    message.GetUnsafePtr(),
                    (UIntPtr)message.Length,
                    Ptr);
#endif
                return signature;
            }
        }

        [StructLayout(LayoutKind.Explicit, Size = Size)]
        [Serializable]
        public struct Seed
            : IByteArray
            , IEquatable<Seed>
        {
            [FieldOffset(0), SerializeField] internal FixedBytes16 offset0000;
            [FieldOffset(16), SerializeField] internal FixedBytes16 offset0016;

            public const int Size = 32;

            public unsafe void* GetUnsafePtr()
            {
                fixed (byte* b = &offset0000.byte0000)
                    return b;
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
                var (sk, pk) = ToKeyPair();
                sk.Dispose();
                return pk;
            }

            public KeyPair ToKeyPair()
            {
                var pk = new PublicKey();
                var sk = SecretKeyHandle.Create();
                fixed (Seed* seedPtr = &this)
                {
                    int error = crypto_sign_ed25519_seed_keypair(
                        &pk,
                        sk.Ptr,
                        seedPtr
                        );
                    if (error > 0)
                    {
                        throw new System.Exception($"error code {error} when converting to KeyPair");
                    }
                }
                return new KeyPair(sk, pk);
            }
        }

        [Serializable]
        [StructLayout(LayoutKind.Explicit, Size = SizeBytes)]
        public struct PublicKey
            : IByteArray
            , IEquatable<PublicKey>
        {
            [FieldOffset(0), SerializeField] internal FixedBytes16 offset0000;
            [FieldOffset(16), SerializeField] internal FixedBytes16 offset0016;

            public const int SizeBytes = 32;

            public unsafe void* GetUnsafePtr()
            {
                fixed (byte* b = &offset0000.byte0000)
                    return b;
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
                return ByteArray.Equals(this, obj);
            }

            public override int GetHashCode()
            {
                return ByteArray.GetHashCode(this);
            }

            public static bool operator ==(in PublicKey x, in PublicKey y)
            {
                return ByteArray.Equals(x, y);
            }

            public static bool operator !=(in PublicKey x, in PublicKey y)
            {
                return !ByteArray.Equals(x, y);
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

            public unsafe void* GetUnsafePtr()
            {
                fixed (byte* b = &buffer.offset0000.offset0000.byte0000)
                    return b;
            }

            public int Length => SizeBytes;

            public bool Verify<TMessage>(TMessage message, PublicKey pk)
                where TMessage : IByteArray
            {
                fixed (Signature* s = &this)
                {
                    var error = crypto_sign_ed25519_verify_detached(
                        s,
                        message.GetUnsafePtr(),
                        (UIntPtr)message.Length,
                        &pk);
                    return error == 0;
                }
            }

            public bool Equals(Signature other)
            {
                return ByteArray.Equals(this, other);
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
