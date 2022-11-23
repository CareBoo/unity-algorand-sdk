using System;
using Algorand.Unity.Crypto;

namespace Algorand.Unity.Kmd
{
    [AlgoApiObject]
    public partial struct ExportMultisigResponse
        : IEquatable<ExportMultisigResponse>
    {
        [AlgoApiField("error")]
        public Optional<bool> Error;

        [AlgoApiField("message")]
        public string Message;

        [AlgoApiField("multisig_version")]
        public byte MultisigVersion;

        [AlgoApiField("pks")]
        public Ed25519.PublicKey[] Pks;

        [AlgoApiField("threshold")]
        public byte Threshold;

        public bool Equals(ExportMultisigResponse other)
        {
            return Error.Equals(other.Error)
                && Message.Equals(other.Message)
                && MultisigVersion.Equals(other.MultisigVersion)
                && ArrayComparer.Equals(Pks, other.Pks)
                && Threshold.Equals(other.Threshold)
                ;
        }
    }
}
