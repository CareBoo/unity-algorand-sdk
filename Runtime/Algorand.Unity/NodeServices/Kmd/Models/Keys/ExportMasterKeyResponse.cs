using System;

namespace Algorand.Unity.Kmd
{
    [AlgoApiObject]
    public partial struct ExportMasterKeyResponse
        : IEquatable<ExportMasterKeyResponse>
    {
        [AlgoApiField("master_derivation_key")]
        public PrivateKey MasterDerivationKey;

        [AlgoApiField("error")]
        public Optional<bool> Error;

        [AlgoApiField("message")]
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
