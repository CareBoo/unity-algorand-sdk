using System;
using AlgoSdk.Crypto;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct ExportMultiSigResponse
        : IEquatable<ExportMultiSigResponse>
    {
        [AlgoApiField("error", null)]
        public Optional<bool> Error;

        [AlgoApiField("message", null)]
        public string Message;

        [AlgoApiField("multisig_version", null)]
        public Optional<byte> MultiSigVersion;

        [AlgoApiField("pks", null)]
        public Ed25519.PublicKey[] Pks;

        [AlgoApiField("threshold", null)]
        public Optional<byte> Threshold;

        public bool Equals(ExportMultiSigResponse other)
        {
            return Error.Equals(other.Error)
                && Message.Equals(other.Message)
                && MultiSigVersion.Equals(other.MultiSigVersion)
                && ArrayComparer.Equals(Pks, other.Pks)
                && Threshold.Equals(other.Threshold)
                ;
        }
    }
}
