using AlgoSdk.LowLevel;

namespace AlgoSdk
{
    public struct MultiSig : ISignature
    {
        public bool Verify<TMessage>(TMessage message, Crypto.Ed25519.PublicKey pk) where TMessage : IByteArray
        {
            throw new System.NotImplementedException();
        }

        public static bool operator ==(in MultiSig x, in MultiSig y)
        {
            return false;
        }

        public static bool operator !=(in MultiSig x, in MultiSig y)
        {
            return false;
        }
    }
}
