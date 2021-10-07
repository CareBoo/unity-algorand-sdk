using System;

namespace AlgoSdk.MessagePack
{
    public partial struct MessagePackWriter
    {
        public void Write(short value)
        {
            throw new NotImplementedException();
        }

        public void Write(int value)
        {
            throw new NotImplementedException();
        }

        public void Write(long value)
        {
            throw new NotImplementedException();
        }

        public void Write(ushort value)
        {
            throw new NotImplementedException();
        }

        public void Write(uint value)
        {
            throw new NotImplementedException();
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
