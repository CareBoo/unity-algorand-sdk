using System;
using System.Collections.Generic;
using AlgoSdk.MsgPack;
using Unity.Collections;
using Unity.Jobs;

namespace AlgoSdk
{
    public struct AccountParticipation
        : IEquatable<AccountParticipation>
        , IMessagePackObject
        , INativeDisposable
    {
        public NativeText SelectionParticipationKey;
        public ulong VoteFirstValid;
        public ulong VoteKeyDilution;
        public ulong VoteLastValid;
        public NativeText VoteParticipationKey;

        public JobHandle Dispose(JobHandle inputDeps)
        {
            var dispose1 = SelectionParticipationKey.Dispose(inputDeps);
            var dispose2 = VoteParticipationKey.Dispose(inputDeps);
            return JobHandle.CombineDependencies(dispose1, dispose2);
        }

        public void Dispose()
        {
            SelectionParticipationKey.Dispose();
            VoteParticipationKey.Dispose();
        }

        public bool Equals(AccountParticipation other)
        {
            return this.Equals(ref other);
        }
    }
}

namespace AlgoSdk.MsgPack
{
    internal static partial class FieldMaps
    {
        internal static readonly Dictionary<FixedString64, Field<AccountParticipation>> accountParticipationFields =
            new Dictionary<FixedString64, Field<AccountParticipation>>()
            {
                {"selection-participation-key", Field<AccountParticipation>.Assign((ref AccountParticipation x) => ref x.SelectionParticipationKey)},
                {"vote-first-valid", Field<AccountParticipation>.Assign((ref AccountParticipation x) => ref x.VoteFirstValid)},
                {"vote-key-dilution", Field<AccountParticipation>.Assign((ref AccountParticipation x) => ref x.VoteKeyDilution)},
                {"vote-last-valid", Field<AccountParticipation>.Assign((ref AccountParticipation x) => ref x.VoteLastValid)},
                {"vote-participation-key", Field<AccountParticipation>.Assign((ref AccountParticipation x) => ref x.VoteParticipationKey)},
            };
    }
}
