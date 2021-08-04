using AlgoSdk;
using AlgoSdk.Crypto;
using AlgoSdk.MsgPack;
using MessagePack;
using NUnit.Framework;
using Unity.Collections.LowLevel.Unsafe;

public class SignedTransactionTest
{
    [Test]
    public void SignedTransactionShouldBeVerifiable()
    {
        using var account = Account.Generate();
        var transaction = new Transaction.Payment(
            fee: 40000,
            firstValidRound: 123,
            genesisHash: AlgoSdk.Crypto.Random.Bytes<Sha512_256_Hash>(),
            lastValidRound: 124,
            sender: account.Address,
            receiver: AlgoSdk.Crypto.Random.Bytes<Address>().GenerateCheckSum(),
            amount: 1000000);
        var signedTransaction = transaction.Sign(account.SecretKey);
        Assert.IsTrue(signedTransaction.Verify());
    }

    [Test]
    public void SizeOfSignedTransactionShouldBeUnder4KB()
    {
        const int MAX_BYTES = 4000;
        var size = UnsafeUtility.SizeOf<RawSignedTransaction>();
        UnityEngine.Debug.Log($"Size of {nameof(RawSignedTransaction)}: {size}");
        Assert.IsTrue(size <= MAX_BYTES);
    }

    [Test]
    public void SerializedSignedTransactionShouldEqualDeserializedSignedTransaction()
    {
        using var account = Account.Generate();
        var transaction = new Transaction.Payment(
            fee: 40000,
            firstValidRound: 123,
            genesisHash: AlgoSdk.Crypto.Random.Bytes<Sha512_256_Hash>(),
            lastValidRound: 124,
            sender: account.Address,
            receiver: AlgoSdk.Crypto.Random.Bytes<Address>().GenerateCheckSum(),
            amount: 1000000);
        var signedTransaction = transaction.Sign(account.SecretKey);
        var serialized = MessagePackSerializer.Serialize(signedTransaction, Config.Options);
        var deserialized = MessagePackSerializer.Deserialize<SignedTransaction<Transaction.Payment>>(serialized, Config.Options);
        Assert.IsTrue(signedTransaction.Equals(deserialized));
    }
}
