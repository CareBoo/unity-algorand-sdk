using AlgoSdk;
using AlgoSdk.Abi;
using NUnit.Framework;
using Unity.Collections;

[TestFixture]
public class UintNTest
{
    [Test]
    public void Uint64EncodingShouldEqualUintNOf64Encoding()
    {
        ulong value = 123456;
        using var references = new AbiReferences(Allocator.Temp);
        using var expected = new UIntN(value).Encode(AbiType.UIntN(64), references, Allocator.Temp);
        using var actual = new UInt64(value).Encode(AbiType.UIntN(64), references, Allocator.Temp);

        Assert.AreEqual(expected.Length, actual.Length);
        for (var i = 0; i < expected.Length; i++)
            Assert.AreEqual(expected[i], actual[i]);
    }

    [Test]
    [TestCase(new object[] { ulong.MinValue, (ushort)8 })]
    [TestCase(new object[] { ulong.MaxValue, (ushort)64 })]
    public void Uint64NShouldAlwaysReturnMultipleOf8(ulong value, ushort expected)
    {
        var actual = new UInt64(value).N;
        Assert.AreEqual(expected, actual);
    }

    [Test]
    [TestCase(new object[] { uint.MinValue, (ushort)8 })]
    [TestCase(new object[] { uint.MaxValue, (ushort)32 })]
    public void Uint32NShouldAlwaysReturnMultipleOf8(uint value, ushort expected)
    {
        var actual = new UInt32(value).N;
        Assert.AreEqual(expected, actual);
    }
}
