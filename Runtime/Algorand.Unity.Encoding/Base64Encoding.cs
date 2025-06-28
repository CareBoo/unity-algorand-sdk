using System;
using System.Runtime.InteropServices;
using Algorand.Unity.Collections;
using Algorand.Unity.LowLevel;
using Unity.Collections;

namespace Algorand.Unity
{
    public static class Base64Encoding
    {
        public static FormatError CopyToBase64<TBytes, T>(this TBytes bytes, ref T s)
            where TBytes : struct, IArray<byte>
            where T : unmanaged, IUTF8Bytes, INativeList<byte>
        {
            var byteSpan = MemoryMarshal.AsBytes(MemoryMarshal.CreateReadOnlySpan(ref bytes, 1));
            return byteSpan.CopyToBase64(ref s);
        }

        public static FormatError CopyToBase64<T>(this ReadOnlySpan<byte> bytes, ref T s)
            where T : unmanaged, IUTF8Bytes, INativeList<byte>
        {
            var charLength = CharsRequiredForBase64Encoding(bytes.Length);
            Span<char> chars = charLength <= 512
                ? stackalloc char[charLength]
                : new char[charLength];
            if (!Convert.TryToBase64Chars(bytes, chars, out var charsWritten))
            {
                return FormatError.Overflow;
            }
            chars = chars[..charsWritten];
            s.Clear();
            return s.Append(chars);
        }

        public static FormatError CopyFromBase64<TByteArray, T>(ref this TByteArray bytes, T s, int maxLength = int.MaxValue)
            where TByteArray : struct, IArray<byte>
            where T : unmanaged, IUTF8Bytes, INativeList<byte>
        {
            var byteIndex = 0;
            var charCount = 0;
            while (s.Read(ref byteIndex) != Unicode.BadRune)
            {
                charCount += 1;
            }
            Span<char> chars = charCount <= 512
                ? stackalloc char[charCount]
                : new char[charCount];
            byteIndex = 0;
            for (var i = 0; i < charCount; i++)
            {
                chars[i] = (char)s.Read(ref byteIndex).value;
            }

            var byteSpan = MemoryMarshal.AsBytes(MemoryMarshal.CreateSpan(ref bytes, 1));
            return Convert.TryFromBase64Chars(chars, byteSpan, out var bytesWritten)
                ? FormatError.None
                : FormatError.Overflow;
        }

        public static int CharsRequiredForBase64Encoding(int currentBytes)
        {
            return (currentBytes + 2) / 3 * 4;
        }
    }
}
