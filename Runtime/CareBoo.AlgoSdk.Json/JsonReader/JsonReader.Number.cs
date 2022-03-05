using AlgoSdk.Collections;
using Unity.Collections;

namespace AlgoSdk.Json
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
            var result = ReadNumber(out byte x);
            value = unchecked((sbyte)x);
            return result;
        }

        public JsonReadError ReadNumber(out byte value)
        {
            var result = ReadNumber(out int x);
            value = (byte)x;
            return result;
        }

        public JsonReadError ReadNumber(out short value)
        {
            var result = ReadNumber(out int x);
            value = (short)x;
            return result;
        }

        public JsonReadError ReadNumber(out ushort value)
        {
            var result = ReadNumber(out uint x);
            value = (ushort)x;
            return result;
        }

        public JsonReadError ReadNumber(out int value)
        {
            value = default;
            SkipWhitespace();
            return text.Parse(ref offset, ref value) == ParseError.None
                ? JsonReadError.None
                : JsonReadError.ParseError;
        }

        public JsonReadError ReadNumber(out uint value)
        {
            value = default;
            SkipWhitespace();
            return text.Parse(ref offset, ref value) == ParseError.None
                ? JsonReadError.None
                : JsonReadError.ParseError;
        }

        public JsonReadError ReadNumber(out long value)
        {
            value = default;
            SkipWhitespace();
            return text.Parse(ref offset, ref value) == ParseError.None
                ? JsonReadError.None
                : JsonReadError.ParseError;
        }

        public JsonReadError ReadNumber(out ulong value)
        {
            value = default;
            SkipWhitespace();
            return text.Parse(ref offset, ref value) == ParseError.None
                ? JsonReadError.None
                : JsonReadError.ParseError;
        }

        JsonReadError SkipNumber()
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
