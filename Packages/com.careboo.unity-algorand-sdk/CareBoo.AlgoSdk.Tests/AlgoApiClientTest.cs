using AlgoSdk;
using NUnit.Framework;
using UnityEngine.Networking;

public abstract class AlgoApiClientTest
{
    protected static void AssertResponseSuccess<T>(AlgoApiResponse<T> response) where T : struct
    {
        Assert.AreEqual(UnityWebRequest.Result.Success, response.Raw.Status, response.Error.Message);
    }

    protected static void AssertResponseSuccess(AlgoApiResponse response)
    {
        Assert.AreEqual(UnityWebRequest.Result.Success, response.Status, response.GetText());
    }
}
