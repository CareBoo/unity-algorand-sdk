using System;
using System.Collections.Generic;
using Unity.Collections;

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
                {typeof(DryrunRequest), dryrunRequestFields}
            };
        }

        internal static object GetFieldMap(Type t)
        {
            if (lookup.TryGetValue(t, out var map))
                return map;
            return null;
        }

        internal static SortedDictionary<FixedString64, Field<T>> GetFieldMap<T>()
            where T : struct
        {
            var fieldMap = GetFieldMap(typeof(T));
            return (SortedDictionary<FixedString64, Field<T>>)fieldMap;
        }
    }
}
