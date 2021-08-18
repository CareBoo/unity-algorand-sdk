using System.Collections.Generic;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace AlgoSdk.MsgPack
{
    public struct NativeListComparer<T> : IEqualityComparer<NativeList<T>>
        where T : unmanaged
    {
        public unsafe bool Equals(NativeList<T> x, NativeList<T> y)
        {
            return x.IsCreated == y.IsCreated
                && (!x.IsCreated || (x.Length == y.Length && x.GetUnsafePtr() == y.GetUnsafePtr()));
        }

        public int GetHashCode(NativeList<T> obj)
        {
            return obj.GetHashCode();
        }
    }

    public struct TransactionTypeComparer : IEqualityComparer<TransactionType>
    {
        public bool Equals(TransactionType x, TransactionType y)
        {
            return x == y;
        }

        public int GetHashCode(TransactionType obj)
        {
            return obj.GetHashCode();
        }
    }

    public struct SignatureTypeComparer : IEqualityComparer<SignatureType>
    {
        public bool Equals(SignatureType x, SignatureType y)
        {
            return x == y;
        }

        public int GetHashCode(SignatureType obj)
        {
            return obj.GetHashCode();
        }
    }

    public struct NativeTextComparer : IEqualityComparer<NativeText>
    {
        public bool Equals(NativeText x, NativeText y)
        {
            return x.IsCreated == y.IsCreated
                && (!x.IsCreated || x.Equals(y));
        }

        public int GetHashCode(NativeText obj)
        {
            return obj.GetHashCode();
        }
    }

    public struct NativeReferenceComparer<T> : IEqualityComparer<NativeReference<T>>
        where T : unmanaged
    {
        public bool Equals(NativeReference<T> x, NativeReference<T> y)
        {
            return x.IsCreated == y.IsCreated
                && (!x.IsCreated || x.Equals(y));
        }

        public int GetHashCode(NativeReference<T> obj)
        {
            return obj.GetHashCode();
        }
    }

    public struct NativeArrayComparer<T> : IEqualityComparer<NativeArray<T>>
        where T : unmanaged
    {
        public bool Equals(NativeArray<T> x, NativeArray<T> y)
        {
            return x.IsCreated == y.IsCreated
                && (!x.IsCreated || x.Equals(y));
        }

        public int GetHashCode(NativeArray<T> obj)
        {
            return obj.GetHashCode();
        }
    }
}
