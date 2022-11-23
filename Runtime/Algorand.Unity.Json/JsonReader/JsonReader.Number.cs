using Algorand.Unity.Collections;
using Unity.Collections;

namespace Algorand.Unity.Json
{
    public ref partial struct JsonReader
    {
        public JsonReadError ReadNumber(out float value)
        {
            value = default;
            SkipWhitespace();
            return text.Parse(ref offset, ref value) == ParseError.None
                ? JsonReadError.None
                : JsonReadError.ParseError;
        }

        public JsonReadError ReadNumber(out sbyte value)
        {
            value = default;
            var result = ReadNumber(out JsonNumber number);
            if (result != JsonReadError.None)
                return result;

            return number.TryCast(out value)
                ? JsonReadError.None
                : JsonReadError.Overflow
                ;
        }

        public JsonReadError ReadNumber(out byte value)
        {
            value = default;
            var result = ReadNumber(out JsonNumber number);
            if (result != JsonReadError.None)
                return result;

            return number.TryCast(out value)
                ? JsonReadError.None
                : JsonReadError.Overflow
                ;
        }

        public JsonReadError ReadNumber(out short value)
        {
            value = default;
            var result = ReadNumber(out JsonNumber number);
            if (result != JsonReadError.None)
                return result;

            return number.TryCast(out value)
                ? JsonReadError.None
                : JsonReadError.Overflow
                ;
        }

        public JsonReadError ReadNumber(out ushort value)
        {
            value = default;
            var result = ReadNumber(out JsonNumber number);
            if (result != JsonReadError.None)
                return result;

            return number.TryCast(out value)
                ? JsonReadError.None
                : JsonReadError.Overflow
                ;
        }

        public JsonReadError ReadNumber(out int value)
        {
            value = default;
            var result = ReadNumber(out JsonNumber number);
            if (result != JsonReadError.None)
                return result;

            return number.TryCast(out value)
                ? JsonReadError.None
                : JsonReadError.Overflow
                ;
        }

        public JsonReadError ReadNumber(out uint value)
        {
            value = default;
            var result = ReadNumber(out JsonNumber number);
            if (result != JsonReadError.None)
                return result;

            return number.TryCast(out value)
                ? JsonReadError.None
                : JsonReadError.Overflow
                ;
        }

        public JsonReadError ReadNumber(out long value)
        {
            value = default;
            var result = ReadNumber(out JsonNumber number);
            if (result != JsonReadError.None)
                return result;

            return number.TryCast(out value)
                ? JsonReadError.None
                : JsonReadError.Overflow
                ;
        }

        public JsonReadError ReadNumber(out ulong value)
        {
            value = default;
            var result = ReadNumber(out JsonNumber number);
            if (result != JsonReadError.None)
                return result;

            return number.TryCast(out value)
                ? JsonReadError.None
                : JsonReadError.Overflow
                ;
        }

        public JsonReadError ReadNumber(out JsonNumber value)
        {
            value = default;
            SkipWhitespace();
            bool sign = default;
            ulong integer = default;
            ushort fractionDigits = default;
            ulong fraction = default;
            bool exponentSign = default;
            ushort exponent = default;

            int resetOffset = offset;
            var c = (char)text.Peek(offset).value;
            if (c == '-')
            {
                sign = true;
                text.Read(ref offset);
            }
            else if (c == '+')
            {
                text.Read(ref offset);
            }

            while (offset < text.Length && Unicode.Rune.IsDigit(text.Peek(offset)))
            {
                integer = integer * 10 + (ulong)(text.Peek(offset).value - '0');
                text.Read(ref offset);
            }

            if (text.Peek(offset).value == '.')
            {
                text.Read(ref offset);
                while (offset < text.Length && Unicode.Rune.IsDigit(text.Peek(offset)))
                {
                    fraction = fraction * 10 + (ulong)(text.Peek(offset).value - '0');
                    text.Read(ref offset);
                    fractionDigits++;
                }
            }

            c = (char)text.Peek(offset).value;
            if (c == 'e' || c == 'E')
            {
                text.Read(ref offset);
                c = (char)text.Peek(offset).value;
                if (c == '-')
                {
                    exponentSign = true;
                    text.Read(ref offset);
                }
                else if (c == '+')
                {
                    text.Read(ref offset);
                }
                while (offset < text.Length && Unicode.Rune.IsDigit(text.Peek(offset)))
                {
                    exponent = (ushort)(exponent * 10 + (ushort)(text.Peek(offset).value - '0'));
                    text.Read(ref offset);
                }
            }

            value = new JsonNumber(sign, integer, fractionDigits, fraction, exponentSign, exponent);
            return JsonReadError.None;
        }

        private JsonReadError SkipNumber()
        {
            while (offset < text.Length)
            {
                var t = text.Peek(offset).ToJsonToken();
                if (t != JsonToken.Number)
                    break;
                offset++;
            }
            return JsonReadError.None;
        }
    }
}
