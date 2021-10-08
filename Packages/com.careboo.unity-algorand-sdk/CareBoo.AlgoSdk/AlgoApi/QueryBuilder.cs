using System;
using System.Globalization;
using AlgoSdk.Json;
using Unity.Collections;
using Unity.Jobs;

namespace AlgoSdk
{
    public struct QueryBuilder
        : INativeDisposable
    {
        const string DateTimeFormat = "yyyy-MM-dd'T'HH:mm:ss.fffzzz";
        NativeText text;

        public QueryBuilder(Allocator allocator)
        {
            text = new NativeText(allocator);
        }

        public QueryBuilder Add(string key, string value)
        {
            if (string.IsNullOrEmpty(value))
                return this;

            AddKey(key);
            text.Append(value);
            return this;
        }

        public QueryBuilder Add(string key, Optional<ulong> value)
        {
            if (!value.HasValue)
                return this;

            AddKey(key);
            text.Append(value.Value);
            return this;
        }

        public QueryBuilder Add(string key, Optional<bool> value)
        {
            if (!value.HasValue)
                return this;

            AddKey(key);
            text.Append(value.Value);
            return this;
        }

        public QueryBuilder Add<T>(string key, T value)
            where T : struct, IEquatable<T>
        {
            if (value.Equals(default))
                return this;

            AddKey(key);
            text.Append(value.ToString());
            return this;
        }

        public QueryBuilder Add(string key, DateTime dt)
        {
            if (dt.Equals(default))
                return this;

            AddKey(key);
            text.Append(dt.ToString(DateTimeFormat, DateTimeFormatInfo.InvariantInfo));
            return this;
        }

        public QueryBuilder Add<T>(string key, Optional<T> value)
            where T : unmanaged, IUTF8Bytes, INativeList<byte>, IEquatable<T>
        {
            if (!value.HasValue)
                return this;

            AddKey(key);
            text.Append(value.Value);
            return this;
        }

        public JobHandle Dispose(JobHandle inputDeps)
        {
            return text.IsCreated
                ? text.Dispose(inputDeps)
                : inputDeps
                ;
        }

        public void Dispose()
        {
            if (text.IsCreated)
                text.Dispose();
        }

        public override string ToString()
        {
            return text.ToString();
        }

        void AddKey(string key)
        {
            text.Append(text.Length == 0 ? '?'.ToRune() : '&'.ToRune());
            text.Append(key);
            text.Append('='.ToRune());
        }
    }
}
