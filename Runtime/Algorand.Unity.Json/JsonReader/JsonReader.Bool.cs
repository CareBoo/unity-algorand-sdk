using Algorand.Unity.Collections;
using Unity.Collections;

namespace Algorand.Unity.Json
{
    public ref partial struct JsonReader
    {
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

        public JsonReadError SkipBool()
        {
            var resetOffset = offset;
            var c = text.Read(ref offset).ToChar();
            if (c == 't' && text.Found(ref offset, 'r', 'u', 'e'))
            {
                return JsonReadError.None;
            }
            if (c == 'f' && text.Found(ref offset, 'a', 'l', 's', 'e'))
            {
                return JsonReadError.None;
            }
            offset = resetOffset;
            return JsonReadError.ParseError;
        }
    }
}
