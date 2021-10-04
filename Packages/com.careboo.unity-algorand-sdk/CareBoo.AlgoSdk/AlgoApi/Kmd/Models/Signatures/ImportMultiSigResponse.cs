using System;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct ImportMultiSigResponse
        : IEquatable<ImportMultiSigResponse>
    {
        [AlgoApiField("address", null)]
        public Address Address;

        [AlgoApiField("error", null)]
        public Optional<bool> Error;

        [AlgoApiField("message", null)]
        public string Message;

        public bool Equals(ImportMultiSigResponse other)
        {
            return Address.Equals(other.Address)
                && Error.Equals(other.Error)
                && Message.Equals(other.Message)
                ;
        }
    }
}
