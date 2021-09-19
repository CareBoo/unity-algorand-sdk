using System;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct Application
        : IEquatable<Application>
    {
        [AlgoApiKey("id")]
        public ulong Id;

        [AlgoApiKey("params")]
        public ApplicationParams Params;

        public bool Equals(Application other)
        {
            return Id.Equals(other.Id);
        }
    }
}
