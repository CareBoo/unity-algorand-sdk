using System;
using System.Collections.Generic;

namespace AlgoSdk.MsgPack
{
    internal static partial class FieldMaps
    {
        private static readonly Dictionary<Type, object> lookup;

        static FieldMaps()
        {
            lookup = new Dictionary<Type, object>()
            {
                {typeof(RawTransaction), rawTransactionFields},
                {typeof(RawSignedTransaction), rawSignedTransactionFields},
                {typeof(Account), accountFields},
                {typeof(ErrorResponse), errorResponseFields},
                {typeof(AccountParticipation), accountParticipationFields},
                {typeof(AccountStateDelta), accountStateDeltaFields},
                {typeof(Application), applicationFields},
                {typeof(ApplicationLocalState), applicationLocalStateFields},
                {typeof(ApplicationParams), applicationParamsFields},
                {typeof(ApplicationStateSchema), applicationStateSchemaFields},
                {typeof(Asset), assetFields},
                {typeof(AssetHolding), assetHoldingFields},
                {typeof(AssetParams), assetParamsFields},
                {typeof(BuildVersion), buildVersionFields},
                {typeof(CatchupMessage), catchupMessageFields},
                {typeof(DryrunRequest), dryrunRequestFields},
                {typeof(PendingTransactions), pendingTransactionsFields},
                {typeof(Block), blockFields}
            };
        }

        internal static Field<T>.Map GetFieldMap<T>()
            where T : struct
        {
            if (lookup.TryGetValue(typeof(T), out var map))
                return (Field<T>.Map)map;
            return null;
        }
    }
}
