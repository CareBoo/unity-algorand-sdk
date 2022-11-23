using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Algorand.Unity.LowLevel
{
    public struct ListEnumerator<T, TList>
        : IEnumerator<T>
        where TList : IReadOnlyList<T>
    {
        private readonly TList list;

        private int current;

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
