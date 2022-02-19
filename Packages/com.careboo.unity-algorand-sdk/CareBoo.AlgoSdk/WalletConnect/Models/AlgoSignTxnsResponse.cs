namespace AlgoSdk.WalletConnect
{
    /// <summary>
    /// Response of calling <see cref="AlgoSignTxnsRequest"/>.
    /// </summary>
    [AlgoApiFormatter(typeof(JsonRpcResponseFormatter<AlgoSignTxnsResponse, byte[][]>))]
    public struct AlgoSignTxnsResponse
        : JsonRpcResponse<byte[][]>
    {
        public ulong Id { get; set; }
        public string JsonRpc { get; set; }

        /// <summary>Result from <see cref="AlgoSignTxnsRequest"/>.</summary>
        /// 
        /// <remarks>
        /// This array is the same length as the number of transactions provided in
        /// <see cref="AlgoSignTxnsRequest.Params"/>.
        /// 
        /// For every index in this result, the value is
        /// <list type="bullet">
        ///     <item><c>null</c> if the wallet was not requested to sign this transaction</item>
        ///     <item>the canonical message pack encoding of the signed transaction</item>
        /// </list>
        /// </remarks>
        public byte[][] Result { get; set; }
    }
}
