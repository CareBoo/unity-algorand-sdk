using System;

namespace Algorand.Unity
{
    /// <summary>
    /// Identifier of an Asset
    /// </summary>
    [Serializable]
    [AlgoApiFormatter(typeof(WrappedValueFormatter<AssetIndex, ulong>))]
    public partial struct AssetIndex
        : IEquatable<AssetIndex>
        , IEquatable<ulong>
        , IWrappedValue<ulong>
    {
        public ulong Index;

        public AssetIndex(ulong index)
        {
            Index = index;
        }

        ulong IWrappedValue<ulong>.WrappedValue { get => Index; set => Index = value; }

        public bool Equals(AssetIndex other)
        {
            return this == other;
        }

        public bool Equals(ulong other)
        {
            return this == other;
        }

        public static implicit operator AssetIndex(ulong index) => new AssetIndex(index);

        public static implicit operator ulong(AssetIndex assetIndex) => assetIndex.Index;
    }
}
