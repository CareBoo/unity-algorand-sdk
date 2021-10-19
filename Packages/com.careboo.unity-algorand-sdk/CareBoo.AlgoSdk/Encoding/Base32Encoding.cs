using System;
using AlgoSdk.LowLevel;
using Unity.Collections;
using Unity.Mathematics;

namespace AlgoSdk
{
    public static class Base32Encoding
    {
        public static byte[] ToBytes(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                throw new ArgumentNullException("input");
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

        public static unsafe void ToBytes<TByteArray, TString>(TString s, ref TByteArray bytes)
            where TByteArray : struct, IByteArray
            where TString : struct, IUTF8Bytes, INativeList<byte>
        {
            if (s.Length == 0)
            {
                throw new ArgumentException("argument must have a length > 0", nameof(s));
            }

            var length = s.Length;
            while (s[length - 1] == PaddingCharValue)
                length--;

            int byteCount = length * 5 / 8; //this must be TRUNCATED
            if (bytes.Length != byteCount)
                throw new ArgumentException($"bytes.Length ({bytes.Length}) is different than expected bytes ({byteCount}). This usually means the given format is not Base32...");

            byte curByte = 0, bitsRemaining = 8;
            int mask = 0, arrayIndex = 0;
            var sPtr = s.GetUnsafePtr();

            var i = 0;
            while (i < length)
            {
                var error = Unicode.Utf8ToUcs(out var rune, sPtr, ref i, s.Capacity);
                int cValue = CharToValue((char)rune.value);

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

        public static void ToString<TByteArray, TString>(in TByteArray bytes, ref TString s)
            where TByteArray : struct, IByteArray
            where TString : struct, IUTF8Bytes, INativeList<byte>
        {
            if (bytes.Length == 0)
                throw new ArgumentNullException(nameof(bytes));

            s.Length = (int)math.ceil(bytes.Length / 5f) * 8;

            byte nextCharByte = 0, bitsRemaining = 5;
            int arrayIndex = 0;

            for (var i = 0; i < bytes.Length; i++)
            {
                var b = bytes[i];
                nextCharByte = (byte)(nextCharByte | (b >> (8 - bitsRemaining)));
                s[arrayIndex++] = (byte)ValueToChar(nextCharByte);

                if (bitsRemaining < 4)
                {
                    nextCharByte = (byte)((b >> (3 - bitsRemaining)) & 31);
                    s[arrayIndex++] = (byte)ValueToChar(nextCharByte);
                    bitsRemaining += 5;
                }

                bitsRemaining -= 3;
                nextCharByte = (byte)((b << bitsRemaining) & 31);
            }

            //if we didn't end with a full char
            if (arrayIndex != s.Length)
            {
                s[arrayIndex++] = (byte)ValueToChar(nextCharByte);
                while (arrayIndex != s.Length) s[arrayIndex++] = PaddingCharValue; //padding
            }
        }

        public static readonly byte PaddingCharValue = (byte)'=';

        private static int CharToValue(char c)
        {
            int value = (int)c;

            //65-90 == uppercase letters
            if (value < 91 && value > 64)
            {
                return value - 65;
            }
            //50-55 == numbers 2-7
            if (value < 56 && value > 49)
            {
                return value - 24;
            }
            //97-122 == lowercase letters
            if (value < 123 && value > 96)
            {
                return value - 97;
            }

            throw new ArgumentException("Character is not a Base32 character.", "c");
        }

        private static char ValueToChar(byte b)
        {
            if (b < 26)
            {
                return (char)(b + 65);
            }

            if (b < 32)
            {
                return (char)(b + 24);
            }

            throw new ArgumentException("Byte is not a value Base32 value.", "b");
        }

    }
}
