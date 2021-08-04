using AlgoSdk.Crypto;
using AlgoSdk.LowLevel;

namespace AlgoSdk
{
    public interface ISignature
    {
        bool Verify<TMessage>(TMessage message, Ed25519.PublicKey pk) where TMessage : IByteArray;
    }
}
