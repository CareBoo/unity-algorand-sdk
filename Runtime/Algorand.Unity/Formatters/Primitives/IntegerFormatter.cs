using Algorand.Unity.Json;
using Algorand.Unity.MessagePack;

namespace Algorand.Unity.Formatters
{
    public class UInt64Formatter : IAlgoApiFormatter<ulong>
    {
        public ulong Deserialize(ref JsonReader reader)
        {
            reader.ReadNumber(out ulong val)
                .ThrowIfError(reader);
            return val;
        }

        public ulong Deserialize(ref MessagePackReader reader)
        {
            return reader.ReadUInt64();
        }

        public void Serialize(ref JsonWriter writer, ulong value)
        {
            writer.WriteNumber(value);
        }

        public void Serialize(ref MessagePackWriter writer, ulong value)
        {
            writer.Write(value);
        }
    }

    public class UInt32Formatter : IAlgoApiFormatter<uint>
    {
        public uint Deserialize(ref JsonReader reader)
        {
            reader.ReadNumber(out uint val)
                .ThrowIfError(reader);
            return val;
        }

        public uint Deserialize(ref MessagePackReader reader)
        {
            return reader.ReadUInt32();
        }

        public void Serialize(ref JsonWriter writer, uint value)
        {
            writer.WriteNumber(value);
        }

        public void Serialize(ref MessagePackWriter writer, uint value)
        {
            writer.Write(value);
        }
    }

    public class UInt16Formatter : IAlgoApiFormatter<ushort>
    {
        public ushort Deserialize(ref JsonReader reader)
        {
            reader.ReadNumber(out ushort val)
                .ThrowIfError(reader);
            return val;
        }

        public ushort Deserialize(ref MessagePackReader reader)
        {
            return reader.ReadUInt16();
        }

        public void Serialize(ref JsonWriter writer, ushort value)
        {
            writer.WriteNumber(value);
        }

        public void Serialize(ref MessagePackWriter writer, ushort value)
        {
            writer.Write(value);
        }
    }

    public class UInt8Formatter : IAlgoApiFormatter<byte>
    {
        public byte Deserialize(ref JsonReader reader)
        {
            reader.ReadNumber(out byte val)
                .ThrowIfError(reader);
            return val;
        }

        public byte Deserialize(ref MessagePackReader reader)
        {
            return reader.ReadUInt8();
        }

        public void Serialize(ref JsonWriter writer, byte value)
        {
            writer.WriteNumber(value);
        }

        public void Serialize(ref MessagePackWriter writer, byte value)
        {
            writer.Write(value);
        }
    }

    public class Int64Formatter : IAlgoApiFormatter<long>
    {
        public long Deserialize(ref JsonReader reader)
        {
            reader.ReadNumber(out long val)
                .ThrowIfError(reader);
            return val;
        }

        public long Deserialize(ref MessagePackReader reader)
        {
            return reader.ReadInt64();
        }

        public void Serialize(ref JsonWriter writer, long value)
        {
            writer.WriteNumber(value);
        }

        public void Serialize(ref MessagePackWriter writer, long value)
        {
            writer.Write(value);
        }
    }

    public class Int32Formatter : IAlgoApiFormatter<int>
    {
        public int Deserialize(ref JsonReader reader)
        {
            reader.ReadNumber(out int val)
                .ThrowIfError(reader);
            return val;
        }

        public int Deserialize(ref MessagePackReader reader)
        {
            return reader.ReadInt32();
        }

        public void Serialize(ref JsonWriter writer, int value)
        {
            writer.WriteNumber(value);
        }

        public void Serialize(ref MessagePackWriter writer, int value)
        {
            writer.Write(value);
        }
    }

    public class Int16Formatter : IAlgoApiFormatter<short>
    {
        public short Deserialize(ref JsonReader reader)
        {
            reader.ReadNumber(out short val)
                .ThrowIfError(reader);
            return val;
        }

        public short Deserialize(ref MessagePackReader reader)
        {
            return reader.ReadInt16();
        }

        public void Serialize(ref JsonWriter writer, short value)
        {
            writer.WriteNumber(value);
        }

        public void Serialize(ref MessagePackWriter writer, short value)
        {
            writer.Write(value);
        }
    }

    public class Int8Formatter : IAlgoApiFormatter<sbyte>
    {
        public sbyte Deserialize(ref JsonReader reader)
        {
            reader.ReadNumber(out sbyte val)
                .ThrowIfError(reader);
            return val;
        }

        public sbyte Deserialize(ref MessagePackReader reader)
        {
            return reader.ReadInt8();
        }

        public void Serialize(ref JsonWriter writer, sbyte value)
        {
            writer.WriteNumber(value);
        }

        public void Serialize(ref MessagePackWriter writer, sbyte value)
        {
            writer.Write(value);
        }
    }
}
