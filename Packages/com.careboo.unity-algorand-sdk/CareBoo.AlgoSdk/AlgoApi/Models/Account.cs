using System;
using AlgoSdk.MsgPack;
using Unity.Collections;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct Account
        : IMessagePackObject
        , IEquatable<Account>
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
            return this.Equals(ref other);
        }
    }
}

namespace AlgoSdk.MsgPack
{
    internal static partial class FieldMaps
    {
        internal static readonly Field<Account>.Map accountFields =
            new Field<Account>.Map()
                .Assign("address", (ref Account a) => ref a.Address)
                .Assign("amount", (ref Account a) => ref a.Amount)
                .Assign("amount-without-pending-rewards", (ref Account a) => ref a.AmountWithoutPendingRewards)
                .Assign("apps-local-state", (ref Account a) => ref a.ApplicationsLocalState, ArrayComparer<ApplicationLocalState>.Instance)
                .Assign("apps-total-extra-pages", (ref Account a) => ref a.ApplicationsTotalExtraPages)
                .Assign("apps-total-schema", (ref Account a) => ref a.ApplicationsTotalSchema)
                .Assign("assets", (ref Account a) => ref a.Assets, ArrayComparer<AssetHolding>.Instance)
                .Assign("auth-addr", (ref Account a) => ref a.AuthAddress)
                .Assign("created-apps", (ref Account a) => ref a.CreatedApplications, ArrayComparer<Application>.Instance)
                .Assign("created-assets", (ref Account a) => ref a.CreatedAssets, ArrayComparer<Asset>.Instance)
                .Assign("participation", (ref Account a) => ref a.Participation)
                .Assign("pending-rewards", (ref Account a) => ref a.PendingRewards)
                .Assign("reward-base", (ref Account a) => ref a.RewardBase)
                .Assign("rewards", (ref Account a) => ref a.Rewards)
                .Assign("round", (ref Account a) => ref a.Round)
                .Assign("sig-type", (ref Account a) => ref a.SignatureType, SignatureTypeComparer.Instance)
                .Assign("status", (ref Account a) => ref a.Status)
                ;
    }
}
