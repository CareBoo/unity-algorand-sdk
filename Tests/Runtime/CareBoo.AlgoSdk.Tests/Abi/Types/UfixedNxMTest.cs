using AlgoSdk.Abi;
using NUnit.Framework;
using Unity.Collections;

[TestFixture]
public class UfixedNxMTest
{
    [Test]
    public void ChangingPrecisionToAndFromValueShouldPreserveValue()
    {
        var value = new UintN(300);
        byte precision = 2;
        var expected = new UfixedNxM(value, precision);
        var actual = expected.As(n: 16, m: 1);
        var defn = new Method.Arg { Type = "ufixed16x2" };
        var expectedEncoded = expected.Encode(defn, Allocator.Temp);
        var actualEncoded = actual.Encode(defn, Allocator.Temp);

        Assert.AreEqual(expectedEncoded.Length, actualEncoded.Length);
        for (var i = 0; i < expectedEncoded.Length; i++)
            Assert.AreEqual(expectedEncoded[i], actualEncoded[i], $"bytes differed at index {i}");
    }

    [Test]
    public void ChangingPrecisionShouldChangeValueByPowersOf10()
    {
        var expected = new UfixedNxM(new UintN(30), 1);
        var actual = new UfixedNxM(new UintN(300), 2).As(16, 1);
        Assert.AreEqual(expected.Value, actual.Value);
    }
}
