using System.Collections;
using System.Linq;
using Algorand.Unity;
using Algorand.Unity.Experimental.Abi;
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
            txn.Group = Algorand.Unity.Crypto.Random.Bytes<TransactionId>();
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

    [Test]
    public void BuildingTransactionWithAbiCallsShouldValidateTxnTypes()
    {
        var methodAbi = new Method
        {
            Name = "test",
            Description = "Blah blah blah",
            Arguments = new[]
            {
                new Method.Arg
                {
                    Type = AbiType.Parse("pay"),
                    Name = "a",
                    Description = "some payment txn"
                }
            },
            Returns = new Method.Return
            {
                Type = AbiType.Parse("uint8"),
                Description = "something"
            }
        };

        var sender = Account.GenerateAccount();
        var receiver = Account.GenerateAccount();

        var group = Transaction.Atomic()
            .AddTxn(Transaction.Payment(sender.Address, default, receiver.Address, 100000))
            .AddMethodCall(
                sender.Address,
                default,
                default,
                methodAbi,
                Args.Empty);
    }

    [Test]
    public void BuildingAtomicTxnWithMoreThan15ArgsShouldEncodeTheLastOnesIntoATuple()
    {
        var length = 20;
        var argType = AbiType.UIntN(32);
        var methodAbi = new Method
        {
            Name = "test",
            Description = "Blah blah blah",
            Arguments = Enumerable.Range(0, length).Select(i => new Method.Arg
            {
                Type = argType,
                Name = $"arg{i}",
                Description = $"description{i}"
            }).ToArray(),
            Returns = new Method.Return
            {
                Type = AbiType.Parse("uint8"),
                Description = "something"
            }
        };

        var sender = Account.GenerateAccount();
        var receiver = Account.GenerateAccount();

        var args = new IAbiValue[length];
        for (var i = 0; i < length; i++)
            args[i] = new UInt32((uint)i);

        Assert.AreEqual(methodAbi.Arguments.Length, args.Length);
        var group = Transaction.Atomic()
            .AddTxn(Transaction.Payment(sender.Address, default, receiver.Address, 10000))
            .AddMethodCall(
                sender.Address,
                default,
                default,
                methodAbi,
                args
                );

        Assert.AreEqual(16, group[1].AppArguments.Length);
        Assert.AreEqual((20 - 14) * argType.StaticLength, group[1].AppArguments[15].Bytes.Length);
    }

    [Test]
    public void BuildingAtomicTxnWithReferencesShouldEncodeToUInt8()
    {
        var methodAbi = new Method
        {
            Name = "test",
            Arguments = new[]
            {
                new Method.Arg
                {
                    Type = AbiType.AccountReference
                },
                new Method.Arg
                {
                    Type = AbiType.AssetReference
                },
                new Method.Arg
                {
                    Type = AbiType.ApplicationReference
                },
                new Method.Arg
                {
                    Type = AbiType.AccountReference
                },
                new Method.Arg
                {
                    Type = AbiType.ApplicationReference
                },
            }
        };

        var sender = Account.GenerateAccount();
        AppIndex appId0 = (ulong)UnityEngine.Random.Range(0, int.MaxValue);
        AssetIndex assetId0 = (ulong)UnityEngine.Random.Range(0, int.MaxValue);
        AppIndex appId1 = appId0 + 1;
        var account = Account.GenerateAccount();

        var args = Args.Add(sender.Address).Add(assetId0).Add(appId0).Add(account.Address).Add(appId1);
        var group = Transaction.Atomic()
            .AddMethodCall(sender.Address, default, appId0, methodAbi, args);
        var txn = group[0];
        Assert.AreEqual(methodAbi.Arguments.Length + 1, txn.AppArguments.Length);
        var expectedIndices = new byte[] { 0, 0, 0, 1, 1 };
        for (var i = 1; i < txn.AppArguments.Length; i++)
        {
            Assert.AreEqual(1, txn.AppArguments[i].Bytes.Length);
            Assert.AreEqual(expectedIndices[i - 1], txn.AppArguments[i].Bytes[0]);
        }
        for (var i = 4; i < txn.AppArguments.Length; i++)
        {
            Assert.AreEqual(1, txn.AppArguments[i].Bytes.Length);
        }
        Assert.AreEqual(1, txn.Accounts.Length);
        Assert.AreEqual(1, txn.ForeignApps.Length);
        Assert.AreEqual(1, txn.ForeignAssets.Length);
        Assert.AreEqual((ulong)assetId0, txn.ForeignAssets[0]);
        Assert.AreEqual((ulong)appId1, txn.ForeignApps[0]);
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
