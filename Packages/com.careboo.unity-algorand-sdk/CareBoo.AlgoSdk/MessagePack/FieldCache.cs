using System.IO;

namespace AlgoSdk.MsgPack
{
    public static class FieldCache<T> where T : struct, IMessagePackObject
    {
        public static readonly Field<T>.Map Map;

        static FieldCache()
        {
            Map = FieldMaps.GetFieldMap<T>();
            if (Map == null)
                throw new InvalidDataException($"There is no map set for type: {typeof(T)}");
        }
    }
}
