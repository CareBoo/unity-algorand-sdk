using AlgoSdk.Crypto;
using AlgoSdk.LowLevel;

namespace AlgoSdk
{
    public struct Signature : ISignature
    {
        readonly Ed25519.Signature sig;

        public Signature(in Ed25519.Signature sig)
        {
            this.sig = sig;
        }

        public bool Verify<TMessage>(TMessage message, Ed25519.PublicKey pk)
            where TMessage : IByteArray
        {
            return sig.Verify(message, pk);
        }

        public static implicit operator Ed25519.Signature(Signature signature)
        {
            return signature.sig;
        }

        public static implicit operator Signature(Ed25519.Signature sig)
        {
            return new Signature(in sig);
        }

        public static bool operator ==(in Signature x, in Signature y)
        {
            return x.sig == y.sig;
        }

        public static bool operator !=(in Signature x, in Signature y)
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
                case Signature signature: return this == signature;
                case Ed25519.Signature edSig: return sig == edSig;
                default: return false;
            }
        }
    }
}
