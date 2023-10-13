using System;
using Algorand.Unity.LowLevel;
using NUnit.Framework;

namespace Algorand.Unity.Encoding.Tests
{
    public class Base58Test
    {
        [Test]
        [TestCase("Hello World!", "2NEpo7TZRRrLZSi2U")]
        [TestCase("The quick brown fox jumps over the lazy dog.",
            "USm3fpXnKG5EUBx2ndxBDMPVciP5hGey2Jh4NDv6gmeo1LkMeiKrLJUUBk6Z")]
        public void Base58EncodeFromUTF8ShouldEqualExpectedValue(string utf8, string expected)
        {
            var bytes = System.Text.Encoding.UTF8.GetBytes(utf8);
            var actual = Base58.Encode(bytes);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        [TestCase(new byte[] { 0x00, 0x00, 0x28, 0x7f, 0xb4, 0xcd }, "11233QC4")]
        public void Base58EncodeFromBytesShouldEqualExpectedValue(byte[] bytes, string expected)
        {
            var actual = Base58.Encode(bytes);
            Assert.AreEqual(expected, actual);
        }
    }
}