using System;
using AlgoSdk.MsgPack;
using Unity.Collections;

namespace AlgoSdk
{
    public struct ErrorResponse
        : IMessagePackObject
        , IEquatable<ErrorResponse>
    {
        public string Data;
        public string Message;

        public ErrorResponse(string message, string data)
        {
            Message = message;
            Data = data;
        }

        public ErrorResponse(string message)
            : this(message, null)
        {
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
                .Assign("data", (ref ErrorResponse x) => ref x.Data)
                .Assign("message", (ref ErrorResponse x) => ref x.Message)
                ;
    }
}
