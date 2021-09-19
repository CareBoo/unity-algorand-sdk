using System;
using AlgoSdk.Json;
using AlgoSdk.MessagePack;
using Unity.Collections.LowLevel.Unsafe;

namespace AlgoSdk.Formatters
{
    public class EnumFormatter<T> : IAlgoApiFormatter<T>
        where T : Enum
    {
        private delegate T JsonDeserialize(ref JsonReader reader);
        private delegate void JsonSerialize(ref JsonWriter writer, ref T value);
        private delegate T MessagePackDeserialize(ref MessagePackReader reader);
        private delegate void MessagePackSerialize(ref MessagePackWriter writer, ref T value);

        private readonly JsonDeserialize jsonDeserialize;
        private readonly JsonSerialize jsonSerialize;
        private readonly MessagePackDeserialize messagePackDeserialize;
        private readonly MessagePackSerialize messagePackSerialize;

        public EnumFormatter()
        {
            var underlyingType = typeof(T).GetEnumUnderlyingType();
            switch (Type.GetTypeCode(underlyingType))
            {
#pragma warning disable SA1107 // Avoid multiple statements on same line.
                case TypeCode.Byte:
                    jsonSerialize = (ref JsonWriter writer, ref T value) => writer.WriteNumber(UnsafeUtility.As<T, Byte>(ref value));
                    jsonDeserialize = (ref JsonReader reader) => { reader.ReadNumber(out byte v); return UnsafeUtility.As<Byte, T>(ref v); };
                    messagePackSerialize = (ref MessagePackWriter writer, ref T value) => writer.Write(UnsafeUtility.As<T, Byte>(ref value));
                    messagePackDeserialize = (ref MessagePackReader reader) => { var v = reader.ReadByte(); return UnsafeUtility.As<Byte, T>(ref v); };
                    break;
                case TypeCode.Int16:
                    jsonSerialize = (ref JsonWriter writer, ref T value) => writer.WriteNumber(UnsafeUtility.As<T, Int16>(ref value));
                    jsonDeserialize = (ref JsonReader reader) => { reader.ReadNumber(out short v); return UnsafeUtility.As<Int16, T>(ref v); };
                    messagePackSerialize = (ref MessagePackWriter writer, ref T value) => writer.Write(UnsafeUtility.As<T, Int16>(ref value));
                    messagePackDeserialize = (ref MessagePackReader reader) => { var v = reader.ReadInt16(); return UnsafeUtility.As<Int16, T>(ref v); };
                    break;
                case TypeCode.Int32:
                    jsonSerialize = (ref JsonWriter writer, ref T value) => writer.WriteNumber(UnsafeUtility.As<T, Int32>(ref value));
                    jsonDeserialize = (ref JsonReader reader) => { reader.ReadNumber(out int v); return UnsafeUtility.As<Int32, T>(ref v); };
                    messagePackSerialize = (ref MessagePackWriter writer, ref T value) => writer.Write(UnsafeUtility.As<T, Int32>(ref value));
                    messagePackDeserialize = (ref MessagePackReader reader) => { var v = reader.ReadInt32(); return UnsafeUtility.As<Int32, T>(ref v); };
                    break;
                case TypeCode.Int64:
                    jsonSerialize = (ref JsonWriter writer, ref T value) => writer.WriteNumber(UnsafeUtility.As<T, Int64>(ref value));
                    jsonDeserialize = (ref JsonReader reader) => { reader.ReadNumber(out long v); return UnsafeUtility.As<Int64, T>(ref v); };
                    messagePackSerialize = (ref MessagePackWriter writer, ref T value) => writer.Write(UnsafeUtility.As<T, Int64>(ref value));
                    messagePackDeserialize = (ref MessagePackReader reader) => { var v = reader.ReadInt64(); return UnsafeUtility.As<Int64, T>(ref v); };
                    break;
                case TypeCode.SByte:
                    jsonSerialize = (ref JsonWriter writer, ref T value) => writer.WriteNumber(UnsafeUtility.As<T, SByte>(ref value));
                    jsonDeserialize = (ref JsonReader reader) => { reader.ReadNumber(out sbyte v); return UnsafeUtility.As<SByte, T>(ref v); };
                    messagePackSerialize = (ref MessagePackWriter writer, ref T value) => writer.Write(UnsafeUtility.As<T, SByte>(ref value));
                    messagePackDeserialize = (ref MessagePackReader reader) => { var v = reader.ReadSByte(); return UnsafeUtility.As<SByte, T>(ref v); };
                    break;
                case TypeCode.UInt16:
                    jsonSerialize = (ref JsonWriter writer, ref T value) => writer.WriteNumber(UnsafeUtility.As<T, UInt16>(ref value));
                    jsonDeserialize = (ref JsonReader reader) => { reader.ReadNumber(out ushort v); return UnsafeUtility.As<UInt16, T>(ref v); };
                    messagePackSerialize = (ref MessagePackWriter writer, ref T value) => writer.Write(UnsafeUtility.As<T, UInt16>(ref value));
                    messagePackDeserialize = (ref MessagePackReader reader) => { var v = reader.ReadUInt16(); return UnsafeUtility.As<UInt16, T>(ref v); };
                    break;
                case TypeCode.UInt32:
                    jsonSerialize = (ref JsonWriter writer, ref T value) => writer.WriteNumber(UnsafeUtility.As<T, UInt32>(ref value));
                    jsonDeserialize = (ref JsonReader reader) => { reader.ReadNumber(out uint v); return UnsafeUtility.As<UInt32, T>(ref v); };
                    messagePackSerialize = (ref MessagePackWriter writer, ref T value) => writer.Write(UnsafeUtility.As<T, UInt32>(ref value));
                    messagePackDeserialize = (ref MessagePackReader reader) => { var v = reader.ReadUInt32(); return UnsafeUtility.As<UInt32, T>(ref v); };
                    break;
                case TypeCode.UInt64:
                    jsonSerialize = (ref JsonWriter writer, ref T value) => writer.WriteNumber(UnsafeUtility.As<T, UInt64>(ref value));
                    jsonDeserialize = (ref JsonReader reader) => { reader.ReadNumber(out ulong v); return UnsafeUtility.As<UInt64, T>(ref v); };
                    messagePackSerialize = (ref MessagePackWriter writer, ref T value) => writer.Write(UnsafeUtility.As<T, UInt64>(ref value));
                    messagePackDeserialize = (ref MessagePackReader reader) => { var v = reader.ReadUInt64(); return UnsafeUtility.As<UInt64, T>(ref v); };
                    break;
                default:
                    break;
#pragma warning restore SA1107 // Avoid multiple statements on same line.
            }
        }

        public T Deserialize(ref JsonReader reader)
        {
            throw new NotImplementedException();
        }

        public T Deserialize(ref MessagePackReader reader)
        {
            throw new NotImplementedException();
        }

        public void Serialize(ref JsonWriter writer, T value)
        {
            throw new NotImplementedException();
        }

        public void Serialize(ref MessagePackWriter writer, T value)
        {
            throw new NotImplementedException();
        }
    }
}
