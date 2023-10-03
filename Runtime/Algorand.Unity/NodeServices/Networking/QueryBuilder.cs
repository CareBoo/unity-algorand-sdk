using System;
using Algorand.Unity.Json;
using Algorand.Unity.Collections;
using Unity.Collections;
using Unity.Jobs;

namespace Algorand.Unity
{
    internal struct QueryBuilder
        : INativeDisposable
    {
        private const string DateTimeFormat = "o";
        private NativeText text;

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

        public QueryBuilder Add(string key, byte[] value)
        {
            if (value == null)
                return this;
            return Add(key, System.Convert.ToBase64String(value));
        }

        public QueryBuilder Add(string key, ExcludeFields value) =>
            Add(key, value.ToOptionalFixedString());

        public QueryBuilder Add(string key, ResponseFormat value) =>
            Add(key, value.ToOptionalFixedString());

        public QueryBuilder Add(string key, ExcludeAccountFields value) =>
            Add(key, value.ToOptionalFixedString());

        public QueryBuilder Add(string key, AddressRole value) =>
            Add(key, value.ToOptionalFixedString());

        public QueryBuilder Add(string key, TransactionType value) =>
            Add(key, value.ToOptionalFixedString());

        public QueryBuilder Add(string key, SignatureType value) =>
            Add(key, value.ToOptionalFixedString());

        public QueryBuilder Add<T>(string key, T value, T defaultValue = default)
            where T : struct, IEquatable<T>
        {
            if (value.Equals(defaultValue))
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
            text.Append(dt.ToString(DateTimeFormat));
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

        private void AddKey(string key)
        {
            text.Append(text.Length == 0 ? '?'.ToRune() : '&'.ToRune());
            text.Append(key);
            text.Append('='.ToRune());
        }
    }
}
