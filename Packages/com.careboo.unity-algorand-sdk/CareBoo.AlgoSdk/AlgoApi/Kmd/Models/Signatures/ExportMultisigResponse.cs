using System;
using AlgoSdk.Crypto;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct ExportMultisigResponse
        : IEquatable<ExportMultisigResponse>
    {
        [AlgoApiField("error", null)]
        public Optional<bool> Error;

        [AlgoApiField("message", null)]
        public string Message;

        [AlgoApiField("multisig_version", null)]
        public Optional<byte> MultisigVersion;

        [AlgoApiField("pks", null)]
        public Ed25519.PublicKey[] Pks;

        [AlgoApiField("threshold", null)]
        public Optional<byte> Threshold;

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
