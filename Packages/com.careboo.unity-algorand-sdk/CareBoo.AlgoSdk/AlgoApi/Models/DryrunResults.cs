using System;
using Unity.Collections;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct DryrunResults
        : IEquatable<DryrunResults>
    {
        [AlgoApiKey("error", null)]
        public string Error;
        [AlgoApiKey("protocol-version", null)]
        public FixedString128Bytes ProtocolVersion;
        [AlgoApiKey("txns", null)]
        public DryrunTxnResult[] Txns;

        public bool Equals(DryrunResults other)
        {
            return StringComparer.Equals(Error, other.Error)
                && ProtocolVersion.Equals(other.ProtocolVersion)
                && ArrayComparer.Equals(Txns, other.Txns)
                ;
        }
    }
}
