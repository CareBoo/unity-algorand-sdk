using System;

namespace AlgoSdk
{
    /// <summary>
    /// Base unit of Algos
    /// </summary>
    [Serializable]
    [AlgoApiFormatter(typeof(WrappedValueFormatter<MicroAlgos, ulong>))]
    public partial struct MicroAlgos
        : IEquatable<MicroAlgos>
        , IEquatable<ulong>
        , IWrappedValue<ulong>
    {
        /// <summary>
        /// Amount of micro algos per algo.
        /// </summary>
        public const ulong PerAlgo = 1_000_000;

        public ulong Amount;

        public MicroAlgos(ulong amount)
        {
            Amount = amount;
        }

        ulong IWrappedValue<ulong>.WrappedValue { get => Amount; set => Amount = value; }

        public bool Equals(MicroAlgos other)
        {
            return this == other;
        }

        public bool Equals(ulong other)
        {
            return this == other;
        }

        public double ToAlgos() => (double)Amount / PerAlgo;

        public static implicit operator MicroAlgos(ulong amount) => new MicroAlgos(amount);

        public static implicit operator ulong(MicroAlgos microAlgos) => microAlgos.Amount;
    }
}
