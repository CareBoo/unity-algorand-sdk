using Unity.Collections;

namespace AlgoSdk.Json
{
    public ref struct JsonWriter
    {
        NativeText text;

        public JsonWriter(NativeText text)
        {
            text.Clear();
            this.text = text;
        }

        public JsonWriter BeginObject() => WriteChar('{');

        public JsonWriter EndObject() => WriteChar('}');

        public JsonWriter BeginArray() => WriteChar('[');

        public JsonWriter EndArray() => WriteChar(']');

        public JsonWriter WriteString<T>(T fs)
            where T : struct, INativeList<byte>, IUTF8Bytes
        {
            WriteChar('"');
            text.Append(fs);
            WriteChar('"');
            return this;
        }

        public JsonWriter WriteNumber(float value)
        {
            text.Append(value);
            return this;
        }

        public JsonWriter WriteNumber(ulong value)
        {
            text.Append(value);
            return this;
        }

        public JsonWriter WriteNumber(long value)
        {
            text.Append(value);
            return this;
        }

        public JsonWriter WriteBool(bool value)
        {
            text.Append(value);
            return this;
        }

        public JsonWriter WriteNull()
        {
            text.Append("null");
            return this;
        }

        public JsonWriter WriteObjectKey<T>(T fs)
            where T : struct, INativeList<byte>, IUTF8Bytes
        {
            return WriteString(fs)
                .WriteChar(':');
        }

        public JsonWriter BeginNextItem() => WriteChar(',');

        public JsonWriter WriteChar(char c)
        {
            text.Append(c.ToRune());
            return this;
        }
    }
}
