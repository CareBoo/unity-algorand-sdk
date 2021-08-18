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
                {typeof(RawSignedTransaction), rawSignedTransactionFields}
            };
        }

        internal static object GetFieldMap(Type t)
        {
            return lookup[t];
        }

        internal static SortedDictionary<FixedString32, Field<T>> GetFieldMap<T>()
            where T : struct
        {
            var fieldMap = GetFieldMap(typeof(T));
            return (SortedDictionary<FixedString32, Field<T>>)fieldMap;
        }
    }
}
