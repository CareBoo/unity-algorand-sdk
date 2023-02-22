using System;
using Algorand.Unity.LowLevel;
using Unity.Collections;
using Unity.Mathematics;

namespace Algorand.Unity
{
    public static class Base32Encoding
    {
        public static byte[] ToBytes(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                throw new ArgumentNullException(nameof(input));
            }

            input = input.TrimEnd('='); //remove padding characters
            int byteCount = input.Length * 5 / 8; //this must be TRUNCATED
            byte[] returnArray = new byte[byteCount];

            byte curByte = 0, bitsRemaining = 8;
            int mask = 0, arrayIndex = 0;

            foreach (char c in input)
            {
                int cValue = CharToValue(c);

                if (bitsRemaining > 5)
                {
                    mask = cValue << (bitsRemaining - 5);
                    curByte = (byte)(curByte | mask);
                    bitsRemaining -= 5;
                }
                else
                {
                    mask = cValue >> (5 - bitsRemaining);
                    curByte = (byte)(curByte | mask);
                    returnArray[arrayIndex++] = curByte;
                    curByte = (byte)(cValue << (3 + bitsRemaining));
                    bitsRemaining += 3;
                }
            }

            //if we didn't end with a full byte
            if (arrayIndex != byteCount)
            {
                returnArray[arrayIndex] = curByte;
            }

            return returnArray;
        }

        public static unsafe ConversionError ToBytes<TByteArray, TString>(TString s, ref TByteArray bytes)
            where TByteArray : struct, IByteArray
            where TString : struct, IUTF8Bytes, INativeList<byte>
        {
            var length = s.Length;
            if (s.Length > 0)
            {
                while (s[length - 1] == PaddingCharValue)
                    length--;
            }

            int byteCount = length * 5 / 8; //this must be TRUNCATED
            if (bytes.Length != byteCount)
                return ConversionError.Encoding;

            byte curByte = 0, bitsRemaining = 8;
            int mask = 0, arrayIndex = 0;
            var sPtr = s.GetUnsafePtr();

            var i = 0;
            while (i < length)
            {
                var error = Unicode.Utf8ToUcs(out var rune, sPtr, ref i, s.Capacity);
                if (error != ConversionError.None)
                {
                    return error;
                }

                error = CharToValue((char)rune.value, out var cValue);
                if (error != ConversionError.None)
                {
                    return error;
                }

                if (bitsRemaining > 5)
                {
                    mask = cValue << (bitsRemaining - 5);
                    curByte = (byte)(curByte | mask);
                    bitsRemaining -= 5;
                }
                else
                {
                    mask = cValue >> (5 - bitsRemaining);
                    curByte = (byte)(curByte | mask);
                    bytes[arrayIndex++] = curByte;
                    curByte = (byte)(cValue << (3 + bitsRemaining));
                    bitsRemaining += 3;
                }
            }

            //if we didn't end with a full byte
            if (arrayIndex != byteCount)
            {
                bytes[arrayIndex] = curByte;
            }

            return ConversionError.None;
        }

        public static void TrimPadding<T>(ref T fs)
            where T : struct, INativeList<byte>, IUTF8Bytes
        {
            var trimPadding = fs.Length;
            while (fs[trimPadding - 1] == Base32Encoding.PaddingCharValue)
                trimPadding--;
            fs.Length = trimPadding;
        }

        public static string ToString(byte[] input)
        {
            if (input == null || input.Length == 0)
            {
                throw new ArgumentNullException("input");
            }

            int charCount = (int)Math.Ceiling(input.Length / 5d) * 8;
            char[] returnArray = new char[charCount];

            byte nextChar = 0, bitsRemaining = 5;
            int arrayIndex = 0;

            foreach (byte b in input)
            {
                nextChar = (byte)(nextChar | (b >> (8 - bitsRemaining)));
                returnArray[arrayIndex++] = ValueToChar(nextChar);

                if (bitsRemaining < 4)
                {
                    nextChar = (byte)((b >> (3 - bitsRemaining)) & 31);
                    returnArray[arrayIndex++] = ValueToChar(nextChar);
                    bitsRemaining += 5;
                }

                bitsRemaining -= 3;
                nextChar = (byte)((b << bitsRemaining) & 31);
            }

            //if we didn't end with a full char
            if (arrayIndex != charCount)
            {
                returnArray[arrayIndex++] = ValueToChar(nextChar);
                while (arrayIndex != charCount) returnArray[arrayIndex++] = '='; //padding
            }

            return new string(returnArray);
        }

        public static ConversionError ToString<TByteArray, TString>(TByteArray bytes, ref TString s)
            where TByteArray : struct, IByteArray
            where TString : struct, IUTF8Bytes, INativeList<byte>
        {
            if (bytes.Length == 0)
                throw new ArgumentNullException(nameof(bytes));

            s.Length = (int)math.ceil(bytes.Length / 5f) * 8;

            byte nextCharByte = 0, bitsRemaining = 5;
            int arrayIndex = 0;

            ConversionError error;
            char c;
            for (var i = 0; i < bytes.Length; i++)
            {
                var b = bytes[i];
                nextCharByte = (byte)(nextCharByte | (b >> (8 - bitsRemaining)));
                error = ValueToChar(nextCharByte, out c);
                if (error != ConversionError.None)
                {
                    return error;
                }

                s[arrayIndex++] = (byte)c;

                if (bitsRemaining < 4)
                {
                    nextCharByte = (byte)((b >> (3 - bitsRemaining)) & 31);
                    error = ValueToChar(nextCharByte, out c);
                    if (error != ConversionError.None)
                    {
                        return error;
                    }

                    s[arrayIndex++] = (byte)c;
                    bitsRemaining += 5;
                }

                bitsRemaining -= 3;
                nextCharByte = (byte)((b << bitsRemaining) & 31);
            }

            //if we didn't end with a full char
            if (arrayIndex != s.Length)
            {
                error = ValueToChar(nextCharByte, out c);
                if (error != ConversionError.None)
                {
                    return error;
                }

                s[arrayIndex++] = (byte)c;
                while (arrayIndex != s.Length) s[arrayIndex++] = PaddingCharValue; //padding
            }

            return ConversionError.None;
        }

        public static readonly byte PaddingCharValue = (byte)'=';

        private static ConversionError CharToValue(char c, out int value)
        {
            value = c;

            switch (value)
            {
                //65-90 == uppercase letters
                case < 91 and > 64:
                    value -= 65;
                    break;
                //50-55 == numbers 2-7
                case < 56 and > 49:
                    value -= 24;
                    break;
                //97-122 == lowercase letters
                case < 123 and > 96:
                    value -= 97;
                    break;
                default:
                    return ConversionError.Encoding;
            }

            return ConversionError.None;
        }

        private static int CharToValue(char c)
        {
            if (CharToValue(c, out var value) != ConversionError.None)
            {
                throw new ArgumentException("not a valid base32 character", nameof(c));
            }

            return value; 
        }

        private static ConversionError ValueToChar(byte b, out char c)
        {
            c = default;
            switch (b)
            {
                case < 26:
                    c = (char)(b + 65);
                    break;
                case < 32:
                    c = (char)(b + 24);
                    break;
                default:
                    return ConversionError.Encoding;
            }

            return ConversionError.None;
        }

        private static char ValueToChar(byte b)
        {
            if (ValueToChar(b, out var c) != ConversionError.None)
            {
                throw new ArgumentException("not a valid base32 byte", nameof(b));
            }

            return c;
        }
    }
}