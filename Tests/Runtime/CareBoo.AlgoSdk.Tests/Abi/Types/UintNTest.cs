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
        using var references = new AbiReferences(Allocator.Persistent);
        using var expected = new UIntN(value).Encode(AbiType.UIntN(64), references, Allocator.Persistent);
        using var actual = new UInt64(value).Encode(AbiType.UIntN(64), references, Allocator.Persistent);

        Assert.AreEqual(expected.Length, actual.Length);
        for (var i = 0; i < expected.Length; i++)
            Assert.AreEqual(expected[i], actual[i]);
    }

    [Test]
    public void Uint64NShouldAlwaysReturnMultipleOf8()
    {
        var testCases = new[]
        {
            (value: ulong.MinValue, expected: (ushort)8),
            (value: ulong.MaxValue, expected: (ushort)64)
        };
        foreach (var (value, expected) in testCases)
        {
            var actual = new UInt64(value).N;
            Assert.AreEqual(expected, actual);
        }
    }

    [Test]
    public void Uint32NShouldAlwaysReturnMultipleOf8()
    {
        var testCases = new[]
        {
            (value: uint.MinValue, expected: (ushort)8),
            (value: uint.MaxValue, expected: (ushort)32)
        };
        foreach (var (value, expected) in testCases)
        {
            var actual = new UInt32(value).N;
            Assert.AreEqual(expected, actual);
        }
    }
}
