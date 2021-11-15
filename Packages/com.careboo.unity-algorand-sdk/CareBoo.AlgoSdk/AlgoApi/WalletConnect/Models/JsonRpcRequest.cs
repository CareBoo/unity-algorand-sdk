namespace AlgoSdk
{
    public interface JsonRpcRequest<T>
    {
        ulong Id { get; set; }
        string JsonRpc { get; }
        string Method { get; }
        T Params { get; set; }
    }
}
