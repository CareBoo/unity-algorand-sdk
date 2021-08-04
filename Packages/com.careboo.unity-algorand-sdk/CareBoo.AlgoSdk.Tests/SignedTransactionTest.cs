using System.Collections;
using System.Collections.Generic;
using AlgoSdk;
using AlgoSdk.Crypto;
using NUnit.Framework;
using Unity.Collections;

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
}
