using System;

namespace Algorand.Unity.JsonRpc
{
    public interface IJsonRpcRequest<T> : IJsonRpcRequest
    {
        /// <summary>
        ///     Parameters for this request.
        /// </summary>
        T Params { get; set; }
    }

    public interface IJsonRpcRequest
    {
        /// <summary>
        ///     The id of this request. Used to keep track of the correct response.
        /// </summary>
        Optional<ulong> Id { get; set; }

        /// <summary>
        ///     The JsonRpc version of this request.
        /// </summary>
        string JsonRpc { get; set; }

        /// <summary>
        ///     The method name to call in this request.
        /// </summary>
        string Method { get; set; }
    }

    [AlgoApiObject(IsStrict = true)]
    public partial struct JsonRpcRequest
        : IJsonRpcRequest<AlgoApiObject>
            , IEquatable<JsonRpcRequest>
    {
        public bool Equals(JsonRpcRequest other)
        {
            return Params.Equals(other.Params)
                   && Id.Equals(other.Id)
                   && StringComparer.Equals(JsonRpc, other.JsonRpc)
                   && StringComparer.Equals(Method, other.Method)
                ;
        }

        /// <inheritdoc />
        [AlgoApiField("id")]
        public Optional<ulong> Id { get; set; }

        /// <inheritdoc />
        [AlgoApiField("jsonrpc")]
        public string JsonRpc { get; set; }

        /// <inheritdoc />
        [AlgoApiField("method")]
        public string Method { get; set; }

        /// <inheritdoc />
        [AlgoApiField("params")]
        public AlgoApiObject Params { get; set; }
    }

    [AlgoApiObject(IsStrict = true)]
    public partial struct JsonRpcRequest<T>
        : IJsonRpcRequest<T>
            , IEquatable<JsonRpcRequest<T>>
        where T : IEquatable<T>
    {
        public bool Equals(JsonRpcRequest<T> other)
        {
            return Params.Equals(other.Params)
                   && Id.Equals(other.Id)
                   && StringComparer.Equals(JsonRpc, other.JsonRpc)
                   && StringComparer.Equals(Method, other.Method)
                ;
        }

        /// <inheritdoc />
        [AlgoApiField("id")]
        public Optional<ulong> Id { get; set; }

        /// <inheritdoc />
        [AlgoApiField("jsonrpc")]
        public string JsonRpc { get; set; }

        /// <inheritdoc />
        [AlgoApiField("method")]
        public string Method { get; set; }

        /// <inheritdoc />
        [AlgoApiField("params")]
        public T Params { get; set; }
    }
}