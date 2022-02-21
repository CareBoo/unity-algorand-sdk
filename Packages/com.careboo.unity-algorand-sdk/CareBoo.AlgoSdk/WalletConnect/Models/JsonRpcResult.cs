using System;

namespace AlgoSdk.WalletConnect
{
    /// <summary>
    /// Either a <see cref="IJsonRpcResponse"/> or a <see cref="JsonRpcError"/>.
    /// </summary>
    public struct JsonRpcResult<TResponse, TError>
        where TResponse : IJsonRpcResponse
        where TError : JsonRpcError
    {
        /// <summary>
        /// Response of a <see cref="JsonRpcRequest"/> if <see cref="IsResponse"/>.
        /// </summary>
        public TResponse Response { get; private set; }

        /// <summary>
        /// Error of a <see cref="JsonRpcRequest"/> if <see cref="IsError"/>.
        /// </summary>
        public TError Error { get; private set; }

        /// <summary>
        /// Returns true if the result of the <see cref="JsonRpcRequest"/> is a response.
        /// </summary>
        public bool IsResponse { get; private set; }

        /// <summary>
        /// Returns true if the result of the <see cref="JsonRpcRequest"/> is an error.
        /// </summary>
        public bool IsError { get; private set; }

        public JsonRpcResult(TResponse response)
        {
            Response = response;
            Error = default;
            IsResponse = true;
            IsError = false;
        }

        public JsonRpcResult(TError error)
        {
            Response = default;
            Error = error;
            IsResponse = true;
            IsError = false;
        }
    }
}
