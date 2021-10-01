namespace AlgoSdk
{
    public partial struct RawTransaction
    {
        [AlgoApiKey("vote-participation-key", "votekey")]
        public Address VotePk
        {
            get => KeyRegistrationParams.VotePk;
            set => KeyRegistrationParams.VotePk = value;
        }

        [AlgoApiKey("selection-participation-key", "selkey")]
        public VrfPubkey SelectionPk
        {
            get => KeyRegistrationParams.SelectionPk;
            set => KeyRegistrationParams.SelectionPk = value;
        }

        [AlgoApiKey("vote-first-valid", "votefst")]
        public ulong VoteFirst
        {
            get => KeyRegistrationParams.VoteFirst;
            set => KeyRegistrationParams.VoteFirst = value;
        }

        [AlgoApiKey("vote-last-valid", "votelst")]
        public ulong VoteLast
        {
            get => KeyRegistrationParams.VoteLast;
            set => KeyRegistrationParams.VoteLast = value;
        }

        [AlgoApiKey("vote-key-dilution", "votekd")]
        public ulong VoteKeyDilution
        {
            get => KeyRegistrationParams.VoteKeyDilution;
            set => KeyRegistrationParams.VoteKeyDilution = value;
        }

        [AlgoApiKey("non-participation", "nonpart")]
        public Optional<bool> NonParticipation
        {
            get => KeyRegistrationParams.NonParticipation;
            set => KeyRegistrationParams.NonParticipation = value;
        }
    }
}
