using AlgoSdk;
using NUnit.Framework;
using Unity.Collections;

public class BlockTest
{
    static readonly byte[] blockMsgPack = System.Convert.FromBase64String("gqVibG9ja94AEKRlYXJuTqRmZWVzxCAH2stLbZ7RQbF1dr1FmuZCHUhto9TvIkfECaOWuC6iIaRmcmFjzwAAAAGhC0Hvo2dlbqpzYW5kbmV0LXYxomdoxCCbRcibEKkgOd4wx6eQ6XKLEK1EpFV3SZsfy9X9AtgHVaRwcmV2xCCzUgTf66QpbzCkzzjHydNEAz4eElrNu/2yNDOijLtOrqVwcm90b9lZaHR0cHM6Ly9naXRodWIuY29tL2FsZ29yYW5kZm91bmRhdGlvbi9zcGVjcy90cmVlL2FiYzU0Zjc5ZjlhZDY3OWQyZDIyZjBmYjk5MDlmYjAwNWMxNmY4YTGkcmF0Zc4O5rJ/o3JuZM0MTKZyd2NhbHLOAAehIKNyd2TEIP//////////////////////////////////////////pHNlZWTEINk59nEYaK04fSD/kZXg4jjiJ2INfRTGDw7UNE9twS0KonRjEaJ0c85hXxTqo3R4bsQg666vUD3zLGr3TVCKOgUPLOrLb8LhOlzB9J2DdLBU5kekdHhuc5GFo2hnacOicnIBonJzzu5v2yGjc2lnxECojukuS0CFYps14otGey+YnCqizYPIE2B4i9Dz1UkV66fMVgKEp6rAzy0/bts+J8a4NMoq6UXjmoOZJFhMSjYOo3R4boijYW10zgABhqCjZmVlzQPoomZ2zQxKomx2zRAypG5vdGXEBWhlbGxvo3JjdsQgiOUaz3f2OvcsaG+9OU2UwgNXMeRwncxoWACeVQ75eMWjc25kxCBONIT7wSbtdxuVQzgrm8uZ+eIpcLoZPxKIcZTMGD0FUKR0eXBlo3BheaRjZXJ0hKRwcm9wg6NkaWfEIFdf2gpux1Ws3VrcONAPFXTldvvsl0flmmAPjmJT+e6EpmVuY2RpZ8QgS0SW4zKR/39j5MwJB+Hr+skeERTUx4iFkxqBhcsvN/Olb3Byb3DEIE40hPvBJu13G5VDOCuby5n54ilwuhk/EohxlMwYPQVQo3JuZM0MTKRzdGVwAqR2b3RlkYOkY3JlZIGicGbEUNEn1WXDwWIsJHrUVBn8T99Pz75h2Kv4OZFAQyqStgr9WVCGjGRIt6PNFEr+354aFX04/UgrxHOR+J+Zb+A1zWXR4AT5UDtLhJmvSHyY2OoBo3NpZ4ahcMQgwXeZFTFcbcjyXJFPceNGbuRwLVHnINhQ/tao+9AmMsSjcDFzxECcjuIWRCzbrBhvY8RQDMl9fLktqlbBMTYtT/V6AvP8Ax+O+ixkJls9LOZLaW5jcUDL78tntCUSZ2rCjoqnD6EEonAyxCAq8QmdJlQ5J9yUDhRn/RQ3YV14o4G5gf+HIsHLR2sSw6NwMnPEQOnw61EVtw9ve2ZUqFxoqtv5EfXsCJNCMAg2XDAc4EiYlZbQvr3ksYP2B7TbrltFtM2+Pp0MMKEJkoL0uWjrBAGicHPEQAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAChc8RALEZ2yaTCxxQtrWZAw9qo7E1ASusXY/6vYAI/liiyh6iEP4ZRGW3m8Mqns95UwIxLxoaVvzUmGBUTSG2JhhRJBKNzbmTEIE40hPvBJu13G5VDOCuby5n54ilwuhk/EohxlMwYPQVQ");

    [Test]
    public void DeserializingBlockFromMessagePackShouldThrowNoErrors()
    {
        using var bytes = new NativeArray<byte>(blockMsgPack, Allocator.Temp);
        AlgoApiSerializer.DeserializeMessagePack<BlockResponse>(bytes);
    }

    [Test]
    public void DefaultFixedStringsShouldBeEqual()
    {
        var fs = new FixedString32Bytes();
        var defaultFs = default(FixedString32Bytes);
        Assert.IsTrue(defaultFs == fs);
    }
}
