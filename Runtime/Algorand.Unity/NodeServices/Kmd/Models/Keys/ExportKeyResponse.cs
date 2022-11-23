using System;
using Algorand.Unity.Crypto;

namespace Algorand.Unity.Kmd
{
    [AlgoApiObject]
    public partial struct ExportKeyResponse
        : IEquatable<ExportKeyResponse>
    {
        [AlgoApiField("error")]
        public Optional<bool> Error;

        [AlgoApiField("message")]
        public string Message;

        [AlgoApiField("private_key")]
        public PrivateKey PrivateKey;

        public bool Equals(ExportKeyResponse other)
        {
            return Error.Equals(other.Error)
                && Message.Equals(other.Message)
                && PrivateKey.Equals(other.PrivateKey)
                ;
        }
    }
}
