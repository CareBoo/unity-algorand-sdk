using System;

namespace Algorand.Unity.Kmd
{
    [AlgoApiObject]
    public partial struct ListKeysResponse
        : IEquatable<ListKeysResponse>
    {
        [AlgoApiField("addresses")]
        public Address[] Addresses;

        [AlgoApiField("error")]
        public Optional<bool> Error;

        [AlgoApiField("message")]
        public string Message;

        public bool Equals(ListKeysResponse other)
        {
            return ArrayComparer.Equals(Addresses, other.Addresses)
                && Error.Equals(other.Error)
                && Message.Equals(other.Message)
                ;
        }
    }
}
