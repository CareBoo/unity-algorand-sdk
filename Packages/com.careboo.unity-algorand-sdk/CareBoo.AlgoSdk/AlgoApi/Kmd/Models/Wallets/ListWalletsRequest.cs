using System;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct ListWalletsRequest
        : IEquatable<ListWalletsRequest>
    {
        public bool Equals(ListWalletsRequest other)
        {
            return true;
        }
    }
}
