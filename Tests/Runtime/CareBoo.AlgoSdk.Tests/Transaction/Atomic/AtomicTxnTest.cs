using System.Collections;
using AlgoSdk;
using Cysharp.Threading.Tasks;
using NUnit.Framework;
using UnityEngine.TestTools;

[TestFixture]
public class AtomicTxnTest
{
    [Test]
    public void AddingATxnWithANonEmptyGroupIdShouldThrowAnException()
    {
        Assert.Throws<System.ArgumentException>(() =>
        {
            var txn = Transaction.Payment(default, default, default, 100);
            txn.Group = AlgoSdk.Crypto.Random.Bytes<TransactionId>();
            Transaction.Atomic().AddTxn(txn);
        });
    }

    [Test]
    public void AddingMoreThanMaxTxnsShouldThrowAnException()
    {
        var group = Transaction.Atomic();
        var txn = Transaction.Payment(default, default, default, 100);
        for (var i = 0; i < AtomicTxn.MaxNumTxns; i++)
            group.AddTxn(txn);
        Assert.Throws<System.NotSupportedException>(() =>
        {
            group.AddTxn(txn);
        });
    }

    [Test]
    public void BuildingAnAtomicTxnShouldReturnOneReadyForSigningWithTheSameNumberOfTxns(
        [Values(1, 2, 3, 4, 5)] int expected
    )
    {
        var group = Transaction.Atomic();
        var txn = Transaction.Payment(default, default, default, 100);
        for (var i = 0; i < expected; i++)
            group.AddTxn(txn);

        var actual = group.Build().Txns.Length;
        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void SigningWithAccountSignerShouldSignOnlyIndicesThatHaveSameSender()
    {
        var sender = Account.GenerateAccount();
        var receiver = Account.GenerateAccount();
        var group = Transaction.Atomic()
            .AddTxn(Transaction.Payment(sender.Address, default, receiver.Address, 100000))
            .AddTxn(Transaction.Payment(receiver.Address, default, sender.Address, 100))
            .Build()
            .SignWith(sender);

        var expected = TxnIndices.Select(0);
        var actual = group.SignedTxnIndices;
        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void SigningWithAccountAsyncSignerShouldSignOnlyIndicesThatHaveSameSender()
    {
        var sender = Account.GenerateAccount();
        var receiver = Account.GenerateAccount();
        var group = Transaction.Atomic()
            .AddTxn(Transaction.Payment(sender.Address, default, receiver.Address, 100000))
            .AddTxn(Transaction.Payment(receiver.Address, default, sender.Address, 100))
            .Build()
            .SignWithAsync(sender);

        var expected = TxnIndices.Select(0);
        var actual = group.SignedTxnIndices;
        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void FinishSigningWithoutAllSignaturesShouldThrowException()
    {
        var sender = Account.GenerateAccount();
        var receiver = Account.GenerateAccount();
        var group = Transaction.Atomic()
            .AddTxn(Transaction.Payment(sender.Address, default, receiver.Address, 100000))
            .AddTxn(Transaction.Payment(receiver.Address, default, sender.Address, 100))
            .Build()
            .SignWith(sender);

        Assert.Throws<System.InvalidOperationException>(() =>
        {
            group.FinishSigning();
        });
    }

    [UnityTest]
    public IEnumerator FinishSigningAsyncWithoutAllSignaturesShouldThrowException()
    {
        var sender = Account.GenerateAccount();
        var receiver = Account.GenerateAccount();
        var group = Transaction.Atomic()
            .AddTxn(Transaction.Payment(sender.Address, default, receiver.Address, 100000))
            .AddTxn(Transaction.Payment(receiver.Address, default, sender.Address, 100))
            .Build()
            .SignWithAsync(sender);

        System.InvalidOperationException actual = null;
        var task = group.FinishSigningAsync().ToCoroutine(exceptionHandler: (ex) => actual = (System.InvalidOperationException)ex);
        yield return task;
        Assert.NotNull(actual);
    }
}
