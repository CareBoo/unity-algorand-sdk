using AlgoSdk.MsgPack;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;

namespace AlgoSdk
{
    public struct Account
        : IMessagePackObject
        , INativeDisposable
    {
        public Address Address;
        public ulong Amount;
        public ulong AmountWithoutPendingRewards;
        public UnsafeList<ApplicationLocalState> ApplicationsLocalState;
        public Optional<ulong> ApplicationsTotalExtraPages;
        public Optional<ApplicationStateSchema> ApplicationsTotalSchema;
        public UnsafeList<AssetHolding> Assets;
        public Optional<Address> AuthAddress;
        public UnsafeList<Application> CreatedApplications;
        public UnsafeList<Asset> CreatedAssets;
        public Optional<AccountParticipation> Participation;
        public ulong PendingRewards;
        public Optional<ulong> RewardBase;
        public ulong Rewards;
        public ulong Round;
        public SignatureType SignatureType;
        public FixedString32 Status;

        public JobHandle Dispose(JobHandle inputDeps)
        {
            var dispose1 = ApplicationsLocalState.Dispose(inputDeps);
            var dispose2 = Assets.Dispose(inputDeps);
            dispose1 = JobHandle.CombineDependencies(dispose1, dispose2);
            var dispose3 = CreatedApplications.Dispose(inputDeps);
            var dispose4 = CreatedAssets.Dispose(inputDeps);
            dispose3 = JobHandle.CombineDependencies(dispose3, dispose4);
            return JobHandle.CombineDependencies(dispose1, dispose3);
        }

        public void Dispose()
        {
            ApplicationsLocalState.Dispose();
            Assets.Dispose();
            CreatedApplications.Dispose();
            CreatedAssets.Dispose();
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
                .Assign("apps-local-state", (ref Account a) => ref a.ApplicationsLocalState, default(UnsafeListComparer<ApplicationLocalState>))
                .Assign("apps-total-extra-pages", (ref Account a) => ref a.ApplicationsTotalExtraPages)
                .Assign("apps-total-schema", (ref Account a) => ref a.ApplicationsTotalSchema)
                .Assign("assets", (ref Account a) => ref a.Assets, default(UnsafeListComparer<AssetHolding>))
                .Assign("auth-addr", (ref Account a) => ref a.AuthAddress)
                .Assign("created-apps", (ref Account a) => ref a.CreatedApplications, default(UnsafeListComparer<Application>))
                .Assign("created-assets", (ref Account a) => ref a.CreatedAssets, default(UnsafeListComparer<Asset>))
                .Assign("participation", (ref Account a) => ref a.Participation)
                .Assign("pending-rewards", (ref Account a) => ref a.PendingRewards)
                .Assign("reward-base", (ref Account a) => ref a.RewardBase)
                .Assign("rewards", (ref Account a) => ref a.Rewards)
                .Assign("round", (ref Account a) => ref a.Round)
                .Assign("sig-type", (ref Account a) => ref a.SignatureType, default(SignatureTypeComparer))
                .Assign("status", (ref Account a) => ref a.Status)
                ;
    }
}
