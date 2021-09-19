using System;
using Unity.Collections;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct Account
        : IEquatable<Account>
    {
        [AlgoApiKey("address")]
        public Address Address;

        [AlgoApiKey("amount")]
        public ulong Amount;

        [AlgoApiKey("amount-without-pending-rewards")]
        public ulong AmountWithoutPendingRewards;

        [AlgoApiKey("apps-local-state")]
        public ApplicationLocalState[] ApplicationsLocalState;

        [AlgoApiKey("apps-total-extra-pages")]
        public Optional<ulong> ApplicationsTotalExtraPages;

        [AlgoApiKey("apps-total-schema")]
        public Optional<ApplicationStateSchema> ApplicationsTotalSchema;

        [AlgoApiKey("assets")]
        public AssetHolding[] Assets;

        [AlgoApiKey("auth-addr")]
        public Optional<Address> AuthAddress;

        [AlgoApiKey("created-apps")]
        public Application[] CreatedApplications;

        [AlgoApiKey("created-assets")]
        public Asset[] CreatedAssets;

        [AlgoApiKey("participation")]
        public Optional<AccountParticipation> Participation;

        [AlgoApiKey("pending-rewards")]
        public ulong PendingRewards;

        [AlgoApiKey("reward-base")]
        public Optional<ulong> RewardBase;

        [AlgoApiKey("rewards")]
        public ulong Rewards;

        [AlgoApiKey("round")]
        public ulong Round;

        [AlgoApiKey("sig-type")]
        public SignatureType SignatureType;

        [AlgoApiKey("status")]
        public FixedString32Bytes Status;

        public bool Equals(Account other)
        {
            return Address.Equals(other.Address);
        }
    }
}
