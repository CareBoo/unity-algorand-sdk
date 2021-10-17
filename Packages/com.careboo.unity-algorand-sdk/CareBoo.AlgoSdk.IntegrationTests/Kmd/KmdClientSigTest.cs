using System.Collections;
using System.Linq;
using AlgoSdk;
using AlgoSdk.Crypto;
using Cysharp.Threading.Tasks;
using NUnit.Framework;
using UnityEngine.TestTools;

public class KmdClientSigTest : KmdClientTestFixture
{
    PrivateKey[] privateKeys;
    Address msigAddress;
    Multisig msig;

    protected override async UniTask SetUpAsync()
    {
        await base.SetUpAsync();
        privateKeys = Enumerable.Range(0, 3)
            .Select(x => AlgoSdk.Crypto.Random.Bytes<PrivateKey>())
            .ToArray();
        msig = new Multisig
        {
            Subsigs = privateKeys
                .Select(x => new Multisig.Subsig { PublicKey = x.ToPublicKey() })
                .ToArray(),
            Version = 1,
            Threshold = 2,
        };
        var importResponse = await kmd.ImportMultisig(
            publicKeys: msig.Subsigs.Select(x => x.PublicKey).ToArray(),
            version: msig.Version,
            threshold: msig.Threshold,
            walletHandleToken: walletHandleToken
        );
        msigAddress = importResponse.Payload.Address;
        var importKeys = await UniTask.WhenAll(
            privateKeys.Select(key => kmd.ImportKey(key, walletHandleToken)).ToArray()
        );
    }

    protected override async UniTask TearDownAsync()
    {
        if (!msigAddress.Equals(default))
        {
            await kmd.DeleteMultisig(msigAddress, walletHandleToken, WalletPassword);
            await UniTask.WhenAll(
                privateKeys.Select(key => kmd.DeleteKey(key.ToAddress(), walletHandleToken, WalletPassword))
            );
        }
        await base.TearDownAsync();
    }

    protected async UniTask<PaymentTxn> GetTransaction()
    {
        var txnParams = (await algod.GetSuggestedParams()).Payload;
        return Transaction.Payment(
            sender: msigAddress,
            txnParams: txnParams,
            receiver: AlgoSdk.Crypto.Random.Bytes<Address>(),
            amount: 30000
        );
    }

    [UnityTest]
    public IEnumerator ExportMultisigShouldReturnOkay() => UniTask.ToCoroutine(async () =>
    {
        var response = await kmd.ExportMultisig(msigAddress, walletHandleToken);
        AssertResponseSuccess(response);
    });

    [UnityTest]
    public IEnumerator ListMultisigShouldReturnOkay() => UniTask.ToCoroutine(async () =>
    {
        var response = await kmd.ListMultisig(walletHandleToken);
        AssertResponseSuccess(response);
        var addresses = response.Payload.Addresses;
        Assert.IsTrue(addresses.Any(a => a.Equals(msigAddress)));
    });


    [UnityTest]
    public IEnumerator SignMultisigShouldReturnOkay() => UniTask.ToCoroutine(async () =>
    {
        using var keyPair = privateKeys[0].ToKeyPair();
        var txn = await GetTransaction();
        var pubKeyAddress = (Address)keyPair.PublicKey;
        var txnBytes = AlgoApiSerializer.SerializeMessagePack(txn);
        var response = await kmd.SignMultisig(
            msig: msig,
            publicKey: keyPair.PublicKey,
            transactionData: txnBytes,
            walletHandleToken: walletHandleToken,
            walletPassword: WalletPassword
        );
        AssertResponseSuccess(response);
    });


    [UnityTest]
    public IEnumerator SignProgramMultisigShouldReturnOkay() => UniTask.ToCoroutine(async () =>
    {
        using var keyPair = privateKeys[0].ToKeyPair();
        var txn = await GetTransaction();
        var pubKeyAddress = (Address)keyPair.PublicKey;
        txn.Sender = keyPair.PublicKey;
        var txnBytes = AlgoApiSerializer.SerializeMessagePack(txn);
        var response = await kmd.SignProgramMultisig(
            msigAccount: msigAddress,
            msig: msig,
            publicKey: keyPair.PublicKey,
            programData: txnBytes,
            walletHandleToken: walletHandleToken,
            walletPassword: WalletPassword
        );
        AssertResponseSuccess(response);
    });


    [UnityTest]
    public IEnumerator SignTransactionShouldReturnOkay() => UniTask.ToCoroutine(async () =>
    {
        var txn = AlgoApiSerializer.SerializeMessagePack(await GetTransaction());
        Ed25519.PublicKey pk = privateKeys[0].ToPublicKey();
        var signResponse = await kmd.SignTransaction(
            pk,
            txn,
            walletHandleToken,
            WalletPassword
        );
        AssertResponseSuccess(signResponse);
    });

    [UnityTest]
    public IEnumerator SignProgramShouldReturnOkay() => UniTask.ToCoroutine(async () =>
    {
        var txn = AlgoApiSerializer.SerializeMessagePack(await GetTransaction());
        Ed25519.PublicKey pk = privateKeys[0].ToPublicKey();
        var signResponse = await kmd.SignProgram(pk, txn, walletHandleToken, WalletPassword);
        AssertResponseSuccess(signResponse);
    });
}
