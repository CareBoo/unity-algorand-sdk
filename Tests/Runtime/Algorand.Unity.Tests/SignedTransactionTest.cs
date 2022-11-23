using Algorand.Unity;
using NUnit.Framework;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

public class SignedTransactionTest
{
    [Test]
    public void SizeOfSignedTransactionShouldBeUnder4KB()
    {
        const int MAX_BYTES = 4000;
        var size = UnsafeUtility.SizeOf<SignedTxn>();
        UnityEngine.Debug.Log($"Size of {nameof(SignedTxn)}: {size}");
        Assert.IsTrue(size <= MAX_BYTES);
    }

    [Test]
    public void SerializedSignedTransactionShouldEqualDeserializedSignedTransaction()
    {
        var account = Account.GenerateAccount();
        var txn = Transaction.Payment(
            sender: account.Address,
            txnParams: new TransactionParams
            {
                ConsensusVersion = "https://github.com/algorandfoundation/specs/tree/abc54f79f9ad679d2d22f0fb9909fb005c16f8a1",
                Fee = 0,
                GenesisHash = "m0XImxCpIDneMMenkOlyixCtRKRVd0mbH8vV/QLYB1U=",
                GenesisId = "sandnet-v1",
                MinFee = 1000,
                LastRound = 45666234
            },
            receiver: Algorand.Unity.Crypto.Random.Bytes<Address>(),
            amount: 1000000
        );
        var signedTxn = account.SignTxn(txn);
        using var serialized = AlgoApiSerializer.SerializeMessagePack(signedTxn, Allocator.Persistent);
        UnityEngine.Debug.Log($"Serialized bytes: {System.Convert.ToBase64String(serialized.ToArray())}");
        var deserialized = AlgoApiSerializer.Deserialize<SignedTxn<PaymentTxn>>(serialized.AsArray(), ContentType.MessagePack);
        Assert.IsTrue(signedTxn.Equals(deserialized));
    }
}
