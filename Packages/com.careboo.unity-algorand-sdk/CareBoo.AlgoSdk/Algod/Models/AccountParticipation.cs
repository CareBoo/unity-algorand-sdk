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
        internal static readonly Field<AccountParticipation>.Map accountParticipationFields =
            new Field<AccountParticipation>.Map()
                .Assign("selection-participation-key", (ref AccountParticipation x) => ref x.SelectionParticipationKey, default(NativeTextComparer))
                .Assign("vote-first-valid", (ref AccountParticipation x) => ref x.VoteFirstValid)
                .Assign("vote-key-dilution", (ref AccountParticipation x) => ref x.VoteKeyDilution)
                .Assign("vote-last-valid", (ref AccountParticipation x) => ref x.VoteLastValid)
                .Assign("vote-participation-key", (ref AccountParticipation x) => ref x.VoteParticipationKey, default(NativeTextComparer))
                ;
    }
}
