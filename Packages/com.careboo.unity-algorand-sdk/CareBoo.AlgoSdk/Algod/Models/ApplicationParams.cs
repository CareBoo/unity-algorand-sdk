using System;
using AlgoSdk.MsgPack;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;

namespace AlgoSdk
{
    public struct ApplicationParams
        : IMessagePackObject
        , IEquatable<ApplicationParams>
        , INativeDisposable
    {
        public UnsafeList<byte> ApprovalProgram;
        public UnsafeList<byte> ClearStateProgram;
        public Address Creator;
        public Optional<ulong> ExtraProgramPages;
        public UnsafeList<TealKeyValue> GlobalState;
        public Optional<ApplicationStateSchema> GlobalStateSchema;
        public Optional<ApplicationStateSchema> LocalStateSchema;

        public JobHandle Dispose(JobHandle inputDeps)
        {
            return JobHandle.CombineDependencies(
                ApprovalProgram.Dispose(inputDeps),
                ClearStateProgram.Dispose(inputDeps),
                GlobalState.Dispose(inputDeps)
            );
        }

        public void Dispose()
        {
            ApprovalProgram.Dispose();
            ClearStateProgram.Dispose();
            GlobalState.Dispose();
        }

        public bool Equals(ApplicationParams other)
        {
            return this.Equals(ref other);
        }
    }
}

namespace AlgoSdk.MsgPack
{
    internal static partial class FieldMaps
    {
        internal static readonly Field<ApplicationParams>.Map applicationParamsFields =
            new Field<ApplicationParams>.Map()
                .Assign("approval-program", (ref ApplicationParams x) => ref x.ApprovalProgram, default(UnsafeListComparer<byte>))
                .Assign("clear-state-program", (ref ApplicationParams x) => ref x.ClearStateProgram, default(UnsafeListComparer<byte>))
                .Assign("creator", (ref ApplicationParams x) => ref x.Creator)
                .Assign("extra-program-pages", (ref ApplicationParams x) => ref x.ExtraProgramPages)
                .Assign("global-state", (ref ApplicationParams x) => ref x.GlobalState, default(UnsafeListComparer<TealKeyValue>))
                .Assign("global-state-schema", (ref ApplicationParams x) => ref x.GlobalStateSchema)
                .Assign("local-state-schema", (ref ApplicationParams x) => ref x.LocalStateSchema)
                ;
    }
}
