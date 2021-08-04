using System;
using AlgoSdk.LowLevel;

namespace AlgoSdk
{
    public struct LogicSig
        : ISignature
        , IEquatable<LogicSig>
    {
        public bool Equals(LogicSig other)
        {
            return true;
        }

        public bool Verify<TMessage>(TMessage message, Crypto.Ed25519.PublicKey pk) where TMessage : IByteArray
        {
            throw new System.NotImplementedException();
        }

        public static bool operator ==(in LogicSig x, in LogicSig y)
        {
            return true;
        }

        public static bool operator !=(in LogicSig x, in LogicSig y)
        {
            return false;
        }
    }
}
