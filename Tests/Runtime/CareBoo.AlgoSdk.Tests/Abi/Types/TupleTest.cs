using System.Linq;
using AlgoSdk.Abi;
using NUnit.Framework;
using Unity.Collections;

[TestFixture]
public class TupleTest
{
    [Test]
    public void EncodeShouldPackUpTo8BitsIntoASingleByte()
    {
        using var references = new AbiReferences(Allocator.Temp);
        var expected = new byte[] { 0xff, 0x00 };
        var bools = Args.Add(Boolean.True)
            .Add(Boolean.True)
            .Add(Boolean.True)
            .Add(Boolean.True)
            .Add(Boolean.True)
            .Add(Boolean.True)
            .Add(Boolean.True)
            .Add(Boolean.True)
            .Add(Boolean.False)
            ;
        var tuple = Tuple.Of(bools);
        var typeStr = string.Join(",", Enumerable.Range(0, 9).Select(_ => "bool"));
        typeStr = $"({typeStr})";
        using var actual = tuple.Encode(AbiType.Parse(typeStr), references, Allocator.Temp);

        Assert.AreEqual(expected.Length, actual.Length);

        for (var i = 0; i < expected.Length; i++)
            Assert.AreEqual(expected[i], actual[i], $"index {i} differed");
    }
}