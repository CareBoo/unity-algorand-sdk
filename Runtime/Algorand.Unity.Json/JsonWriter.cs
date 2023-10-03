using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using Algorand.Unity.Collections;

namespace Algorand.Unity.Json
{
    public struct JsonWriter
        : INativeDisposable
    {
        private NativeText text;

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

        public JsonWriter WriteRaw(NativeArray<byte> raw)
        {
            unsafe
            {
                text.Append((byte*)raw.GetUnsafeReadOnlyPtr(), raw.Length);
            }
            return this;
        }

        public JsonWriter WriteString(string s)
        {
            WriteChar('"');
            foreach (var c in s)
            {
                if (c == '"')
                    text.Append('\\'.ToRune());
                text.Append(c.ToRune());
            }
            WriteChar('"');
            return this;
        }

        public JsonWriter WriteString<T>(T fs)
            where T : unmanaged, INativeList<byte>, IUTF8Bytes
        {
            WriteChar('"');
            int index = 0;
            var rune = fs.Read(ref index);
            while (!rune.Equals(Unicode.BadRune))
            {
                var c = rune.ToChar();
                if (c == '"')
                    text.Append('\\'.ToRune());
                text.Append(rune);
                rune = fs.Read(ref index);
            }
            WriteChar('"');
            return this;
        }

        public JsonWriter WriteString(NativeText fs)
        {
            WriteChar('"');
            int index = 0;
            var rune = fs.Read(ref index);
            while (!rune.Equals(Unicode.BadRune))
            {
                var c = rune.ToChar();
                if (c == '"')
                    text.Append('\\'.ToRune());
                text.Append(rune);
                rune = fs.Read(ref index);
            }
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
            where T : unmanaged, INativeList<byte>, IUTF8Bytes
        {
            WriteString(fs);
            WriteChar(':');
            return this;
        }

        public JsonWriter WriteObjectKey(NativeText fs)
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
