using System.Collections;
using System.Linq;
using AlgoSdk;
using AlgoSdk.Crypto;
using AlgoSdk.Kmd;
using Cysharp.Threading.Tasks;
using NUnit.Framework;
using Unity.Collections;
using UnityEngine.TestTools;

public class KmdClientSigTest : KmdClientTestFixture
{
    PrivateKey[] privateKeys;
    Address msigAddress;
    MultisigSig msig;

    protected override async UniTask SetUpAsync()
    {
        await base.SetUpAsync();
        privateKeys = Enumerable.Range(0, 3)
            .Select(x => AlgoSdk.Crypto.Random.Bytes<PrivateKey>())
            .ToArray();
        msig = new MultisigSig
        {
            Subsigs = privateKeys
                .Select(x => new MultisigSig.Subsig { PublicKey = x.ToPublicKey() })
                .ToArray(),
            Version = 1,
            Threshold = 2,
        };
        var importResponse = await AlgoApiClientSettings.Kmd.ImportMultisig(
            publicKeys: msig.Subsigs.Select(x => x.PublicKey).ToArray(),
            version: msig.Version,
            threshold: msig.Threshold,
            walletHandleToken: walletHandleToken
        );
        msigAddress = importResponse.Payload.Address;
        var importKeys = await UniTask.WhenAll(
            privateKeys.Select(key => ImportKey(key, walletHandleToken)).ToArray()
        );
    }

    protected override async UniTask TearDownAsync()
    {
        if (!msigAddress.Equals(default))
        {
            await AlgoApiClientSettings.Kmd.DeleteMultisig(msigAddress, walletHandleToken, WalletPassword);
            await UniTask.WhenAll(
                privateKeys.Select(key => DeleteKey(key, walletHandleToken))
            );
        }
        await base.TearDownAsync();
    }

    protected async UniTask<PaymentTxn> GetTransaction()
    {
        var txnParams = (await AlgoApiClientSettings.Algod.TransactionParams()).Payload;
        return Transaction.Payment(
            sender: msigAddress,
            txnParams: txnParams,
            receiver: AlgoSdk.Crypto.Random.Bytes<Address>(),
            amount: 30000
        );
    }

    async UniTask<AlgoApiResponse<ImportKeyResponse>> ImportKey(PrivateKey key, FixedString128Bytes walletHandleToken) =>
        await AlgoApiClientSettings.Kmd.ImportKey(key, walletHandleToken);

    async UniTask<AlgoApiResponse<ImportKeyResponse>> DeleteKey(PrivateKey key, FixedString128Bytes walletHandleToken) =>
        await AlgoApiClientSettings.Kmd.DeleteKey(key.ToAddress(), walletHandleToken, WalletPassword);

    [UnityTest]
    public IEnumerator ExportMultisigShouldReturnOkay() => UniTask.ToCoroutine(async () =>
    {
        var response = await AlgoApiClientSettings.Kmd.ExportMultisig(msigAddress, walletHandleToken);
        AssertOkay(response.Error);
    });

    [UnityTest]
    public IEnumerator ListMultisigShouldReturnOkay() => UniTask.ToCoroutine(async () =>
    {
        var response = await AlgoApiClientSettings.Kmd.ListMultisig(walletHandleToken);
        AssertOkay(response.Error);
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
        var response = await AlgoApiClientSettings.Kmd.SignMultisig(
            msig: msig,
            publicKey: keyPair.PublicKey,
            transactionData: txnBytes,
            walletHandleToken: walletHandleToken,
            walletPassword: WalletPassword
        );
        AssertOkay(response.Error);
    });


    [UnityTest]
    public IEnumerator SignProgramMultisigShouldReturnOkay() => UniTask.ToCoroutine(async () =>
    {
        using var keyPair = privateKeys[0].ToKeyPair();
        var txn = await GetTransaction();
        var pubKeyAddress = (Address)keyPair.PublicKey;
        txn.Sender = keyPair.PublicKey;
        var txnBytes = AlgoApiSerializer.SerializeMessagePack(txn);
        var response = await AlgoApiClientSettings.Kmd.SignProgramMultisig(
            msigAccount: msigAddress,
            msig: msig,
            publicKey: keyPair.PublicKey,
            programData: txnBytes,
            walletHandleToken: walletHandleToken,
            walletPassword: WalletPassword
        );
        AssertOkay(response.Error);
    });


    [UnityTest]
    public IEnumerator SignTransactionShouldReturnOkay() => UniTask.ToCoroutine(async () =>
    {
        var txn = AlgoApiSerializer.SerializeMessagePack(await GetTransaction());
        Ed25519.PublicKey pk = privateKeys[0].ToPublicKey();
        var signResponse = await AlgoApiClientSettings.Kmd.SignTransaction(
            pk,
            txn,
            walletHandleToken,
            WalletPassword
        );
        AssertOkay(signResponse.Error);
    });

    [UnityTest]
    public IEnumerator SignProgramShouldReturnOkay() => UniTask.ToCoroutine(async () =>
    {
        var txn = AlgoApiSerializer.SerializeMessagePack(await GetTransaction());
        Ed25519.PublicKey pk = privateKeys[0].ToPublicKey();
        var signResponse = await AlgoApiClientSettings.Kmd.SignProgram(pk, txn, walletHandleToken, WalletPassword);
        AssertOkay(signResponse.Error);
    });
}
