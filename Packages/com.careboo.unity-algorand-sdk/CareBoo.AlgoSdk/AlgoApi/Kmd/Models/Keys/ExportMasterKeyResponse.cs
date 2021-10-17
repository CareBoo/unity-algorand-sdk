using System;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct ExportMasterKeyResponse
        : IEquatable<ExportMasterKeyResponse>
    {
        [AlgoApiField("master_derivation_key", null)]
        public PrivateKey MasterDerivationKey;

        [AlgoApiField("error", null)]
        public Optional<bool> Error;

        [AlgoApiField("message", null)]
        public string Message;

        public bool Equals(ExportMasterKeyResponse other)
        {
            return MasterDerivationKey.Equals(other.MasterDerivationKey)
                && Error.Equals(other.Error)
                && Message.Equals(other.Message)
                ;
        }
    }
}
