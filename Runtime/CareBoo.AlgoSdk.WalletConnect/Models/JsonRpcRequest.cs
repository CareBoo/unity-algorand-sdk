using System;

namespace AlgoSdk.WalletConnect
{
    public interface IJsonRpcRequest<T> : IJsonRpcRequest
    {
        /// <summary>
        /// Parameters for this request.
        /// </summary>
        T Params { get; set; }
    }

    public interface IJsonRpcRequest
    {
        /// <summary>
        /// The id of this request. Used to keep track of the correct response.
        /// </summary>
        ulong Id { get; set; }

        /// <summary>
        /// The JsonRpc version of this request.
        /// </summary>
        string JsonRpc { get; set; }

        /// <summary>
        /// The method name to call in this request.
        /// </summary>
        string Method { get; set; }
    }

    [AlgoApiObject(IsStrict = true)]
    public partial struct JsonRpcRequest
        : IJsonRpcRequest<AlgoApiObject[]>
        , IEquatable<JsonRpcRequest>
    {
        /// <inheritdoc />
        [AlgoApiField("id", null)]
        public ulong Id { get; set; }

        /// <inheritdoc />
        [AlgoApiField("jsonrpc", null)]
        public string JsonRpc { get; set; }

        /// <inheritdoc />
        [AlgoApiField("method", null)]
        public string Method { get; set; }

        /// <inheritdoc />
        [AlgoApiField("params", null)]
        public AlgoApiObject[] Params { get; set; }

        public bool Equals(JsonRpcRequest other)
        {
            return ArrayComparer.Equals(Params, other.Params)
                && Id.Equals(other.Id)
                && StringComparer.Equals(JsonRpc, other.JsonRpc)
                && StringComparer.Equals(Method, other.Method)
                ;
        }
    }
}
