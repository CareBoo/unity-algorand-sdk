using System.Linq;
using AlgoSdk.LowLevel;
using NUnit.Framework;
using Unity.Collections;

public class NativeArrayUtilTest
{
    [Test]
    public void ConcatAllShouldReturnArrayWithConcattedBytes()
    {
        var input = RandomBytes();
        var expected = input.SelectMany(b => b).ToArray();
        using var actualNative = NativeArrayUtil.ConcatAll(input, Allocator.Persistent);
        var actual = actualNative.ToArray();
        Assert.True(expected.SequenceEqual(actual));
    }

    byte[][] RandomBytes()
    {
        var random = new System.Random();
        var numArrays = UnityEngine.Random.Range(3, 10);
        var result = new byte[numArrays][];
        for (var i = 0; i < numArrays; i++)
        {
            var element = new byte[UnityEngine.Random.Range(128, 1024)];
            random.NextBytes(element);
            result[i] = element;
        }
        return result;
    }
}
