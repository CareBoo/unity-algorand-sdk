using Unity.Collections;

namespace Algorand.Unity.Json
{
    public ref partial struct JsonReader
    {
        private JsonReadError SkipObject()
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
                    case JsonToken.ObjectBegin:
                        depth++;
                        break;
                    case JsonToken.ObjectEnd:
                        depth--;
                        break;
                }
            }
            while (offset < text.Length && depth > 0);
            return t == JsonToken.ObjectEnd
                ? JsonReadError.None
                : JsonReadError.IncorrectFormat
                ;
        }
    }
}
