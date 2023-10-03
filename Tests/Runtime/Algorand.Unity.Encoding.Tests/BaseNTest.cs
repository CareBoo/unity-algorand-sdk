using System;
using NUnit.Framework;

namespace Algorand.Unity.Encoding.Tests
{
    public class BaseNTest
    {
        [Test]
        [TestCase(new byte[] { 5 }, 10, 2, new byte[] { 1, 0, 1 }, BaseN.EncodeError.None)]
        [TestCase(new byte[] { 5, 5, 2 }, 10, 16, new byte[] { 15, 15 }, BaseN.EncodeError.None)]
        [TestCase(new byte[] { 15, 15 }, 16, 10, new byte[] { 5, 5, 2 }, BaseN.EncodeError.None)]
        [TestCase(new byte[] { 2, 5, 5 }, 1, 16, null, BaseN.EncodeError.UnsupportedInputEncoding)]
        [TestCase(new byte[] { 2, 5, 5 }, 10, 0, null, BaseN.EncodeError.UnsupportedOutputEncoding)]
        public void BaseNConversionShouldEqualExpectedValue(byte[] inputData, byte fromBase, byte toBase,
            byte[] expectedOutput, BaseN.EncodeError expectedError)
        {
            ReadOnlySpan<byte> data = inputData;
            Span<byte>
                result = new byte[expectedOutput?.Length ??
                                  data.Length]; // If expectedOutput is null, just size the result span as the input data length
            int length;
            var error = BaseN.ChangeBase(data, fromBase, toBase, ref result, out length);

            Assert.AreEqual(expectedError, error);
            if (expectedError == BaseN.EncodeError.None)
            {
                Assert.AreEqual(expectedOutput.Length, length);
                CollectionAssert.AreEqual(expectedOutput, result.Slice(0, length).ToArray());
            }
        }

        [Test]
        [TestCase(new byte[] { 255 }, 16, new byte[] { 15, 15 }, BaseN.EncodeError.None)]
        public void BaseNConvertFromBase256ShouldEqualExpectedValue(byte[] inputData, byte toBase,
            byte[] expectedOutput, BaseN.EncodeError expectedError)
        {
            ReadOnlySpan<byte> data = inputData;
            Span<byte>
                result = new byte[expectedOutput?.Length ??
                                  data.Length]; // If expectedOutput is null, just size the result span as the input data length
            int length;
            var error = BaseN.ChangeBase(data, toBase, ref result, out length);

            Assert.AreEqual(expectedError, error);
            if (expectedError == BaseN.EncodeError.None)
            {
                Assert.AreEqual(expectedOutput.Length, length);
                CollectionAssert.AreEqual(expectedOutput, result.Slice(0, length).ToArray());
            }
        }
    }
}