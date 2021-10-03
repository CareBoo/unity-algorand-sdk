using System;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct ExportKeyResponse
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
            throw new NotImplementedException();
        }
    }
}
