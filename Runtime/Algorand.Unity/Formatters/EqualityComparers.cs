using System;
using System.Collections.Generic;
using Algorand.Unity.Experimental.Abi;
using Unity.Collections.LowLevel.Unsafe;

namespace Algorand.Unity
{
    public struct ByteEnumComparer<T> : IEqualityComparer<T>
        where T : Enum
    {
        bool IEqualityComparer<T>.Equals(T x, T y) => Equals(x, y);
        int IEqualityComparer<T>.GetHashCode(T obj) => GetHashCode(obj);

        public static bool Equals(T x, T y)
        {
            return UnsafeUtility.As<T, byte>(ref x) == UnsafeUtility.As<T, byte>(ref y);
        }

        public static int GetHashCode(T obj)
        {
            return obj.GetHashCode();
        }

        public static readonly ByteEnumComparer<T> Instance = new ByteEnumComparer<T>();
    }

    public struct IntEnumComparer<T> : IEqualityComparer<T>
        where T : Enum
    {
        bool IEqualityComparer<T>.Equals(T x, T y) => Equals(x, y);
        int IEqualityComparer<T>.GetHashCode(T obj) => GetHashCode(obj);

        public static bool Equals(T x, T y)
        {
            return UnsafeUtility.As<T, int>(ref x) == UnsafeUtility.As<T, int>(ref y);
        }

        public static int GetHashCode(T obj)
        {
            return obj.GetHashCode();
        }

        public static readonly IntEnumComparer<T> Instance = new IntEnumComparer<T>();
    }

    public struct ArrayComparer<T, TComparer> : IEqualityComparer<T[]>
        where TComparer : struct, IEqualityComparer<T>
    {
        bool IEqualityComparer<T[]>.Equals(T[] x, T[] y) => Equals(x, y);
        int IEqualityComparer<T[]>.GetHashCode(T[] obj) => GetHashCode(obj);

        public static bool Equals(T[] x, T[] y)
        {
            if (x == null || y == null) return x == y;

            if (x.Length != y.Length) return false;

            for (var i = 0; i < x.Length; i++)
                if (!default(TComparer).Equals(x[i], y[i]))
                    return false;
            return true;
        }

        public static int GetHashCode(T[] obj)
        {
            return obj.GetHashCode();
        }

        public static readonly ArrayComparer<T, TComparer> Instance = new ArrayComparer<T, TComparer>();
    }

    public struct ArrayComparer<T> : IEqualityComparer<T[]>
        where T : IEquatable<T>
    {
        bool IEqualityComparer<T[]>.Equals(T[] x, T[] y) => Equals(x, y);
        int IEqualityComparer<T[]>.GetHashCode(T[] obj) => GetHashCode(obj);

        public static bool Equals(T[] x, T[] y)
        {
            if (x == null || y == null) return x == y;

            if (x.Length != y.Length) return false;

            for (var i = 0; i < x.Length; i++)
                if (!x[i].Equals(y[i]))
                    return false;
            return true;
        }

        public static int GetHashCode(T[] obj)
        {
            return obj.GetHashCode();
        }

        public static readonly ArrayComparer<T> Instance = new ArrayComparer<T>();
    }

    public static class ArrayComparer
    {
        public static bool Equals<T>(T[] x, T[] y) where T : IEquatable<T>
        {
            return ArrayComparer<T>.Equals(x, y);
        }

        public static bool Equals(string[] x, string[] y)
        {
            if (x == null || y == null) return x == y;

            if (x.Length != y.Length) return false;

            for (var i = 0; i < x.Length; i++)
                if (!StringComparer.Equals(x[i], y[i]))
                    return false;
            return true;
        }

        public static bool Equals(byte[][] x, byte[][] y)
        {
            if (x == null || y == null) return x == y;

            if (x.Length != y.Length) return false;

            for (var i = 0; i < x.Length; i++)
                if (!ArrayComparer.Equals(x[i], y[i]))
                    return false;
            return true;
        }
    }

    public struct StringComparer : IEqualityComparer<string>
    {
        bool IEqualityComparer<string>.Equals(string x, string y) => string.Equals(x, y);

        int IEqualityComparer<string>.GetHashCode(string obj) => obj.GetHashCode();

        public static readonly StringComparer Instance = new StringComparer();

        public static bool Equals(string x, string y) => string.Equals(x, y);
    }

    public struct IAbiTypeComparer : IEqualityComparer<IAbiType>
    {
        public static IAbiTypeComparer Instance => default;

        public bool Equals(IAbiType x, IAbiType y)
        {
            return StringComparer.Equals(x?.Name, y?.Name);
        }

        public int GetHashCode(IAbiType obj)
        {
            return obj?.Name.GetHashCode() ?? 0;
        }
    }
}
