namespace AlgoSdk
{
    public partial struct RawTransaction
    {
        [AlgoApiField(null, "votekey")]
        public Address VotePk
        {
            get => KeyRegistrationParams.VotePk;
            set => KeyRegistrationParams.VotePk = value;
        }

        [AlgoApiField(null, "selkey")]
        public VrfPubkey SelectionPk
        {
            get => KeyRegistrationParams.SelectionPk;
            set => KeyRegistrationParams.SelectionPk = value;
        }

        [AlgoApiField(null, "votefst")]
        public ulong VoteFirst
        {
            get => KeyRegistrationParams.VoteFirst;
            set => KeyRegistrationParams.VoteFirst = value;
        }

        [AlgoApiField(null, "votelst")]
        public ulong VoteLast
        {
            get => KeyRegistrationParams.VoteLast;
            set => KeyRegistrationParams.VoteLast = value;
        }

        [AlgoApiField(null, "votekd")]
        public ulong VoteKeyDilution
        {
            get => KeyRegistrationParams.VoteKeyDilution;
            set => KeyRegistrationParams.VoteKeyDilution = value;
        }

        [AlgoApiField(null, "nonpart")]
        public Optional<bool> NonParticipation
        {
            get => KeyRegistrationParams.NonParticipation;
            set => KeyRegistrationParams.NonParticipation = value;
        }
    }
}
