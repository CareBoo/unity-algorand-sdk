using System;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct ApplicationParams
        : IEquatable<ApplicationParams>
    {
        [AlgoApiKey("approval-program", null)]
        public byte[] ApprovalProgram;

        [AlgoApiKey("clear-state-program", null)]
        public byte[] ClearStateProgram;

        [AlgoApiKey("creator", null)]
        public Address Creator;

        [AlgoApiKey("extra-program-pages", null)]
        public Optional<ulong> ExtraProgramPages;

        [AlgoApiKey("global-state", null)]
        public TealKeyValue[] GlobalState;

        [AlgoApiKey("global-state-schema", null)]
        public Optional<ApplicationStateSchema> GlobalStateSchema;

        [AlgoApiKey("local-state-schema", null)]
        public Optional<ApplicationStateSchema> LocalStateSchema;

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
