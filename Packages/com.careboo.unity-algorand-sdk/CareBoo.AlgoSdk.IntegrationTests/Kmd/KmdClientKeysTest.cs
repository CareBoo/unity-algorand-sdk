using System.Collections;
using System.Linq;
using AlgoSdk;
using Cysharp.Threading.Tasks;
using NUnit.Framework;
using UnityEngine.TestTools;

public class KmdClientKeysTest : KmdClientTestFixture
{
    protected async UniTask<Address> GenerateKey()
    {
        var response = await kmd.GenerateKey(walletHandleToken: walletHandleToken);
        AssertResponseSuccess(response);
        return response.Payload.Address;
    }

    protected async UniTask<AlgoApiResponse> DeleteKey(Address address)
    {
        return await kmd.DeleteKey(
            address: address,
            walletHandleToken: walletHandleToken,
            walletPassword: WalletPassword
        );
    }

    [UnityTest]
    public IEnumerator DeleteKeyShouldReturnOkay() => UniTask.ToCoroutine(async () =>
    {
        var address = await GenerateKey();
        var response = await DeleteKey(address);
        AssertResponseSuccess(response);
    });

    [UnityTest]
    public IEnumerator ExportKeyShouldReturnOkay() => UniTask.ToCoroutine(async () =>
    {
        var address = await GenerateKey();
        var response = await kmd.ExportKey(address, walletHandleToken, WalletPassword);
        AssertResponseSuccess(response);
        await DeleteKey(address);
    });

    [UnityTest]
    public IEnumerator ExportMasterKeyShouldReturnOkay() => UniTask.ToCoroutine(async () =>
    {
        var response = await kmd.ExportMasterKey(walletHandleToken, WalletPassword);
        AssertResponseSuccess(response);
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

    [UnityTest]
    public IEnumerator ListKeysShouldReturnOkay() => UniTask.ToCoroutine(async () =>
    {
        var address = await GenerateKey();
        var response = await kmd.ListKeys(walletHandleToken);
        await DeleteKey(address);
        AssertResponseSuccess(response);
        var addresses = response.Payload.Addresses;
        Assert.IsTrue(addresses.Any(x => x.Equals(address)));
    });
}
