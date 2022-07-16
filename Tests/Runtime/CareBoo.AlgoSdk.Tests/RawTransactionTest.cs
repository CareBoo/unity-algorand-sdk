using AlgoSdk;
using AlgoSdk.Crypto;
using NUnit.Framework;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class RawTransactionTest
{
    [Test]
    public void SerializedTransactionShouldEqualDeserializedTransaction()
    {
        var transaction = new Transaction
        {
            Fee = 32,
            FirstValidRound = 1009,
            GenesisHash = AlgoSdk.Crypto.Random.Bytes<Sha512_256_Hash>(),
            LastValidRound = 10021,
            Sender = AlgoSdk.Crypto.Random.Bytes<Address>(),
            TransactionType = TransactionType.Payment,
            Receiver = AlgoSdk.Crypto.Random.Bytes<Address>(),
            Amount = 40000
        };
        using var bytes = AlgoApiSerializer.SerializeMessagePack(transaction, Allocator.Persistent);
        Debug.Log(System.Convert.ToBase64String(bytes.ToArray()));
        var deserialized = AlgoApiSerializer.DeserializeMessagePack<Transaction>(bytes.AsArray());
        Assert.IsTrue(transaction.Equals(deserialized));
    }

    [Test]
    public void SizeOfRawTransactionShouldBeUnder4KB()
    {
        const int MAX_BYTES = 4000;
        var size = UnsafeUtility.SizeOf<Transaction>();
        UnityEngine.Debug.Log($"Size of {nameof(Transaction)}: {size}");
        Assert.IsTrue(size <= MAX_BYTES);
    }
}
