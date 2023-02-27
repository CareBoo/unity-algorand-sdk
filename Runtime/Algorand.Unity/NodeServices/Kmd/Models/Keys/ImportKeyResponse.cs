using System;

namespace Algorand.Unity.Kmd
{
    [AlgoApiObject]
    public partial struct ImportKeyResponse
        : IEquatable<ImportKeyResponse>
    {
        [AlgoApiField("address")]
        public Address Address;

        [AlgoApiField("error")]
        public Optional<bool> Error;

        [AlgoApiField("message")]
        public string Message;

        public bool Equals(ImportKeyResponse other)
        {
            return Address.Equals(other.Address)
                && Error.Equals(other.Error)
                && Message.Equals(other.Message)
                ;
        }
    }
}
