using Unity.Collections;

namespace AlgoSdk.Json
{
    public ref partial struct JsonReader
    {
        NativeText text;
        int offset;

        public JsonReader(NativeText text)
        {
            this.text = text;
            this.offset = 0;
        }

        public JsonToken Peek()
        {
            if (offset >= text.Length)
                return JsonToken.None;
            SkipWhitespace();
            return text.Peek(offset).ToJsonToken();
        }

        public JsonToken Read()
        {
            if (offset >= text.Length)
                return JsonToken.None;
            SkipWhitespace();
            return text.Read(ref offset).ToJsonToken();
        }

        public JsonReadError ReadString<T>(ref T value)
            where T : struct, INativeList<byte>, IUTF8Bytes
        {
            value.Clear();
            if (Peek() != JsonToken.String)
                return JsonReadError.IncorrectType;
            text.Read(ref offset);
            while (offset < text.Length)
            {
                var r = text.Read(ref offset);
                var c = r.ToChar();
                if (c == '"')
                    break;
                if (c == '\\')
                {
                    if (offset >= text.Length)
                        return JsonReadError.IncorrectFormat;
                    r = text.Read(ref offset);
                }
                value.Append(r);
            }
            return JsonReadError.None;
        }

        public JsonReadError ReadBool(out bool value)
        {
            SkipWhitespace();
            value = default;

            var resetOffset = offset;
            var c = text.Read(ref offset).ToChar();
            if (c == 't' && text.Found(ref offset, 'r', 'u', 'e'))
            {
                value = true;
                return JsonReadError.None;
            }
            if (c == 'f' && text.Found(ref offset, 'a', 'l', 's', 'e'))
            {
                return JsonReadError.None;
            }
            offset = resetOffset;
            return JsonReadError.ParseError;
        }

        public JsonReadError ReadNull()
        {
            SkipWhitespace();
            var resetOffset = offset;
            if (text.Found(ref offset, 'n', 'u', 'l', 'l'))
            {
                return JsonReadError.None;
            }
            offset = resetOffset;
            return JsonReadError.ParseError;
        }

        public bool TryReadNull()
        {
            var token = Peek();
            if (token != JsonToken.Null)
                return false;
            ReadNull();
            return true;
        }

        public bool TryRead(JsonToken token)
        {
            if (token != Peek())
                return false;
            Read();
            return true;
        }

        void SkipWhitespace()
        {
            while (offset < text.Length)
            {
                if (text.Peek(offset).IsWhiteSpaceOrSeparator())
                    offset++;
                else
                    break;
            }
        }
    }
}
