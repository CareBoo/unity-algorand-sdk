using Unity.Collections;

namespace Algorand.Unity.Json
{
    public ref partial struct JsonReader
    {
        private NativeText text;
        private int offset;

        public JsonReader(NativeText text)
        {
            this.text = text;
            this.offset = 0;
        }

        public int Position
        {
            get => offset;
            set => offset = value;
        }

        public char Char => text.Peek(offset).ToChar();

        public string Text => text.ToString();

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

        public JsonReadError Skip()
        {
            SkipWhitespace();
            var startToken = Peek();
            switch (startToken)
            {
                case JsonToken.ArrayBegin:
                    return SkipArray();
                case JsonToken.Bool:
                    return SkipBool();
                case JsonToken.Null:
                    return SkipNull();
                case JsonToken.Number:
                    return SkipNumber();
                case JsonToken.ObjectBegin:
                    return SkipObject();
                case JsonToken.String:
                    return SkipString();
                default:
                    return JsonReadError.IncorrectFormat;
            }
        }

        public JsonReadError ReadRaw<T>(ref T value)
            where T : struct, INativeList<byte>, IUTF8Bytes
        {
            value.Clear();
            SkipWhitespace();
            var startOffset = offset;
            var skipErr = Skip();
            if (skipErr != JsonReadError.None)
            {
                offset = startOffset;
                return skipErr;
            }
            unsafe
            {
                var appendError = value.Append(text.GetUnsafePtr() + startOffset, offset - startOffset);
                switch (appendError)
                {
                    case FormatError.Overflow:
                        offset = startOffset;
                        return JsonReadError.Overflow;
                }
            }
            return JsonReadError.None;
        }

        public bool TryRead(JsonToken token)
        {
            if (token != Peek())
                return false;
            Read();
            return true;
        }

        private void SkipWhitespace()
        {
            while (offset < text.Length)
            {
                if (!text.Peek(offset).IsWhiteSpaceOrSeparator())
                    break;
                offset++;
            }
        }
    }
}
