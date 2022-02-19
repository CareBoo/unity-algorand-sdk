using System;

namespace AlgoSdk.WalletConnect
{
    /// <summary>
    /// A WalletConnect request used to sign Algorand transactions.
    /// </summary>
    [AlgoApiFormatter(typeof(JsonRpcRequestFormatter<AlgoSignTxnsRequest, SignTxnsParams>))]
    public struct AlgoSignTxnsRequest
        : JsonRpcRequest<SignTxnsParams>
        , IEquatable<AlgoSignTxnsRequest>
    {
        public ulong Id { get; set; }

        public string JsonRpc => "2.0";

        public string Method => "algo_signTxn";

        public SignTxnsParams Params { get; set; }

        public bool Equals(AlgoSignTxnsRequest other)
        {
            return Id.Equals(other.Id);
        }
    }
}
