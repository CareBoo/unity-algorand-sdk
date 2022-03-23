using System;

namespace AlgoSdk
{
    [AlgoApiFormatter(typeof(WrappedValueFormatter<AppStateDelta, EvalDeltaKeyValue[]>))]
    public partial struct AppStateDelta
        : IEquatable<AppStateDelta>
        , IEquatable<EvalDeltaKeyValue[]>
        , IWrappedValue<EvalDeltaKeyValue[]>
    {
        public EvalDeltaKeyValue[] Map;

        EvalDeltaKeyValue[] IWrappedValue<EvalDeltaKeyValue[]>.WrappedValue { get => Map; set => Map = value; }

        public AppStateDelta(EvalDeltaKeyValue[] map)
        {
            this.Map = map;
        }

        public bool Equals(AppStateDelta other)
        {
            return ArrayComparer.Equals(Map, other.Map);
        }

        public bool Equals(EvalDeltaKeyValue[] other)
        {
            return ArrayComparer.Equals(Map, other);
        }

        public static implicit operator EvalDeltaKeyValue[](AppStateDelta asd) => asd.Map;

        public static implicit operator AppStateDelta(EvalDeltaKeyValue[] map) =>
            new AppStateDelta(map);
    }
}
