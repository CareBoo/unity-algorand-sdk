using AlgoSdk.Experimental.Abi;
using NUnit.Framework;
using Unity.Collections;

[TestFixture]
public class UFixedNxMTest
{
    [Test]
    public void ChangingPrecisionToAndFromValueShouldPreserveValue()
    {
        var value = new UIntN(300);
        byte precision = 2;
        using var references = new AbiReferences(Allocator.Persistent);
        var expected = new UFixedNxM(value, precision);
        var actual = expected.As(AbiType.UFixedNxM(16, 1));
        using var expectedEncoded = expected.Encode(AbiType.UFixedNxM(16, 2), references, Allocator.Persistent);
        using var actualEncoded = actual.Encode(AbiType.UFixedNxM(16, 2), references, Allocator.Persistent);

        Assert.AreEqual(expectedEncoded.Length, actualEncoded.Length);
        for (var i = 0; i < expectedEncoded.Length; i++)
            Assert.AreEqual(expectedEncoded[i], actualEncoded[i], $"bytes differed at index {i}");
    }

    [Test]
    public void ChangingPrecisionShouldChangeValueByPowersOf10()
    {
        var expected = new UFixedNxM(new UIntN(30), 1);
        var actual = new UFixedNxM(new UIntN(300), 2).As(AbiType.UFixedNxM(16, 1));
        Assert.AreEqual(expected.Value, actual.Value);
    }
}
