using System;

namespace AlgoSdk.WalletConnect
{
    public interface IJsonRpcResponse<T> : IJsonRpcResponse
    {
        /// <summary>
        /// The result of the corresponding <see cref="JsonRpcRequest"/>.
        /// </summary>
        T Result { get; set; }
    }

    public interface IJsonRpcResponse
    {
        /// <summary>
        /// The Id of the response.
        /// It should be the same as the ID of the corresponding <see cref="JsonRpcRequest"/>.
        /// </summary>
        ulong Id { get; set; }

        /// <summary>
        /// The JsonRpc version.
        /// </summary>
        string JsonRpc { get; set; }
    }

    [AlgoApiFormatter(typeof(JsonRpcResponseFormatter<JsonRpcResponse, AlgoApiObject>))]
    public struct JsonRpcResponse
        : IJsonRpcResponse<AlgoApiObject>
        , IEquatable<JsonRpcResponse>
    {
        public ulong Id { get; set; }

        public string JsonRpc { get; set; }

        public AlgoApiObject Result { get; set; }

        public bool Equals(JsonRpcResponse other)
        {
            return Result.Equals(other.Result)
                && Id.Equals(other.Id)
                && StringComparer.Equals(JsonRpc, other.JsonRpc)
                ;
        }
    }
}
