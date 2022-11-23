using System;
using Algorand.Unity.Json;
using Algorand.Unity.MessagePack;
using Unity.Collections.LowLevel.Unsafe;

namespace Algorand.Unity
{
    public sealed class IntEnumFormatter<T> : IAlgoApiFormatter<T>
        where T : Enum
    {
        public T Deserialize(ref JsonReader reader)
        {
            reader.ReadNumber(out int value);
            return UnsafeUtility.As<int, T>(ref value);
        }

        public T Deserialize(ref MessagePackReader reader)
        {
            var value = reader.ReadInt32();
            return UnsafeUtility.As<int, T>(ref value);
        }

        public void Serialize(ref JsonWriter writer, T value)
        {
            writer.WriteNumber(UnsafeUtility.As<T, int>(ref value));
        }

        public void Serialize(ref MessagePackWriter writer, T value)
        {
            writer.Write(UnsafeUtility.As<T, int>(ref value));
        }
    }
}
