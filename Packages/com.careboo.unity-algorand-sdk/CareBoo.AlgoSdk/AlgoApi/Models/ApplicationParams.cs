using System;
using AlgoSdk.MsgPack;

namespace AlgoSdk
{
    public struct ApplicationParams
        : IMessagePackObject
        , IEquatable<ApplicationParams>
    {

        [AlgoApiKey("approval-program")]
        public byte[] ApprovalProgram;

        [AlgoApiKey("clear-state-program")]
        public byte[] ClearStateProgram;

        [AlgoApiKey("creator")]
        public Address Creator;

        [AlgoApiKey("extra-program-pages")]
        public Optional<ulong> ExtraProgramPages;

        [AlgoApiKey("global-state")]
        public TealKeyValue[] GlobalState;

        [AlgoApiKey("global-state-schema")]
        public Optional<ApplicationStateSchema> GlobalStateSchema;

        [AlgoApiKey("local-state-schema")]
        public Optional<ApplicationStateSchema> LocalStateSchema;

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
                .Assign("approval-program", (ref ApplicationParams x) => ref x.ApprovalProgram, ArrayComparer<byte>.Instance)
                .Assign("clear-state-program", (ref ApplicationParams x) => ref x.ClearStateProgram, ArrayComparer<byte>.Instance)
                .Assign("creator", (ref ApplicationParams x) => ref x.Creator)
                .Assign("extra-program-pages", (ref ApplicationParams x) => ref x.ExtraProgramPages)
                .Assign("global-state", (ref ApplicationParams x) => ref x.GlobalState, ArrayComparer<TealKeyValue>.Instance)
                .Assign("global-state-schema", (ref ApplicationParams x) => ref x.GlobalStateSchema)
                .Assign("local-state-schema", (ref ApplicationParams x) => ref x.LocalStateSchema)
                ;
    }
}
