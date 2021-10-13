using System.Collections;
using System.Linq;
using System.Text;
using AlgoSdk;
using AlgoSdk.Crypto;
using Cysharp.Threading.Tasks;
using NUnit.Framework;
using UnityEngine.TestTools;

public class KmdClientSigTest : KmdClientTestFixture
{
    PrivateKey[] privateKeys;
    Address msigAddress;
    MultiSig msig;

    protected override async UniTask SetUpAsync()
    {
        await base.SetUpAsync();
        privateKeys = Enumerable.Range(0, 3)
            .Select(x => AlgoSdk.Crypto.Random.Bytes<PrivateKey>())
            .ToArray();
        msig = new MultiSig
        {
            SubSignatures = privateKeys
                .Select(x => new MultiSig.SubSignature { PublicKey = x.ToPublicKey() })
                .ToArray(),
            Version = 1,
            Threshold = 2,
        };
        var importResponse = await kmd.ImportMultiSig(
            publicKeys: msig.SubSignatures.Select(x => x.PublicKey).ToArray(),
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
            await kmd.DeleteMultiSig(msigAddress, walletHandleToken, WalletPassword);
            await UniTask.WhenAll(
                privateKeys.Select(key => kmd.DeleteKey(key.ToAddress(), walletHandleToken, WalletPassword))
            );
        }
        await base.TearDownAsync();
    }

    protected Transaction GetTransaction()
    {
        return new Transaction.Payment(
            fee: 10000,
            firstValidRound: 3000,
            lastValidRound: 4000,
            genesisHash: AlgoSdk.Crypto.Random.Bytes<Sha512_256_Hash>(),
            sender: msigAddress,
            receiver: AlgoSdk.Crypto.Random.Bytes<Address>().GenerateCheckSum(),
            amount: 30000
        )
        {
            Note = Encoding.UTF8.GetBytes("hello"),
            GenesisId = "hello world genesis"
        }.ToRaw();
    }

    [UnityTest]
    public IEnumerator ExportMultiSigShouldReturnOkay() => UniTask.ToCoroutine(async () =>
    {
        var response = await kmd.ExportMultiSig(msigAddress, walletHandleToken);
        AssertResponseSuccess(response);
    });

    [UnityTest]
    public IEnumerator ListMultiSigShouldReturnOkay() => UniTask.ToCoroutine(async () =>
    {
        var response = await kmd.ListMultiSig(walletHandleToken);
        AssertResponseSuccess(response);
        var addresses = response.Payload.Addresses;
        Assert.IsTrue(addresses.Any(a => a.Equals(msigAddress)));
    });


    [UnityTest]
    public IEnumerator SignMultiSigShouldReturnOkay() => UniTask.ToCoroutine(async () =>
    {
        using var keyPair = privateKeys[0].ToKeyPair();
        var txn = GetTransaction();
        var pubKeyAddress = (Address)keyPair.PublicKey;
        var txnBytes = AlgoApiSerializer.SerializeMessagePack(txn);
        var response = await kmd.SignMultiSig(
            multiSig: msig,
            publicKey: keyPair.PublicKey,
            transaction: txnBytes,
            walletHandleToken: walletHandleToken,
            walletPassword: WalletPassword
        );
        AssertResponseSuccess(response);
    });


    [UnityTest]
    public IEnumerator SignProgramMultiSigShouldReturnOkay() => UniTask.ToCoroutine(async () =>
    {
        using var keyPair = privateKeys[0].ToKeyPair();
        var txn = GetTransaction();
        var pubKeyAddress = (Address)keyPair.PublicKey;
        txn.Sender = keyPair.PublicKey;
        var txnBytes = AlgoApiSerializer.SerializeMessagePack(txn);
        var response = await kmd.SignProgramMultiSig(
            address: msigAddress,
            multiSig: msig,
            publicKey: keyPair.PublicKey,
            data: txnBytes,
            walletHandleToken: walletHandleToken,
            walletPassword: WalletPassword
        );
        AssertResponseSuccess(response);
    });


    [UnityTest]
    public IEnumerator SignTransactionShouldReturnOkay() => UniTask.ToCoroutine(async () =>
    {
        var txn = AlgoApiSerializer.SerializeMessagePack(GetTransaction());
        Ed25519.PublicKey pk = privateKeys[0].ToPublicKey();
        var signResponse = await kmd.SignTransaction(
            publicKey: pk,
            transaction: txn,
            walletHandleToken: walletHandleToken,
            walletPassword: WalletPassword
        );
        AssertResponseSuccess(signResponse);
    });

    [UnityTest]
    public IEnumerator SignProgramShouldReturnOkay() => UniTask.ToCoroutine(async () =>
    {
        var txn = AlgoApiSerializer.SerializeMessagePack(GetTransaction());
        Ed25519.PublicKey pk = privateKeys[0].ToPublicKey();
        var signResponse = await kmd.SignProgram(
            address: pk,
            data: txn,
            walletHandleToken: walletHandleToken,
            walletPassword: WalletPassword
        );
        AssertResponseSuccess(signResponse);
    });
}
