using System;
using Unity.Collections;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct Account
        : IEquatable<Account>
    {
        [AlgoApiKey("address", null)]
        public Address Address;

        [AlgoApiKey("amount", null)]
        public ulong Amount;

        [AlgoApiKey("amount-without-pending-rewards", null)]
        public ulong AmountWithoutPendingRewards;

        [AlgoApiKey("apps-local-state", null)]
        public ApplicationLocalState[] ApplicationsLocalState;

        [AlgoApiKey("apps-total-extra-pages", null)]
        public Optional<ulong> ApplicationsTotalExtraPages;

        [AlgoApiKey("apps-total-schema", null)]
        public Optional<ApplicationStateSchema> ApplicationsTotalSchema;

        [AlgoApiKey("assets", null)]
        public AssetHolding[] Assets;

        [AlgoApiKey("auth-addr", null)]
        public Optional<Address> AuthAddress;

        [AlgoApiKey("created-apps", null)]
        public Application[] CreatedApplications;

        [AlgoApiKey("created-assets", null)]
        public Asset[] CreatedAssets;

        [AlgoApiKey("participation", null)]
        public Optional<AccountParticipation> Participation;

        [AlgoApiKey("pending-rewards", null)]
        public ulong PendingRewards;

        [AlgoApiKey("reward-base", null)]
        public Optional<ulong> RewardBase;

        [AlgoApiKey("rewards", null)]
        public ulong Rewards;

        [AlgoApiKey("round", null)]
        public ulong Round;

        [AlgoApiKey("sig-type", null)]
        public SignatureType SignatureType;

        [AlgoApiKey("status", null)]
        public FixedString32Bytes Status;

        public bool Equals(Account other)
        {
            return Address.Equals(other.Address);
        }
    }
}
