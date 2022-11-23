using System;

namespace Algorand.Unity.WalletConnect
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
        Optional<ulong> Id { get; set; }

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
