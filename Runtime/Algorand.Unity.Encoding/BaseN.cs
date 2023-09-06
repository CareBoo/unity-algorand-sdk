using System;
using System.Numerics;

namespace Algorand.Unity
{
    public static class BaseN
    {
        public enum EncodeError
        {
            None,
            InvalidResultLength,
            UnsupportedInputEncoding,
            UnsupportedOutputEncoding
        }

        public static EncodeError ChangeBase(
            ReadOnlySpan<byte> data,
            byte toBase,
            ref Span<byte> result,
            out int length,
            bool bigEndian = false)
        {
            length = 0;

            if (toBase <= 1) return EncodeError.UnsupportedOutputEncoding;
            var amount = new BigInteger(data, true, bigEndian);

            while (amount > 0)
            {
                if (length >= result.Length) return EncodeError.InvalidResultLength;
                result[length++] = (byte)(amount % toBase);
                amount /= toBase;
            }

            return EncodeError.None;
        }

        public static EncodeError ChangeBase(
            ReadOnlySpan<byte> data,
            byte fromBase,
            byte toBase,
            ref Span<byte> result,
            out int length
        )
        {
            length = 0;

            if (fromBase <= 1) return EncodeError.UnsupportedInputEncoding;
            if (toBase <= 1) return EncodeError.UnsupportedOutputEncoding;

            var amount = new BigInteger();
            for (var i = data.Length - 1; i >= 0; i--)
            {
                amount *= fromBase;
                amount += data[i];
            }

            while (amount > 0)
            {
                if (length >= result.Length) return EncodeError.InvalidResultLength;
                result[length++] = (byte)(amount % toBase);
                amount /= toBase;
            }

            return EncodeError.None;
        }
    }
}