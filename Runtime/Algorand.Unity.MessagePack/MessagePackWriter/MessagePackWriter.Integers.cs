namespace Algorand.Unity.MessagePack
{
    public partial struct MessagePackWriter
    {
        public void Write(sbyte value)
        {
            if (value >= 0)
            {
                Write((byte)value);
            }
            else
            {
                if (value >= MessagePackRange.MinFixNegativeInt)
                {
                    data.Add(unchecked((byte)value));
                }
                else
                {
                    data.Add(MessagePackCode.Int8);
                    data.Add(unchecked((byte)value));
                }
            }
        }

        public void Write(short value)
        {
            if (value >= 0)
            {
                Write((ushort)value);
            }
            else
            {
                if (value >= MessagePackRange.MinFixNegativeInt)
                {
                    data.Add(unchecked((byte)value));
                }
                else if (value >= sbyte.MinValue)
                {
                    data.Add(MessagePackCode.Int8);
                    data.Add(unchecked((byte)value));
                }
                else
                {
                    data.Add(MessagePackCode.Int16);
                    WriteBigEndian(value);
                }
            }
        }

        public void Write(int value)
        {
            if (value >= 0)
            {
                Write((uint)value);
            }
            else
            {
                if (value >= MessagePackRange.MinFixNegativeInt)
                {
                    data.Add(unchecked((byte)value));
                }
                else if (value >= sbyte.MinValue)
                {
                    data.Add(MessagePackCode.Int8);
                    data.Add(unchecked((byte)value));
                }
                else if (value >= short.MinValue)
                {
                    data.Add(MessagePackCode.Int16);
                    WriteBigEndian((short)value);
                }
                else
                {
                    data.Add(MessagePackCode.Int32);
                    WriteBigEndian(value);
                }
            }
        }

        public void Write(long value)
        {
            if (value >= 0)
            {
                Write((ulong)value);
            }
            else
            {
                if (value >= MessagePackRange.MinFixNegativeInt)
                {
                    data.Add(unchecked((byte)value));
                }
                else if (value >= sbyte.MinValue)
                {
                    data.Add(MessagePackCode.Int8);
                    data.Add(unchecked((byte)value));
                }
                else if (value >= short.MinValue)
                {
                    data.Add(MessagePackCode.Int16);
                    WriteBigEndian((short)value);
                }
                else if (value >= int.MinValue)
                {
                    data.Add(MessagePackCode.Int32);
                    WriteBigEndian((int)value);
                }
                else
                {
                    data.Add(MessagePackCode.Int64);
                    WriteBigEndian(value);
                }
            }
        }

        public void Write(byte value)
        {
            if (value <= MessagePackRange.MaxFixPositiveInt)
            {
                data.Add(unchecked((byte)value));
            }
            else
            {
                data.Add(MessagePackCode.UInt8);
                data.Add(unchecked((byte)value));
            }
        }

        public void Write(ushort value)
        {
            if (value <= MessagePackRange.MaxFixPositiveInt)
            {
                data.Add(unchecked((byte)value));
            }
            else if (value <= byte.MaxValue)
            {
                data.Add(MessagePackCode.UInt8);
                data.Add(unchecked((byte)value));
            }
            else
            {
                data.Add(MessagePackCode.UInt16);
                WriteBigEndian((ushort)value);
            }
        }

        public void Write(uint value)
        {
            if (value <= MessagePackRange.MaxFixPositiveInt)
            {
                data.Add(unchecked((byte)value));
            }
            else if (value <= byte.MaxValue)
            {
                data.Add(MessagePackCode.UInt8);
                data.Add(unchecked((byte)value));
            }
            else if (value <= ushort.MaxValue)
            {
                data.Add(MessagePackCode.UInt16);
                WriteBigEndian((ushort)value);
            }
            else
            {
                data.Add(MessagePackCode.UInt32);
                WriteBigEndian((uint)value);
            }
        }

        public void Write(ulong value)
        {
            if (value <= MessagePackRange.MaxFixPositiveInt)
            {
                data.Add(unchecked((byte)value));
            }
            else if (value <= byte.MaxValue)
            {
                data.Add(MessagePackCode.UInt8);
                data.Add(unchecked((byte)value));
            }
            else if (value <= ushort.MaxValue)
            {
                data.Add(MessagePackCode.UInt16);
                WriteBigEndian((ushort)value);
            }
            else if (value <= uint.MaxValue)
            {
                data.Add(MessagePackCode.UInt32);
                WriteBigEndian((uint)value);
            }
            else
            {
                data.Add(MessagePackCode.UInt64);
                WriteBigEndian(value);
            }
        }
    }
}
