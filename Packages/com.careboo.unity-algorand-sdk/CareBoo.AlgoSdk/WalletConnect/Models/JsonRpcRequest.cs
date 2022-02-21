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
        string JsonRpc { get; }

        /// <summary>
        /// The method name to call in this request.
        /// </summary>
        string Method { get; }
    }

    public struct JsonRpcRequest
        : IJsonRpcRequest<AlgoApiObject>
        , IEquatable<JsonRpcRequest>
    {
        public ulong Id { get; set; }

        public string JsonRpc { get; set; }

        public string Method { get; set; }

        public AlgoApiObject Params { get; set; }

        public bool Equals(JsonRpcRequest other)
        {
            return Params.Equals(other.Params)
                && Id.Equals(other.Id)
                && StringComparer.Equals(JsonRpc, other.JsonRpc)
                && StringComparer.Equals(Method, other.Method)
                ;
        }
    }
}
