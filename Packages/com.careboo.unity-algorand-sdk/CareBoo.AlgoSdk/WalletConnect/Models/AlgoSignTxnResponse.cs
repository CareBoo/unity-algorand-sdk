namespace AlgoSdk.WalletConnect
{
    [AlgoApiFormatter(typeof(JsonRpcResponseFormatter<AlgoSignTxnResponse, byte[][]>))]
    public struct AlgoSignTxnResponse
        : JsonRpcResponse<byte[][]>
    {
        public ulong Id { get; set; }
        public string JsonRpc { get; set; }
        public byte[][] Result { get; set; }
    }
}
