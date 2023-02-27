using System;
using Algorand.Unity.Crypto;
using Algorand.Unity.Formatters;
using Algorand.Unity.LowLevel;

namespace Algorand.Unity
{
    [AlgoApiFormatter(typeof(ByteArrayFormatter<Sig>))]
    public partial struct Sig
        : ISignature
        , IEquatable<Sig>
        , IByteArray
    {
        private Ed25519.Signature sig;

        public unsafe void* GetUnsafePtr() => sig.GetUnsafePtr();

        public int Length => sig.Length;

        public byte this[int index] { get => sig[index]; set => sig[index] = value; }

        public Sig(in Ed25519.Signature sig)
        {
            this.sig = sig;
        }

        public bool Verify<TMessage>(TMessage message, Ed25519.PublicKey pk)
            where TMessage : IByteArray
        {
            return sig.Verify(message, pk);
        }

        public static implicit operator Ed25519.Signature(Sig signature)
        {
            return signature.sig;
        }

        public static implicit operator Sig(Ed25519.Signature sig)
        {
            return new Sig(in sig);
        }

        public static bool operator ==(in Sig x, in Sig y)
        {
            return x.sig == y.sig;
        }

        public static bool operator !=(in Sig x, in Sig y)
        {
            return x.sig != y.sig;
        }

        public override int GetHashCode()
        {
            return sig.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            switch (obj)
            {
                case Sig signature: return this == signature;
                case Ed25519.Signature edSig: return sig == edSig;
                default: return false;
            }
        }

        public bool Equals(Sig other)
        {
            return this.sig.Equals(other.sig);
        }
    }
}
