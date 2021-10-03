using AlgoSdk;
using AlgoSdk.Crypto;
using NUnit.Framework;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

public class SignedTransactionTest
{
    [Test]
    public void SizeOfSignedTransactionShouldBeUnder4KB()
    {
        const int MAX_BYTES = 4000;
        var size = UnsafeUtility.SizeOf<SignedTransaction>();
        UnityEngine.Debug.Log($"Size of {nameof(SignedTransaction)}: {size}");
        Assert.IsTrue(size <= MAX_BYTES);
    }

    [Test]
    public void SerializedSignedTransactionShouldEqualDeserializedSignedTransaction()
    {
        var seed = AlgoSdk.Crypto.Random.Bytes<PrivateKey>();
        using var kp = seed.ToKeyPair();
        var transaction = new Transaction.Payment(
            fee: 40000,
            firstValidRound: 123,
            genesisHash: AlgoSdk.Crypto.Random.Bytes<Sha512_256_Hash>(),
            lastValidRound: 124,
            sender: (Address)kp.PublicKey,
            receiver: AlgoSdk.Crypto.Random.Bytes<Address>().GenerateCheckSum(),
            amount: 1000000);
        SignedTransaction signedTransaction = transaction.Sign(kp.SecretKey);
        using var serialized = new NativeList<byte>(Allocator.Temp);
        AlgoApiSerializer.SerializeMessagePack(signedTransaction, serialized);
        var deserialized = AlgoApiSerializer.Deserialize<SignedTransaction>(serialized.AsArray().AsReadOnly(), AlgoApiFormat.MessagePack);
        Assert.IsTrue(signedTransaction.Equals(deserialized));
    }
}
