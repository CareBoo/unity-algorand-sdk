namespace AlgoSdk.WalletConnect
{
    public interface JsonRpcRequest<T> : JsonRpcRequest
    {
        /// <summary>
        /// Parameters for this request.
        /// </summary>
        T Params { get; set; }
    }

    public interface JsonRpcRequest
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
}
