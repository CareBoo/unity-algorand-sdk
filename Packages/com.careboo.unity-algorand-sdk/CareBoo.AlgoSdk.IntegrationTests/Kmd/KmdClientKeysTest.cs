using System.Collections;
using AlgoSdk;
using Cysharp.Threading.Tasks;
using UnityEngine.TestTools;

public class KmdClientKeysTest : KmdClientTestFixture
{
    protected async UniTask<Address> GenerateKey()
    {
        var response = await kmd.GenerateKey(walletHandleToken: walletHandleToken);
        AssertResponseSuccess(response);
        return response.Payload.Address;
    }

    protected async UniTask DeleteKey(Address address)
    {
        await kmd.DeleteKey(
            address: address,
            walletHandleToken: walletHandleToken,
            walletPassword: WalletPassword
        );
    }

    [UnityTest]
    public IEnumerator ExportKeyShouldReturnOkay() => UniTask.ToCoroutine(async () =>
    {
        var address = await GenerateKey();
        var response = await kmd.ExportKey(address, walletHandleToken, WalletPassword);
        AssertResponseSuccess(response);
        await DeleteKey(address);
    });

    [UnityTest]
    public IEnumerator ImportKeyShouldReturnOkay() => UniTask.ToCoroutine(async () =>
    {
        var response = await kmd.ImportKey(
            AlgoSdk.Crypto.Random.Bytes<PrivateKey>(),
            walletHandleToken
        );
        AssertResponseSuccess(response);
        var address = response.Payload.Address;
        await DeleteKey(address);
    });
}
