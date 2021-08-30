using System;
using System.Collections.Generic;

namespace AlgoSdk.MsgPack
{
    public class TransactionTypeComparer : IEqualityComparer<TransactionType>
    {
        public bool Equals(TransactionType x, TransactionType y)
        {
            return x == y;
        }

        public int GetHashCode(TransactionType obj)
        {
            return obj.GetHashCode();
        }

        public static readonly TransactionTypeComparer Instance = new TransactionTypeComparer();
    }

    public class SignatureTypeComparer : IEqualityComparer<SignatureType>
    {
        public bool Equals(SignatureType x, SignatureType y)
        {
            return x == y;
        }

        public int GetHashCode(SignatureType obj)
        {
            return obj.GetHashCode();
        }

        public static readonly SignatureTypeComparer Instance = new SignatureTypeComparer();
    }

    public class ArrayComparer<T> : IEqualityComparer<T[]>
        where T : IEquatable<T>
    {
        public bool Equals(T[] x, T[] y)
        {
            if (x == null || y == null) return x == y;

            if (x.Length != y.Length) return false;

            for (var i = 0; i < x.Length; i++)
                if (!x[i].Equals(y[i]))
                    return false;
            return true;
        }

        public int GetHashCode(T[] obj)
        {
            return obj.GetHashCode();
        }

        public static readonly ArrayComparer<T> Instance = new ArrayComparer<T>();
    }

    public class StringComparer : IEqualityComparer<string>
    {
        public bool Equals(string x, string y)
        {
            return string.Equals(x, y);
        }

        public int GetHashCode(string obj)
        {
            return obj.GetHashCode();
        }

        public static readonly StringComparer Instance = new StringComparer();
    }
}
