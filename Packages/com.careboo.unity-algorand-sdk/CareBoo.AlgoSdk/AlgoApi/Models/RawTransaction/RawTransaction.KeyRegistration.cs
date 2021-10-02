namespace AlgoSdk
{
    public partial struct RawTransaction
    {
        [AlgoApiField("vote-participation-key", "votekey")]
        public Address VotePk
        {
            get => KeyRegistrationParams.VotePk;
            set => KeyRegistrationParams.VotePk = value;
        }

        [AlgoApiField("selection-participation-key", "selkey")]
        public VrfPubkey SelectionPk
        {
            get => KeyRegistrationParams.SelectionPk;
            set => KeyRegistrationParams.SelectionPk = value;
        }

        [AlgoApiField("vote-first-valid", "votefst")]
        public ulong VoteFirst
        {
            get => KeyRegistrationParams.VoteFirst;
            set => KeyRegistrationParams.VoteFirst = value;
        }

        [AlgoApiField("vote-last-valid", "votelst")]
        public ulong VoteLast
        {
            get => KeyRegistrationParams.VoteLast;
            set => KeyRegistrationParams.VoteLast = value;
        }

        [AlgoApiField("vote-key-dilution", "votekd")]
        public ulong VoteKeyDilution
        {
            get => KeyRegistrationParams.VoteKeyDilution;
            set => KeyRegistrationParams.VoteKeyDilution = value;
        }

        [AlgoApiField("non-participation", "nonpart")]
        public Optional<bool> NonParticipation
        {
            get => KeyRegistrationParams.NonParticipation;
            set => KeyRegistrationParams.NonParticipation = value;
        }
    }
}
