using System;
using Unity.Collections;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct Account
        : IEquatable<Account>
    {
        [AlgoApiField("address", null)]
        public Address Address;

        [AlgoApiField("amount", null)]
        public ulong Amount;

        [AlgoApiField("amount-without-pending-rewards", null)]
        public ulong AmountWithoutPendingRewards;

        [AlgoApiField("apps-local-state", null)]
        public ApplicationLocalState[] ApplicationsLocalState;

        [AlgoApiField("apps-total-extra-pages", null)]
        public Optional<ulong> ApplicationsTotalExtraPages;

        [AlgoApiField("apps-total-schema", null)]
        public Optional<StateSchema> ApplicationsTotalSchema;

        [AlgoApiField("assets", null)]
        public AssetHolding[] Assets;

        [AlgoApiField("auth-addr", null)]
        public Optional<Address> AuthAddress;

        [AlgoApiField("closed-at-round", null, readOnly: true)]
        public ulong ClosedAtRound;

        [AlgoApiField("created-apps", null)]
        public Application[] CreatedApplications;

        [AlgoApiField("created-assets", null)]
        public Asset[] CreatedAssets;

        [AlgoApiField("created-at-round", null, readOnly: true)]
        public ulong CreatedAtRound;

        [AlgoApiField("deleted", null, readOnly: true)]
        public Optional<bool> Deleted;

        [AlgoApiField("participation", null)]
        public Optional<AccountParticipation> Participation;

        [AlgoApiField("pending-rewards", null)]
        public ulong PendingRewards;

        [AlgoApiField("reward-base", null)]
        public Optional<ulong> RewardBase;

        [AlgoApiField("rewards", null)]
        public ulong Rewards;

        [AlgoApiField("round", null)]
        public ulong Round;

        [AlgoApiField("sig-type", null)]
        public SignatureType SignatureType;

        [AlgoApiField("status", null)]
        public FixedString32Bytes Status;

        public bool Equals(Account other)
        {
            return Address.Equals(other.Address);
        }
    }
}
