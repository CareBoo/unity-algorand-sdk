namespace AlgoSdk.WalletConnect
{
    public interface JsonRpcResponse<T> : JsonRpcResponse
    {
        /// <summary>
        /// The result of the corresponding <see cref="JsonRpcRequest"/>.
        /// </summary>
        T Result { get; set; }
    }

    public interface JsonRpcResponse
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
}
