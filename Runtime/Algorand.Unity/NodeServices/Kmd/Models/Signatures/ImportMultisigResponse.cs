using System;

namespace Algorand.Unity.Kmd
{
    [AlgoApiObject]
    public partial struct ImportMultisigResponse
        : IEquatable<ImportMultisigResponse>
    {
        [AlgoApiField("address")]
        public Address Address;

        [AlgoApiField("error")]
        public Optional<bool> Error;

        [AlgoApiField("message")]
        public string Message;

        public bool Equals(ImportMultisigResponse other)
        {
            return Address.Equals(other.Address)
                && Error.Equals(other.Error)
                && Message.Equals(other.Message)
                ;
        }
    }
}
