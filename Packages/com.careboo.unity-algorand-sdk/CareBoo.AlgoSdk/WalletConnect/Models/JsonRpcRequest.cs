namespace AlgoSdk.WalletConnect
{
    public interface JsonRpcRequest<T> : JsonRpcRequest
    {
        T Params { get; set; }
    }

    public interface JsonRpcRequest
    {
        ulong Id { get; set; }
        string JsonRpc { get; }
        string Method { get; }
    }
}
