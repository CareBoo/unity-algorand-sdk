using System;

namespace Algorand.Unity.JsonRpc
{
    public interface IJsonRpcError
    {
        /// <summary>
        ///     A String providing a short description of the error.
        /// </summary>
        string Message { get; set; }

        /// <summary>
        ///     A Number that indicates the error type that occurred.
        /// </summary>
        int Code { get; set; }

        /// <summary>
        ///     A Primitive or Structured value that contains additional information about the error.
        /// </summary>
        AlgoApiObject Data { get; set; }
    }

    [AlgoApiObject]
    public partial struct JsonRpcError
        : IEquatable<JsonRpcError>
            , IJsonRpcError
    {
        public bool Equals(JsonRpcError other)
        {
            return Code == other.Code
                   && StringComparer.Equals(Message, other.Message)
                   && Data.Equals(other.Data)
                ;
        }

        /// <inheritdoc />
        [AlgoApiField("code")]
        public int Code { get; set; }

        /// <inheritdoc />
        [AlgoApiField("message")]
        public string Message { get; set; }

        /// <inheritdoc />
        [AlgoApiField("data")]
        public AlgoApiObject Data { get; set; }
    }
}