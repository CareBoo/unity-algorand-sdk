using Algorand.Unity;
using Algorand.Unity.WalletConnect;
using NUnit.Framework;

[TestFixture]
public class NetworkMessageTest
{
    [Test]
    public void NetworkMessageShouldDeserializeFromJsonProperly()
    {
        var expected = new NetworkMessage
        {
            Topic = "4d83acea-ff18-4409-8b0c-0ad2e56846ca",
            Type = "pub",
            Payload = @"{""data"":""821d513e79c0d5d20ca5763a7c9c9ef90399acdcc696d2057641e3fcd140db08f78bfc58f943a94af3c63d8049b42ce41d43557e0326d391e921fc90339876824ceda8793b6f673e91c20ce4d1beca79f5ebe969baddef885c15e393b7ed734b234ff291d1e2ed18cb3e3a4bef3b44a51986fcfd78f34e15db69e83ab0fcc18bb41c9dbf3d5444afa9dff7795fb709698981102bd58fc534bf4cb61167bc38477a6ff02908f3292e84cf782c8f367ae4c54dd3304403e459d56cdecadeadb84781f358d0c33862bd9df573bc5908247a"",""hmac"":""78b397e3503175891e00da8708ab35104f0838e4910e7db5979f3c1710f98e40"",""iv"":""12a0fbda5d7cdca8a7eaacf34055c106""}"
        };
        var json =
@"{
    ""topic"": ""4d83acea-ff18-4409-8b0c-0ad2e56846ca"",
    ""type"": ""pub"",
    ""payload"": ""{\""data\"":\""821d513e79c0d5d20ca5763a7c9c9ef90399acdcc696d2057641e3fcd140db08f78bfc58f943a94af3c63d8049b42ce41d43557e0326d391e921fc90339876824ceda8793b6f673e91c20ce4d1beca79f5ebe969baddef885c15e393b7ed734b234ff291d1e2ed18cb3e3a4bef3b44a51986fcfd78f34e15db69e83ab0fcc18bb41c9dbf3d5444afa9dff7795fb709698981102bd58fc534bf4cb61167bc38477a6ff02908f3292e84cf782c8f367ae4c54dd3304403e459d56cdecadeadb84781f358d0c33862bd9df573bc5908247a\"",\""hmac\"":\""78b397e3503175891e00da8708ab35104f0838e4910e7db5979f3c1710f98e40\"",\""iv\"":\""12a0fbda5d7cdca8a7eaacf34055c106\""}""
}";
        var actual = AlgoApiSerializer.DeserializeJson<NetworkMessage>(json);
        UnityEngine.Debug.Log(AlgoApiSerializer.SerializeJson(expected));
        Assert.AreEqual(expected, actual);
    }
}
