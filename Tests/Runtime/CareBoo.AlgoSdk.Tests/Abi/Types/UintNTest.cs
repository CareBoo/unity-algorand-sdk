using AlgoSdk.Abi;
using NUnit.Framework;
using Unity.Collections;

[TestFixture]
public class UintNTest
{
    [Test]
    public void Uint64EncodingShouldEqualUintNOf64Encoding()
    {
        var defn = new Method.Arg { Type = "uint64" };

        ulong value = 123456;
        using var expected = new UintN(value).Encode(defn, Allocator.Temp);
        using var actual = new Uint64(value).Encode(defn, Allocator.Temp);

        Assert.AreEqual(expected.Length, actual.Length);
        for (var i = 0; i < expected.Length; i++)
            Assert.AreEqual(expected[i], actual[i]);
    }

    [Test]
    [TestCase(new object[] { ulong.MinValue, (ushort)8 })]
    [TestCase(new object[] { ulong.MaxValue, (ushort)64 })]
    public void Uint64NShouldAlwaysReturnMultipleOf8(ulong value, ushort expected)
    {
        var actual = new Uint64(value).N;
        Assert.AreEqual(expected, actual);
    }

    [Test]
    [TestCase(new object[] { uint.MinValue, (ushort)8 })]
    [TestCase(new object[] { uint.MaxValue, (ushort)32 })]
    public void Uint32NShouldAlwaysReturnMultipleOf8(uint value, ushort expected)
    {
        var actual = new Uint32(value).N;
        Assert.AreEqual(expected, actual);
    }
}
