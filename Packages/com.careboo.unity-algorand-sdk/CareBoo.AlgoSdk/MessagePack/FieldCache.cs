using System.Collections.Generic;
using System.IO;
using Unity.Collections;

namespace AlgoSdk.MsgPack
{
    public static class FieldCache<T> where T : struct, IMessagePackObject
    {
        public static readonly SortedDictionary<FixedString32, FieldFor<T>> Map;

        static FieldCache()
        {
            Map = FieldMaps.GetFieldMap<T>();
            if (Map == null)
                throw new InvalidDataException($"There is no map set for type: {typeof(T)}");
        }
    }
}
