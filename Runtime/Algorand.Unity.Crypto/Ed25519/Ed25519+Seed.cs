using System;
using System.Runtime.InteropServices;
using Algorand.Unity.LowLevel;
using Unity.Collections;
using UnityEngine;
using static Algorand.Unity.Crypto.sodium;

namespace Algorand.Unity.Crypto
{
    public static partial class Ed25519
    {
        [StructLayout(LayoutKind.Explicit, Size = Size)]
        [Serializable]
        public struct Seed
            : IByteArray
                , IEquatable<Seed>
        {
            public const int Size = 32;

            [FieldOffset(0)]
            [SerializeField]
            internal FixedBytes16 offset0000;

            [FieldOffset(16)]
            [SerializeField]
            internal FixedBytes16 offset0016;

            public unsafe void* GetUnsafePtr()
            {
                fixed (byte* b = &offset0000.byte0000)
                {
                    return b;
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
                return ByteArray.Equals(this, other);
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
                unsafe
                {
                    fixed (Seed* seedPtr = &this)
                    {
                        var error = crypto_sign_ed25519_seed_keypair(
                            &pk,
                            sk.GetUnsafePtr(),
                            seedPtr
                        );
                        if (error > 0) throw new Exception($"error code {error} when converting to KeyPair");
                    }
                }

                return new KeyPair(sk, pk);
            }
        }
    }
}
