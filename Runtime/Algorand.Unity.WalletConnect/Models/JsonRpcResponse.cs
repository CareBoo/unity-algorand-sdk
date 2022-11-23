using System;

namespace Algorand.Unity.WalletConnect
{
    public interface IJsonRpcResponse<T> : IJsonRpcResponse
    {
        /// <summary>
        /// The result of the corresponding <see cref="IJsonRpcRequest"/>.
        /// </summary>
        T Result { get; set; }
    }

    public interface IJsonRpcResponse
    {
        /// <summary>
        /// The Id of the response.
        /// It should be the same as the ID of the corresponding <see cref="IJsonRpcRequest"/>.
        /// </summary>
        Optional<ulong> Id { get; set; }

        /// <summary>
        /// The JsonRpc version.
        /// </summary>
        string JsonRpc { get; set; }

        /// <summary>
        /// An error object if an error occurred.
        /// </summary>
        JsonRpcError Error { get; set; }

        /// <summary>
        /// Returns <c>true</c> if this is an error response.
        /// </summary>
        bool IsError { get; }
    }

    [AlgoApiObject(IsStrict = true)]
    public partial struct JsonRpcResponse
        : IJsonRpcResponse<AlgoApiObject>
        , IEquatable<JsonRpcResponse>
    {
        /// <inheritdoc />
        [AlgoApiField("id")]
        public Optional<ulong> Id { get; set; }

        /// <inheritdoc />
        [AlgoApiField("jsonrpc")]
        public string JsonRpc { get; set; }

        /// <inheritdoc />
        [AlgoApiField("result")]
        public AlgoApiObject Result { get; set; }

        /// <inheritdoc />
        [AlgoApiField("error")]
        public JsonRpcError Error { get; set; }

        /// <inheritdoc />
        public bool IsError => !Error.Equals(default);

        public bool Equals(JsonRpcResponse other)
        {
            return Result.Equals(other.Result)
                && Id.Equals(other.Id)
                && StringComparer.Equals(JsonRpc, other.JsonRpc)
                && Error.Equals(other.Error)
                ;
        }
    }
}
