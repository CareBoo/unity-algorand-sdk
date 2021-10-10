using System.Collections;
using AlgoSdk;
using Cysharp.Threading.Tasks;
using NUnit.Framework;
using UnityEngine.TestTools;
using static UnityEngine.Networking.UnityWebRequest;

public class KmdClientKeysTest : KmdClientTestFixture
{
    protected Address generatedKeyAddress;

    protected override async UniTask SetUpAsync()
    {
        await base.SetUpAsync();
        await GenerateKey();
    }

    protected override async UniTask TearDownAsync()
    {
        await base.TearDownAsync();
        await DeleteGeneratedKey();
    }

    protected async UniTask GenerateKey()
    {
        var response = await kmd.GenerateKey(displayMnemonic: true, walletPassword: WalletPassword);
        if (response.Status != Result.Success)
            Assert.Ignore(
                $"Ignoring test because {nameof(GenerateKey)} response was {response.ResponseCode}: {response.Status}. " +
                $"Message:\n{response.Error.Message}"
            );
    }

    protected async UniTask DeleteGeneratedKey()
    {
        if (!generatedKeyAddress.Equals(default))
            await kmd.DeleteKey(
                address: generatedKeyAddress,
                walletHandleToken: walletHandle,
                walletPassword: WalletPassword
            );
        generatedKeyAddress = default;
    }

    [UnityTest]
    public IEnumerator GenerateKeyThenDeleteKeyShouldReturnOkay() => UniTask.ToCoroutine(async () =>
    {
        var generateKeyResponse = await kmd.GenerateKey(walletPassword: WalletPassword);
        AssertResponseSuccess(generateKeyResponse);
        var generatedAddress = generateKeyResponse.Payload.Address;
        var deleteKeyRequest = await kmd.DeleteKey(
            address: generatedAddress,
            walletHandleToken: walletHandle,
            walletPassword: WalletPassword
        );
        AssertResponseSuccess(deleteKeyRequest);
    });


    [UnityTest]
    public IEnumerator ExportKeyShouldReturnOkay() => UniTask.ToCoroutine(async () =>
    {
        var generateKeyResponse = await kmd.GenerateKey(walletPassword: WalletPassword);
        AssertResponseSuccess(generateKeyResponse);
        var generatedAddress = generateKeyResponse.Payload.Address;
        var deleteKeyRequest = await kmd.DeleteKey(
            address: generatedAddress,
            walletHandleToken: walletHandle,
            walletPassword: WalletPassword
        );
        AssertResponseSuccess(deleteKeyRequest);
    });
}
