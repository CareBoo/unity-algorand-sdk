using System;
using System.Collections.Generic;

namespace AlgoSdk
{
    public class TransactionTypeComparer : IEqualityComparer<TransactionType>
    {
        bool IEqualityComparer<TransactionType>.Equals(TransactionType x, TransactionType y) => Equals(x, y);
        int IEqualityComparer<TransactionType>.GetHashCode(TransactionType obj) => GetHashCode(obj);

        public static bool Equals(TransactionType x, TransactionType y)
        {
            return x == y;
        }

        public static int GetHashCode(TransactionType obj)
        {
            return obj.GetHashCode();
        }

        public static readonly TransactionTypeComparer Instance = new TransactionTypeComparer();
    }

    public class SignatureTypeComparer : IEqualityComparer<SignatureType>
    {
        bool IEqualityComparer<SignatureType>.Equals(SignatureType x, SignatureType y) => Equals(x, y);
        int IEqualityComparer<SignatureType>.GetHashCode(SignatureType obj) => GetHashCode(obj);

        public static bool Equals(SignatureType x, SignatureType y)
        {
            return x == y;
        }

        public static int GetHashCode(SignatureType obj)
        {
            return obj.GetHashCode();
        }

        public static readonly SignatureTypeComparer Instance = new SignatureTypeComparer();
    }

    public class ArrayComparer<T> : IEqualityComparer<T[]>
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
                if (!StringComparer.Equals(x, y))
                    return false;
            return true;
        }
    }

    public class StringComparer : IEqualityComparer<string>
    {
        bool IEqualityComparer<string>.Equals(string x, string y) => string.Equals(x, y);

        int IEqualityComparer<string>.GetHashCode(string obj) => obj.GetHashCode();

        public static readonly StringComparer Instance = new StringComparer();

        public static bool Equals(string x, string y) => string.Equals(x, y);
    }

    public class EvalDeltaActionComparer : IEqualityComparer<EvalDeltaAction>
    {
        bool IEqualityComparer<EvalDeltaAction>.Equals(EvalDeltaAction x, EvalDeltaAction y)
        {
            return Equals(x, y);
        }

        int IEqualityComparer<EvalDeltaAction>.GetHashCode(EvalDeltaAction obj)
        {
            return obj.GetHashCode();
        }

        public static readonly EvalDeltaActionComparer Instance = new EvalDeltaActionComparer();

        public static bool Equals(EvalDeltaAction x, EvalDeltaAction y) => x == y;
    }
}
