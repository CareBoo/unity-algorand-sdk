using System.Linq;
using AlgoSdk.Experimental.Abi;
using NUnit.Framework;
using Unity.Collections;

[TestFixture]
public class TupleTest
{
    [Test]
    public void EncodeShouldPackUpTo8BitsIntoASingleByte()
    {
        using var references = new AbiReferences(Allocator.Persistent);
        var expected = new byte[] { 0xff, 0x00 };
        // var bools = Args.Add(Boolean.True)
        //     .Add(Boolean.True)
        //     .Add(Boolean.True)
        //     .Add(Boolean.True)
        //     .Add(Boolean.True)
        //     .Add(Boolean.True)
        //     .Add(Boolean.True)
        //     .Add(Boolean.True)
        //     .Add(Boolean.False)
        //     ;
        var bools = new IAbiValue[9];
        for (var i = 0; i < 8; i++)
            bools[i] = Boolean.True;
        bools[8] = Boolean.False;
        var tuple = Tuple.Of(bools);
        var typeStr = string.Join(",", Enumerable.Range(0, 9).Select(_ => "bool"));
        typeStr = $"({typeStr})";
        using var actual = tuple.Encode(AbiType.Parse(typeStr), references, Allocator.Persistent);

        Assert.AreEqual(expected.Length, actual.Length);

        for (var i = 0; i < expected.Length; i++)
            Assert.AreEqual(expected[i], actual[i], $"index {i} differed");
    }
}
