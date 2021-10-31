using AlgoSdk;
using AlgoSdk.LowLevel;
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
        var txn = Transaction.Payment(
            sender: (Address)kp.PublicKey,
            txnParams: new TransactionParams
            {
                ConsensusVersion = "https://github.com/algorandfoundation/specs/tree/abc54f79f9ad679d2d22f0fb9909fb005c16f8a1",
                Fee = 0,
                GenesisHash = "m0XImxCpIDneMMenkOlyixCtRKRVd0mbH8vV/QLYB1U=",
                GenesisId = "sandnet-v1",
                MinFee = 1000,
                LastRound = 45666234
            },
            receiver: AlgoSdk.Crypto.Random.Bytes<Address>(),
            amount: 1000000
        ).Sign(kp.SecretKey);
        using var serialized = AlgoApiSerializer.SerializeMessagePack(txn, Allocator.Temp);
        UnityEngine.Debug.Log($"Serialized bytes: {System.Convert.ToBase64String(serialized.ToArray())}");
        var deserialized = AlgoApiSerializer.Deserialize<Signed<PaymentTxn>>(serialized.AsArray(), ContentType.MessagePack);
        Assert.IsTrue(txn.Equals(deserialized));
    }
}
