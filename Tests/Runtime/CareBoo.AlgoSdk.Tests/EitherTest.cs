
using System;
using AlgoSdk;
using AlgoSdk.LowLevel;
using NUnit.Framework;
using Unity.Collections;

[TestFixture]
public class EitherTest
{
    [Test]
    public void JsonOfType2ShouldDeserializeToEitherWithValue2()
    {
        var expected = AlgoSdk.Crypto.Random.Bytes<Address>();
        var json = AlgoApiSerializer.SerializeJson(expected);
        var either = AlgoApiSerializer.DeserializeJson<Either<PrivateKey, Address>>(json);
        var actual = either.Value2;
        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void JsonOfType1ShouldDeserializeToEitherWithValue1()
    {
        var expected = AlgoSdk.Crypto.Random.Bytes<PrivateKey>();
        var json = AlgoApiSerializer.SerializeJson(expected);
        var either = AlgoApiSerializer.DeserializeJson<Either<PrivateKey, Address>>(json);
        var actual = either.Value1;
        Assert.AreEqual(expected, actual);
    }
}
