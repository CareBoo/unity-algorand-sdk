using System;
using Unity.Collections;

namespace AlgoSdk
{
    /// <summary>
    /// Response from <see cref="IAlgodClient.TealDryrun(Optional{DryrunRequest})"/>
    /// </summary>
    [AlgoApiObject]
    public struct DryrunResults
        : IEquatable<DryrunResults>
    {
        [AlgoApiField("error", null)]
        public string Error;

        /// <summary>
        /// Protocol version is the protocol version Dryrun was operated under.
        /// </summary>
        [AlgoApiField("protocol-version", null)]
        public FixedString128Bytes ProtocolVersion;

        [AlgoApiField("txns", null)]
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
