using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AlgoSdk.LowLevel
{
    public struct ListEnumerator<T, TList>
        : IEnumerator<T>
        where TList : IReadOnlyList<T>
    {
        readonly TList list;

        int current;

        public ListEnumerator(TList list)
        {
            this.list = list;
            this.current = -1;
        }

        public T Current => list[current];

        object IEnumerator.Current => Current;

        public void Dispose()
        {
        }

        public bool MoveNext()
        {
            current++;
            return current < list.Count;
        }

        public void Reset()
        {
            current = -1;
        }
    }
}
