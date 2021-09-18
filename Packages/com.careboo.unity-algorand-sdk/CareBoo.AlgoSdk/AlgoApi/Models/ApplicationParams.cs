using System;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct ApplicationParams
        : IEquatable<ApplicationParams>
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
