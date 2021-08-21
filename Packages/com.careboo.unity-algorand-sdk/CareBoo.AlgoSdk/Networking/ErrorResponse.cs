using System;
using AlgoSdk.MsgPack;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;

namespace AlgoSdk
{
    public struct ErrorResponse
        : INativeDisposable
        , IMessagePackObject
        , IEquatable<ErrorResponse>
    {
        public UnsafeText Data;
        public UnsafeText Message;

        public ErrorResponse(string message, string data, Allocator allocator)
        {
            Message = UnsafeTextFromString(message, allocator);
            Data = UnsafeTextFromString(message, allocator);
        }

        public ErrorResponse(string message, Allocator allocator)
            : this(message, null, allocator)
        {
        }

        private static UnsafeText UnsafeTextFromString(string value, Allocator allocator)
        {
            if (string.IsNullOrEmpty(value)) return default;
            var text = new UnsafeText(value.Length * 2, allocator);
            text.Append(value);
            return text;
        }

        public JobHandle Dispose(JobHandle inputDeps)
        {
            return JobHandle.CombineDependencies(
                Data.Dispose(inputDeps),
                Message.Dispose(inputDeps)
            );
        }

        public void Dispose()
        {
            if (Data.IsCreated)
                Data.Dispose();
            if (Message.IsCreated)
                Message.Dispose();
        }

        public bool Equals(ErrorResponse other)
        {
            return this.Equals(ref other);
        }
    }
}

namespace AlgoSdk.MsgPack
{
    internal static partial class FieldMaps
    {
        internal static readonly Field<ErrorResponse>.Map errorResponseFields =
            new Field<ErrorResponse>.Map()
                .Assign("data", (ref ErrorResponse x) => ref x.Data, default(UnsafeTextComparer))
                .Assign("message", (ref ErrorResponse x) => ref x.Message, default(UnsafeTextComparer))
                ;
    }
}
