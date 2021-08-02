using System;
using System.Collections;
using System.Collections.Generic;
using AlgoSdk.LowLevel;
using UnityEngine;

namespace AlgoSdk
{
    public struct AssetParams
        : IByteArray
        , IEquatable<AssetParams>
    {
        public byte this[int index] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public IntPtr Buffer => IntPtr.Zero;

        public int Length => 0;

        public bool Equals(AssetParams other)
        {
            return ByteArray.Equals(in this, in other);
        }

        public override bool Equals(object obj)
        {
            return ByteArray.Equals(in this, obj);
        }

        public override int GetHashCode()
        {
            return ByteArray.GetHashCode(in this);
        }

        public static bool operator ==(in AssetParams x, in AssetParams y)
        {
            return ByteArray.Equals(in x, in y);
        }

        public static bool operator !=(in AssetParams x, in AssetParams y)
        {
            return !ByteArray.Equals(in x, in y);
        }
    }
}
