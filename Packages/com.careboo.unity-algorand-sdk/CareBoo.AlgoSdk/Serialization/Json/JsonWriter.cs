using Unity.Collections;
using Unity.Jobs;

namespace AlgoSdk.Json
{
    public struct JsonWriter
        : INativeDisposable
    {
        NativeText text;

        public JsonWriter(Allocator allocator)
        {
            text = new NativeText(allocator);
        }

        public NativeText Text => text;

        public JsonWriter BeginObject() => WriteChar('{');

        public JsonWriter EndObject() => WriteChar('}');

        public JsonWriter BeginArray() => WriteChar('[');

        public JsonWriter EndArray() => WriteChar(']');

        public JsonWriter WriteRaw(NativeText raw)
        {
            text.Append(raw);
            return this;
        }

        public JsonWriter WriteString(string s)
        {
            WriteChar('"');
            text.Append(s);
            WriteChar('"');
            return this;
        }

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
            WriteString(fs);
            WriteChar(':');
            return this;
        }

        public JsonWriter WriteObjectKey(string s)
        {
            WriteString(s);
            WriteChar(':');
            return this;
        }

        public JsonWriter BeginNextItem() => WriteChar(',');

        public JsonWriter WriteChar(char c)
        {
            text.Append(c.ToRune());
            return this;
        }

        public JobHandle Dispose(JobHandle inputDeps)
        {
            return text.IsCreated
                ? text.Dispose(inputDeps)
                : inputDeps;
        }

        public void Dispose()
        {
            if (text.IsCreated)
                text.Dispose();
        }
    }
}
