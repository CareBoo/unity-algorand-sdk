using System;
using AlgoSdk.MsgPack;
using Unity.Collections;

namespace AlgoSdk
{
    public struct Account
        : IMessagePackObject
        , IEquatable<Account>
    {
        public Address Address;
        public ulong Amount;
        public ulong AmountWithoutPendingRewards;
        public ApplicationLocalState[] ApplicationsLocalState;
        public Optional<ulong> ApplicationsTotalExtraPages;
        public Optional<ApplicationStateSchema> ApplicationsTotalSchema;
        public AssetHolding[] Assets;
        public Optional<Address> AuthAddress;
        public Application[] CreatedApplications;
        public Asset[] CreatedAssets;
        public Optional<AccountParticipation> Participation;
        public ulong PendingRewards;
        public Optional<ulong> RewardBase;
        public ulong Rewards;
        public ulong Round;
        public SignatureType SignatureType;
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
