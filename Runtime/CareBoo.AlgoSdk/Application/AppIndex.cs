using System;

namespace AlgoSdk
{
    /// <summary>
    /// Identifier of an Application
    /// </summary>
    [Serializable]
    [AlgoApiFormatter(typeof(WrappedValueFormatter<AppIndex, ulong>))]
    public partial struct AppIndex
        : IEquatable<AppIndex>
        , IEquatable<ulong>
        , IWrappedValue<ulong>
    {
        public ulong Index;

        public AppIndex(ulong index)
        {
            Index = index;
        }

        ulong IWrappedValue<ulong>.WrappedValue { get => Index; set => Index = value; }

        public bool Equals(AppIndex other)
        {
            return this == other;
        }

        public bool Equals(ulong other)
        {
            return this == other;
        }

        public static implicit operator ulong(AppIndex appIndex) => appIndex.Index;

        public static implicit operator AppIndex(ulong index) => new AppIndex(index);
    }
}
