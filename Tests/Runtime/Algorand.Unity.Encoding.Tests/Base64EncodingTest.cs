using Algorand.Unity;
using NUnit.Framework;

public class Base64Test
{
    [Test]
    public void BytesRequiredForBase64EncodingShouldReturnCorrectValue()
    {
        var testCases = new[]
        {
            (bytes: 3, expected: 4),
            (bytes: 2, expected: 4),
            (bytes: 4, expected: 8)
        };

        foreach (var (bytes, expected) in testCases)
        {
            var actual = Base64Encoding.BytesRequiredForBase64Encoding(bytes);
            Assert.AreEqual(expected, actual);
        }
    }
}
