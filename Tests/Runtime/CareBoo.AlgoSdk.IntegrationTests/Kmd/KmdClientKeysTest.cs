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
        var (error, keyResponse) = await AlgoApiClientSettings.Kmd.GenerateKey(walletHandleToken: walletHandleToken);
        AssertOkay(error);
        return keyResponse.Address;
    }

    protected async UniTask<AlgoApiResponse> DeleteKey(Address address)
    {
        return await AlgoApiClientSettings.Kmd.DeleteKey(
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
        AssertOkay(response.Error);
    });

    [UnityTest]
    public IEnumerator ExportKeyShouldReturnOkay() => UniTask.ToCoroutine(async () =>
    {
        var address = await GenerateKey();
        var response = await AlgoApiClientSettings.Kmd.ExportKey(address, walletHandleToken, WalletPassword);
        AssertOkay(response.Error);
        await DeleteKey(address);
    });

    [UnityTest]
    public IEnumerator ExportMasterKeyShouldReturnOkay() => UniTask.ToCoroutine(async () =>
    {
        var response = await AlgoApiClientSettings.Kmd.ExportMasterKey(walletHandleToken, WalletPassword);
        AssertOkay(response.Error);
    });

    [UnityTest]
    public IEnumerator ImportKeyShouldReturnOkay() => UniTask.ToCoroutine(async () =>
    {
        var (error, importKeyResponse) = await AlgoApiClientSettings.Kmd.ImportKey(
            AlgoSdk.Crypto.Random.Bytes<PrivateKey>(),
            walletHandleToken
        );
        AssertOkay(error);
        await DeleteKey(importKeyResponse.Address);
    });

    [UnityTest]
    public IEnumerator ListKeysShouldReturnOkay() => UniTask.ToCoroutine(async () =>
    {
        var address = await GenerateKey();
        var (error, listKeysResponse) = await AlgoApiClientSettings.Kmd.ListKeys(walletHandleToken);
        await DeleteKey(address);
        AssertOkay(error);
        var addresses = listKeysResponse.Addresses;
        Assert.IsTrue(addresses.Any(x => x.Equals(address)));
    });
}
