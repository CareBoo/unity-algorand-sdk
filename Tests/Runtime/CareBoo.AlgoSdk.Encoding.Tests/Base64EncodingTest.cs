using AlgoSdk;
using NUnit.Framework;
using Unity.Collections;
using UnityEngine.TestTools;

public class Base64Test
{
    [TestCase(3, 4)]
    [TestCase(2, 4)]
    [TestCase(4, 8)]
    public void BytesRequiredForBase64EncodingShouldReturnCorrectValue(int bytes, int expected)
    {
        var actual = Base64Encoding.BytesRequiredForBase64Encoding(bytes);
        Assert.AreEqual(expected, actual);
    }
}
