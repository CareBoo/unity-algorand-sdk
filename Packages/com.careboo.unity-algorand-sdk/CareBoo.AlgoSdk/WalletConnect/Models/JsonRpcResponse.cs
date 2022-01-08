namespace AlgoSdk.WalletConnect
{
    public interface JsonRpcResponse<T>
    {
        ulong Id { get; set; }
        string JsonRpc { get; set; }
        T Result { get; set; }
    }
}
