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
            };
        }

        internal static object GetFieldMap(Type t)
        {
            return lookup[t];
        }

        internal static SortedDictionary<FixedString64, Field<T>> GetFieldMap<T>()
            where T : struct
        {
            var fieldMap = GetFieldMap(typeof(T));
            return (SortedDictionary<FixedString64, Field<T>>)fieldMap;
        }
    }
}
