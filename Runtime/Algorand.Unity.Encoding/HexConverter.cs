using System;
using Unity.Collections;

namespace Algorand.Unity.LowLevel
{
    public static class HexConverter
    {
        public enum ConversionError
        {
            None,
            InvalidOutputLength,
            InvalidCharacters
        }

        public const string HexAlphabet = "0123456789abcdef";

        public static ConversionError ToValue(Unicode.Rune rune, out int value)
        {
            if (rune.value == Unicode.BadRune.value)
            {
                value = 0;
                return ConversionError.InvalidCharacters;
            }

            return ToValue((char)rune.value, out value);
        }

        public static ConversionError ToValue(char c, out int value)
        {
            if (c >= '0' && c <= '9')
            {
                value = c - '0';
            }
            else if (c >= 'A' && c <= 'F')
            {
                value = c - 'A' + 10;
            }
            else if (c >= 'a' && c <= 'f')
            {
                value = c - 'a' + 10;
            }
            else
            {
                value = 0;
                return ConversionError.InvalidCharacters;
            }

            return ConversionError.None;
        }

        public static ConversionError ToHex(ReadOnlySpan<byte> bytes, Span<char> hex, out int length)
        {
            length = 0;
            if (hex.Length < bytes.Length * 2) return ConversionError.InvalidOutputLength;

            for (var i = 0; i < bytes.Length; i++)
            {
                var b = bytes[i];
                hex[length++] = HexAlphabet[b >> 4];
                hex[length++] = HexAlphabet[b & 0xF];
            }

            return ConversionError.None;
        }

        public static ConversionError FromHex(ReadOnlySpan<char> hex, Span<byte> bytes, out int length)
        {
            length = hex.Length / 2;
            if (bytes.Length < length)
                return ConversionError.InvalidOutputLength;

            var error = ConversionError.None;
            for (var i = 0; i < length; i++)
            {
                error |= ToValue(hex[i * 2], out var highNibble);
                error |= ToValue(hex[i * 2 + 1], out var lowNibble);

                bytes[i / 2] = (byte)((highNibble << 4) | lowNibble);
            }

            return error;
        }

        public static ConversionError ToHex<TFixedString>(ReadOnlySpan<byte> bytes, ref TFixedString hex)
            where TFixedString : struct, IUTF8Bytes, INativeList<byte>
        {
            if (!hex.TryResize(bytes.Length * 2, NativeArrayOptions.UninitializedMemory))
            {
                return ConversionError.InvalidOutputLength;
            }

            var hexIndex = 0;
            for (var i = 0; i < bytes.Length; i++)
            {
                var b = bytes[i];

                hex.Write(ref hexIndex, (Unicode.Rune)HexAlphabet[b >> 4]);
                hex.Write(ref hexIndex, (Unicode.Rune)HexAlphabet[b & 0xF]);
            }

            return ConversionError.None;
        }

        public static ConversionError FromHex<TFixedString>(TFixedString hex, Span<byte> bytes, out int length)
            where TFixedString : struct, IUTF8Bytes, INativeList<byte>
        {
            length = hex.Length / 2;
            if (bytes.Length < hex.Length / 2)
                return ConversionError.InvalidOutputLength;

            var error = ConversionError.None;
            var start = 0;
            for (var i = 0; i < length; i++)
            {
                error |= ToValue(hex.Read(ref start), out var highNibble);
                error |= ToValue(hex.Read(ref start), out var lowNibble);

                bytes[i] = (byte)((highNibble << 4) | lowNibble);
            }

            return error;
        }
    }
}
