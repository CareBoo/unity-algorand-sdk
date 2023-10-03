using System;

namespace Algorand.Unity.LowLevel
{
    /// <summary>
    ///     Utilities for encoding and decoding Base58 strings (bitcoin format).
    /// </summary>
    public static class Base58
    {
        private const string Characters = "123456789ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz";

        /// <summary>
        ///     Encode a byte array into a base58 string.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string Encode(ReadOnlySpan<byte> data)
        {
            if (data.Length == 0) return string.Empty;

            var zeroCount = 0;
            while (zeroCount < data.Length && data[zeroCount] == 0) zeroCount++;
            data = data.Slice(zeroCount);
            Span<byte> resultBytes = stackalloc byte[data.Length * 2];
            BaseN.ChangeBase(data, 58, ref resultBytes, out var resultLength, true);
            Span<char> result = stackalloc char[resultLength + zeroCount];
            for (var i = 0; i < zeroCount; i++) result[i] = Characters[0];
            for (var i = zeroCount; i < result.Length; i++)
                result[i] = Characters[resultBytes[resultLength - 1 - (i - zeroCount)]];

            return result.ToString();
        }
    }
}