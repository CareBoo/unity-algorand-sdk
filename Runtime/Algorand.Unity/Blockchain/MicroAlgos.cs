using System;
using UnityEngine;

namespace Algorand.Unity
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

        [SerializeField] private ulong amount;

        public ulong Amount
        {
            get => amount;
            set => amount = value;
        }

        public MicroAlgos(ulong amount)
        {
            this.amount = amount;
        }

        ulong IWrappedValue<ulong>.WrappedValue { get => amount; set => amount = value; }

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
