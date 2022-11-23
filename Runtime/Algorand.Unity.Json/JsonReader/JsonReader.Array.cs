using Unity.Collections;

namespace Algorand.Unity.Json
{
    public ref partial struct JsonReader
    {
        private JsonReadError SkipArray()
        {
            var depth = 0;
            var t = JsonToken.None;
            do
            {
                t = text.Read(ref offset).ToJsonToken();
                switch (t)
                {
                    case JsonToken.EscapeChar:
                        if (offset >= text.Length)
                            return JsonReadError.IncorrectFormat;
                        offset++;
                        continue;
                    case JsonToken.ArrayBegin:
                        depth++; break;
                    case JsonToken.ArrayEnd:
                        depth--; break;
                }
            }
            while (offset < text.Length && depth > 0);
            return t == JsonToken.ArrayEnd
                ? JsonReadError.None
                : JsonReadError.IncorrectFormat
                ;
        }
    }
}
