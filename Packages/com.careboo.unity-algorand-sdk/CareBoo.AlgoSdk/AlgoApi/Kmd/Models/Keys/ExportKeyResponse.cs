using System;
using AlgoSdk.Crypto;

namespace AlgoSdk
{
    [AlgoApiObject]
    public partial struct ExportKeyResponse
        : IEquatable<ExportKeyResponse>
    {
        [AlgoApiField("error", null)]
        public Optional<bool> Error;

        [AlgoApiField("message", null)]
        public string Message;

        [AlgoApiField("private_key", null)]
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
