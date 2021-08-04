using AlgoSdk;
using AlgoSdk.Crypto;
using NUnit.Framework;
using Unity.Collections;
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
            sender: AlgoSdk.Crypto.Random.Bytes<Address>().GenerateCheckSum(),
            receiver: AlgoSdk.Crypto.Random.Bytes<Address>().GenerateCheckSum(),
            amount: 1000000);
        var signedTransaction = transaction.Sign(account.SecretKey);
        using var message = transaction.ToMessagePack(Allocator.Temp);
        Assert.IsTrue(signedTransaction.Signature.Verify(message, account.Address));
    }

    [Test]
    public void SizeOfSignedTransactionShouldBeUnder4KB()
    {
        const int MAX_BYTES = 4000;
        var size = UnsafeUtility.SizeOf<RawSignedTransaction>();
        UnityEngine.Debug.Log($"Size of {nameof(RawSignedTransaction)}: {size}");
        Assert.IsTrue(size <= MAX_BYTES);
    }
}
