using System;
using AlgoSdk.LowLevel;

namespace AlgoSdk
{
    public struct MultiSig
        : ISignature
        , IEquatable<MultiSig>
    {
        public bool Equals(MultiSig other)
        {
            return true;
        }

        public bool Verify<TMessage>(TMessage message, Crypto.Ed25519.PublicKey pk) where TMessage : IByteArray
        {
            throw new System.NotImplementedException();
        }

        public static bool operator ==(in MultiSig x, in MultiSig y)
        {
            return true;
        }

        public static bool operator !=(in MultiSig x, in MultiSig y)
        {
            return false;
        }
    }
}
