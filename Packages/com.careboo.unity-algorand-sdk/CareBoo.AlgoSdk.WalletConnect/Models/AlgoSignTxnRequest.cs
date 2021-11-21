using System;

namespace AlgoSdk.WalletConnect
{
    [AlgoApiFormatter(typeof(JsonRpcRequestFormatter<AlgoSignTxnRequest, SignTxnParams>))]
    public struct AlgoSignTxnRequest
        : JsonRpcRequest<SignTxnParams>
        , IEquatable<AlgoSignTxnRequest>
    {
        public ulong Id { get; set; }
        public string JsonRpc => "2.0";
        public string Method => "algo_signTxn";
        public SignTxnParams Params { get; set; }

        public bool Equals(AlgoSignTxnRequest other)
        {
            return Id.Equals(other.Id);
        }
    }
}
