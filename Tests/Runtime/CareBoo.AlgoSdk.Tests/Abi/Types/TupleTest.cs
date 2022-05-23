using System.Linq;
using AlgoSdk.Abi;
using NUnit.Framework;
using Unity.Collections;

[TestFixture]
public class TupleTest
{
    [Test]
    public void AbiTypeNameShouldReturnCommaSeparatedTypes()
    {
        var t1 = Boolean.True;
        var t2 = new Uint64(32);
        var t3 = new UfixedNxM(new UintN(12), 16);

        var expected = $"({t1.AbiTypeName},{t2.AbiTypeName},{t3.AbiTypeName})";

        var tuple = Tuple.Of(Args.Add(t1).Add(t2).Add(t3));
        var actual = tuple.AbiTypeName;

        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void EncodeShouldPackUpTo8BitsIntoASingleByte()
    {
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
        using var actual = tuple.Encode(new Method.Arg { Type = typeStr }, Allocator.Temp);

        Assert.AreEqual(expected.Length, actual.Length);

        for (var i = 0; i < expected.Length; i++)
            Assert.AreEqual(expected[i], actual[i]);
    }
}
