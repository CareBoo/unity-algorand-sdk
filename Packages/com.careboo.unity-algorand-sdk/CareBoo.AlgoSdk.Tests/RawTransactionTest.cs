using AlgoSdk;
using AlgoSdk.Crypto;
using AlgoSdk.MsgPack;
using MessagePack;
using NUnit.Framework;
using Unity.Collections.LowLevel.Unsafe;

public class RawTransactionTest
{
    [Test]
    public void SerializedTransactionShouldEqualDeserializedTransaction()
    {
        var transaction = new RawTransaction();
        transaction.Fee = 32;
        transaction.FirstValidRound = 1009;
        transaction.GenesisHash = AlgoSdk.Crypto.Random.Bytes<Sha512_256_Hash>();
        transaction.LastValidRound = 10021;
        transaction.Sender = AlgoSdk.Crypto.Random.Bytes<Address>().GenerateCheckSum();
        transaction.TransactionType = TransactionType.Payment;
        transaction.Receiver = AlgoSdk.Crypto.Random.Bytes<Address>().GenerateCheckSum();
        transaction.Amount = 40000;
        var bytes = MessagePackSerializer.Serialize(transaction, Config.Options);
        var json = MessagePackSerializer.ConvertToJson(bytes, Config.Options);
        var deserialized = MessagePackSerializer.Deserialize<RawTransaction>(bytes, Config.Options);
        Assert.AreEqual(transaction, deserialized);
    }

    [Test]
    public void SizeOfRawTransactionShouldBeUnder4KB()
    {
        const int MAX_BYTES = 4000;
        var size = UnsafeUtility.SizeOf<RawTransaction>();
        UnityEngine.Debug.Log($"Size of {nameof(RawTransaction)}: {size}");
        Assert.IsTrue(size <= MAX_BYTES);
    }
}
