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

    protected override async UniTask SetUpAsync()
    {
        await base.SetUpAsync();
        privateKeys = Enumerable.Range(0, 3)
            .Select(x => AlgoSdk.Crypto.Random.Bytes<PrivateKey>())
            .ToArray();
        var importResponse = await kmd.ImportMultiSig(
            publicKeys: privateKeys.Select(sk => sk.ToPublicKey()).ToArray(),
            version: 1,
            threshold: 2,
            walletHandleToken: walletHandleToken
        );
        msigAddress = importResponse.Payload.Address;
    }

    protected override async UniTask TearDownAsync()
    {
        if (!msigAddress.Equals(default))
            await kmd.DeleteMultiSig(msigAddress, walletHandleToken, WalletPassword);
        await base.TearDownAsync();
    }

    protected Transaction GetTransaction()
    {
        var txnPay = new Transaction.Payment(
            fee: 10000,
            firstValidRound: 3000,
            lastValidRound: 4000,
            genesisHash: AlgoSdk.Crypto.Random.Bytes<Sha512_256_Hash>(),
            sender: AlgoSdk.Crypto.Random.Bytes<Address>().GenerateCheckSum(),
            receiver: AlgoSdk.Crypto.Random.Bytes<Address>().GenerateCheckSum(),
            amount: 30000
        )
        {
            Note = Encoding.UTF8.GetBytes("hello"),
            GenesisId = "hello world genesis"
        };
        Transaction rawTxn = default;
        txnPay.CopyTo(ref rawTxn);
        return rawTxn;
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
        var txnBytes = AlgoApiSerializer.SerializeMessagePack(txn);
        var sig = txn.Sign(keyPair.SecretKey).Signature.Sig;
        var response = await kmd.SignMultiSig(
            new MultiSig
            {
                Threshold = 2,
                SubSignatures = privateKeys
                    .Select(x => x.ToPublicKey())
                    .Select(x => new MultiSig.SubSignature { PublicKey = x })
                    .ToArray(),
                Version = 1,
            },
            keyPair.PublicKey,
            msigAddress,
            txnBytes,
            walletHandleToken,
            WalletPassword
        );
        AssertResponseSuccess(response);
    });
}
