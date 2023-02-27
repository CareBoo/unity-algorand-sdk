using System;
using System.Collections;
using System.Collections.Generic;

namespace Algorand.Unity
{
    [Flags]
    public enum TxnFlags : ushort
    {
        None = 0,
        Txn0 = 1 << 0,
        Txn1 = 1 << 1,
        Txn2 = 1 << 2,
        Txn3 = 1 << 3,
        Txn4 = 1 << 4,
        Txn5 = 1 << 5,
        Txn6 = 1 << 6,
        Txn7 = 1 << 7,
        Txn8 = 1 << 8,
        Txn9 = 1 << 9,
        Txn10 = 1 << 10,
        Txn11 = 1 << 11,
        Txn12 = 1 << 12,
        Txn13 = 1 << 13,
        Txn14 = 1 << 14,
        Txn15 = 1 << 15,
        All = (1 << 16) - 1
    }

    public partial struct TxnIndices
        : IEnumerable<int>
    {
        public const int MinIndex = 0;

        public const int MaxIndex = 15;

        private TxnFlags indices;

        public int Count()
        {
            var count = 0;
            var indexEnum = GetEnumerator();
            while (indexEnum.MoveNext())
                count++;
            return count;
        }

        public bool ContainsIndex(int index)
        {
            var flag = (TxnFlags)(1 << index);
            return (flag & indices) > 0;
        }

        public static implicit operator TxnIndices(TxnFlags flags)
        {
            return new TxnIndices { indices = flags };
        }

        public static implicit operator TxnFlags(TxnIndices txnIndices)
        {
            return txnIndices.indices;
        }

        public static explicit operator int(TxnIndices txnIndices)
        {
            return (int)txnIndices.indices;
        }

        public static TxnIndices operator &(TxnIndices x, TxnIndices y)
        {
            return x.indices & y.indices;
        }

        public static TxnIndices operator |(TxnIndices x, TxnIndices y)
        {
            return x.indices | y.indices;
        }

        public static TxnIndices operator ^(TxnIndices x, TxnIndices y)
        {
            return x.indices ^ y.indices;
        }

        public Enumerator GetEnumerator()
        {
            return new Enumerator(indices);
        }

        IEnumerator<int> IEnumerable<int>.GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public struct Enumerator : IEnumerator<int>
        {
            private TxnFlags indices;

            public Enumerator(TxnFlags indices)
            {
                this.indices = indices;
                Current = -1;
            }

            public int Current { get; private set; }

            object IEnumerator.Current => Current;

            public void Dispose()
            {
            }

            public bool MoveNext()
            {
                while (Current <= MaxIndex)
                {
                    Current++;
                    var intersection = (1 << Current) & (int)indices;
                    if (intersection != 0)
                        return true;
                }
                return false;
            }

            public void Reset()
            {
                Current = -1;
            }
        }
    }
}
