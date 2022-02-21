
using System;
using AlgoSdk;
using AlgoSdk.LowLevel;
using AlgoSdk.WalletConnect;
using NUnit.Framework;
using Unity.Collections;

[TestFixture]
public class EitherTest
{
    [Test]
    public void JsonOfType2ShouldDeserializeToEitherWithValue2()
    {
        var expected = new JsonRpcRequest
        {
            Id = (ulong)UnityEngine.Random.Range(1, int.MaxValue),
            JsonRpc = "2.0",
            Method = $"test_{Guid.NewGuid()}",
            Params = new AlgoApiObject[] { @"""test""" }
        };
        var json = AlgoApiSerializer.SerializeJson(expected);
        var either = AlgoApiSerializer.DeserializeJson<Either<JsonRpcResponse, JsonRpcRequest>>(json);
        var actual = either.Value2;
        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void JsonOfType1ShouldDeserializeToEitherWithValue1()
    {
        using var secret = new NativeByteArray(32, Allocator.Temp);
        var size = UnityEngine.Random.Range(1, 10);

        var expected = new byte[size][];
        for (var i = 0; i < size; i++)
        {
            AlgoSdk.Crypto.Random.Randomize(secret);
            expected[i] = secret.ToArray();
        }

        var json = AlgoApiSerializer.SerializeJson(expected);
        var either = AlgoApiSerializer.DeserializeJson<Either<byte[][], SignTxnsError>>(json);
        var actual = either.Value1;
        Assert.AreEqual(expected, actual);
    }
}
