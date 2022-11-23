using System;

namespace Algorand.Unity
{
    [Serializable, AlgoApiFormatter(typeof(StateDeltaFormatter))]
    public partial struct StateDelta
        : IEquatable<StateDelta>
        , IEquatable<ValueDeltaKeyValue[]>
        , IWrappedValue<ValueDeltaKeyValue[]>
    {
        public ValueDeltaKeyValue[] Map;

        ValueDeltaKeyValue[] IWrappedValue<ValueDeltaKeyValue[]>.WrappedValue { get => Map; set => Map = value; }

        public StateDelta(ValueDeltaKeyValue[] map)
        {
            this.Map = map;
        }

        public bool Equals(StateDelta other)
        {
            return ArrayComparer.Equals(Map, other.Map);
        }

        public bool Equals(ValueDeltaKeyValue[] other)
        {
            return ArrayComparer.Equals(Map, other);
        }

        public static implicit operator ValueDeltaKeyValue[](StateDelta sd) => sd.Map;

        public static implicit operator StateDelta(ValueDeltaKeyValue[] map) =>
            new StateDelta(map);
    }
}
