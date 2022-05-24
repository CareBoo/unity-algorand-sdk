using AlgoSdk.Abi;
using NUnit.Framework;
using Unity.Collections;

[TestFixture]
public class UfixedNxMTest
{
    [Test]
    public void ChangingPrecisionToAndFromValueShouldPreserveValue()
    {
        var value = new UIntN(300);
        byte precision = 2;
        var expected = new UfixedNxM(value, precision);
        var actual = expected.As(AbiType.UFixedNxM(16, 1));
        var expectedEncoded = expected.Encode(AbiType.UFixedNxM(16, 2), Allocator.Temp);
        var actualEncoded = actual.Encode(AbiType.UFixedNxM(16, 2), Allocator.Temp);

        Assert.AreEqual(expectedEncoded.Length, actualEncoded.Length);
        for (var i = 0; i < expectedEncoded.Length; i++)
            Assert.AreEqual(expectedEncoded[i], actualEncoded[i], $"bytes differed at index {i}");
    }

    [Test]
    public void ChangingPrecisionShouldChangeValueByPowersOf10()
    {
        var expected = new UfixedNxM(new UIntN(30), 1);
        var actual = new UfixedNxM(new UIntN(300), 2).As(AbiType.UFixedNxM(16, 1));
        Assert.AreEqual(expected.Value, actual.Value);
    }
}
