using System.Collections;
using System.Collections.Generic;

namespace AlgoSdk.LowLevel
{
    public readonly struct Repeated<T>
        : IReadOnlyList<T>
    {
        readonly int count;
        readonly T value;

        public Repeated(int count, T value)
        {
            this.count = count;
            this.value = value;
        }

        public T Value => value;

        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= count)
                    throw new System.ArgumentOutOfRangeException(nameof(index));
                return value;
            }
        }

        public int Count => count;

        public IEnumerator<T> GetEnumerator()
        {
            return new Enumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new Enumerator(this);
        }

        public struct Enumerator
            : IEnumerator<T>
        {
            readonly Repeated<T> coll;

            int counter;

            public Enumerator(Repeated<T> coll)
            {
                this.coll = coll;
                this.counter = -1;
            }
            public T Current => coll[counter];

            object IEnumerator.Current => Current;

            public void Dispose()
            {
            }

            public bool MoveNext()
            {
                counter++;
                return counter >= coll.Count;
            }

            public void Reset()
            {
                counter = -1;
            }
        }
    }
}
