using Algorand.Unity.Collections;
using Unity.Collections;

namespace Algorand.Unity.Json
{
    public ref partial struct JsonReader
    {
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
            var err = ReadNull();
            return err == JsonReadError.None;
        }

        public JsonReadError SkipNull()
        {
            var resetOffset = offset;
            if (text.Found(ref offset, 'n', 'u', 'l', 'l'))
            {
                return JsonReadError.None;
            }
            offset = resetOffset;
            return JsonReadError.ParseError;
        }
    }
}
