using System;
using AlgoSdk.LowLevel;

namespace AlgoSdk
{
    public struct StateSchema
        : IByteArray
        , IEquatable<StateSchema>
    {
        public byte this[int index] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public IntPtr Buffer => IntPtr.Zero;

        public int Length => 0;

        public override bool Equals(object obj)
        {
            return ByteArray.Equals(in this, obj);
        }

        public bool Equals(StateSchema other)
        {
            return ByteArray.Equals(in this, in other);
        }

        public override int GetHashCode()
        {
            return ByteArray.GetHashCode(in this);
        }

        public static bool operator ==(in StateSchema x, in StateSchema y)
        {
            return ByteArray.Equals(in x, in y);
        }

        public static bool operator !=(in StateSchema x, in StateSchema y)
        {
            return !ByteArray.Equals(in x, in y);
        }
    }
}
