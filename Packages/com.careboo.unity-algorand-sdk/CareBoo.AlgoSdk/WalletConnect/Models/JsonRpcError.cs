namespace AlgoSdk.WalletConnect
{
    public interface JsonRpcError
    {
        /// <summary>
        /// A human-readable message of the error.
        /// </summary>
        string Message { get; set; }
    }
}
