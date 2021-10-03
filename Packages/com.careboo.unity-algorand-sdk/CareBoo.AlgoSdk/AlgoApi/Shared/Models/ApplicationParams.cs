using System;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct ApplicationParams
        : IEquatable<ApplicationParams>
    {
        [AlgoApiField("approval-program", null)]
        public byte[] ApprovalProgram;

        [AlgoApiField("clear-state-program", null)]
        public byte[] ClearStateProgram;

        [AlgoApiField("creator", null)]
        public Address Creator;

        [AlgoApiField("extra-program-pages", null)]
        public Optional<ulong> ExtraProgramPages;

        [AlgoApiField("global-state", null)]
        public TealKeyValue[] GlobalState;

        [AlgoApiField("global-state-schema", null)]
        public Optional<StateSchema> GlobalStateSchema;

        [AlgoApiField("local-state-schema", null)]
        public Optional<StateSchema> LocalStateSchema;

        public bool Equals(ApplicationParams other)
        {
            return Creator.Equals(other.Creator)
                && ArrayComparer.Equals(ApprovalProgram, other.ApprovalProgram)
                && ArrayComparer.Equals(ClearStateProgram, other.ClearStateProgram)
                && ExtraProgramPages.Equals(other.ExtraProgramPages)
                && ArrayComparer.Equals(GlobalState, other.GlobalState)
                && GlobalStateSchema.Equals(other.GlobalStateSchema)
                && LocalStateSchema.Equals(other.LocalStateSchema)
                ;
        }
    }
}
