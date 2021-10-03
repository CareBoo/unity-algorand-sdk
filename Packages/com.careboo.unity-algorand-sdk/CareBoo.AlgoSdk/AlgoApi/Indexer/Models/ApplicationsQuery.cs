using System;
using Unity.Collections;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct ApplicationsQuery
        : IEquatable<ApplicationsQuery>
    {
        [AlgoApiField("application-id", null)]
        public ulong ApplicationId;

        [AlgoApiField("include-all", null)]
        public Optional<bool> IncludeAll;

        [AlgoApiField("limit", null)]
        public ulong Limit;

        [AlgoApiField("next", null)]
        public FixedString128Bytes Next;

        public bool Equals(ApplicationsQuery other)
        {
            return ApplicationId.Equals(other.ApplicationId)
                && IncludeAll.Equals(other.IncludeAll)
                && Limit.Equals(other.Limit)
                && Next.Equals(other.Next)
                ;
        }
    }
}
