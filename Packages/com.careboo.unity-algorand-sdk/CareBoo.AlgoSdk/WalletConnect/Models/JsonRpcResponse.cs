namespace AlgoSdk.WalletConnect
{
    public interface JsonRpcResponse<T> : JsonRpcResponse
    {
        T Result { get; set; }
    }

    public interface JsonRpcResponse
    {
        ulong Id { get; set; }
        string JsonRpc { get; set; }
    }
}
