using Unity.Collections;

namespace AlgoSdk.Json
{
    public ref partial struct JsonReader
    {
        public JsonReadError ReadString<T>(ref T value)
            where T : struct, INativeList<byte>, IUTF8Bytes
        {
            value.Clear();
            var resetOffset = offset;
            var t = Read();
            if (t != JsonToken.String)
            {
                offset = resetOffset;
                return JsonReadError.IncorrectType;
            }
            while (offset < text.Length)
            {
                var r = text.Read(ref offset);
                t = r.ToJsonToken();
                if (t == JsonToken.String)
                    break;
                if (t == JsonToken.EscapeChar)
                {
                    if (offset >= text.Length)
                    {
                        offset = resetOffset;
                        return JsonReadError.IncorrectFormat;
                    }
                    r = text.Read(ref offset);
                }
                value.Append(r);
            }
            if (t != JsonToken.String)
            {
                offset = resetOffset;
                return JsonReadError.Overflow;
            }
            return JsonReadError.None;
        }

        public JsonReadError SkipString()
        {
            var resetOffset = offset;
            var t = Read();
            while (offset < text.Length)
            {
                var r = text.Read(ref offset);
                t = r.ToJsonToken();
                if (t == JsonToken.String)
                    break;
                if (t == JsonToken.EscapeChar)
                {
                    if (offset >= text.Length)
                    {
                        offset = resetOffset;
                        return JsonReadError.IncorrectFormat;
                    }
                    text.Read(ref offset);
                }
            }
            if (t != JsonToken.String)
            {
                offset = resetOffset;
                return JsonReadError.Overflow;
            }
            return JsonReadError.None;
        }
    }
}
