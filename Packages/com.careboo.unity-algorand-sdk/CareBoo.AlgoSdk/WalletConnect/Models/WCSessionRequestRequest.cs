using System;

namespace AlgoSdk.WalletConnect
{
    [AlgoApiFormatter(typeof(JsonRpcRequestFormatter<WCSessionRequestRequest, WCSessionRequestParams>))]
    public struct WCSessionRequestRequest
        : IJsonRpcRequest<WCSessionRequestParams>
        , IEquatable<WCSessionRequestRequest>
    {
        public ulong Id { get; set; }

        public string JsonRpc => "2.0";

        public string Method => "wc_sessionRequest";

        public WCSessionRequestParams Params { get; set; }

        public bool Equals(WCSessionRequestRequest other)
        {
            return Id.Equals(other.Id);
        }
    }
}
