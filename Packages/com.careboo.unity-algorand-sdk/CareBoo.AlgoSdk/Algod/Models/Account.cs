using System.Collections.Generic;
using AlgoSdk.MsgPack;
using Unity.Collections;
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
        public NativeArray<ApplicationLocalState> ApplicationsLocalState;
        public Optional<ulong> ApplicationsTotalExtraPages;
        public Optional<ApplicationStateSchema> ApplicationsTotalSchema;
        public NativeArray<AssetHolding> Assets;
        public Optional<Address> AuthAddress;
        public NativeArray<Application> CreatedApplications;
        public NativeArray<Asset> CreatedAssets;
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
            {
                {"address", Field<Account>.Assign((ref Account a) => ref a.Address)},
                {"amount", Field<Account>.Assign((ref Account a) => ref a.Amount)},
                {"amount-without-pending-rewards", Field<Account>.Assign((ref Account a) => ref a.AmountWithoutPendingRewards)},
                {"apps-local-state", Field<Account>.Assign((ref Account a) => ref a.ApplicationsLocalState, default(NativeArrayComparer<ApplicationLocalState>))},
                {"apps-total-extra-pages", Field<Account>.Assign((ref Account a) => ref a.ApplicationsTotalExtraPages)},
                {"apps-total-schema", Field<Account>.Assign((ref Account a) => ref a.ApplicationsTotalSchema)},
                {"assets", Field<Account>.Assign((ref Account a) => ref a.Assets, default(NativeArrayComparer<AssetHolding>))},
                {"auth-addr", Field<Account>.Assign((ref Account a) => ref a.AuthAddress)},
                {"created-apps", Field<Account>.Assign((ref Account a) => ref a.CreatedApplications, default(NativeArrayComparer<Application>))},
                {"created-assets", Field<Account>.Assign((ref Account a) => ref a.CreatedAssets, default(NativeArrayComparer<Asset>))},
                {"participation", Field<Account>.Assign((ref Account a) => ref a.Participation)},
                {"pending-rewards", Field<Account>.Assign((ref Account a) => ref a.PendingRewards)},
                {"reward-base", Field<Account>.Assign((ref Account a) => ref a.RewardBase)},
                {"rewards", Field<Account>.Assign((ref Account a) => ref a.Rewards)},
                {"round", Field<Account>.Assign((ref Account a) => ref a.Round)},
                {"sig-type", Field<Account>.Assign((ref Account a) => ref a.SignatureType, default(SignatureTypeComparer))},
                {"status", Field<Account>.Assign((ref Account a) => ref a.Status)},
            };
    }
}
