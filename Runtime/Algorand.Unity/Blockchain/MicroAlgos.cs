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

        /// <summary>
        /// The numeric amount of MicroAlgos.
        /// </summary>
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

        /// <summary>
        /// Converts these MicroAlgos to units of Algos.
        /// </summary>
        /// <returns></returns>
        public double ToAlgos() => (double)Amount / PerAlgo;

        /// <summary>
        /// Converts the given amount of algos into microalgos.
        /// </summary>
        /// <param name="algos">The amount of algos</param>
        /// <returns>MicroAlgo amount of algos</returns>
        public static MicroAlgos FromAlgos(double algos)
        {
            return (ulong)(algos * PerAlgo);
        }

        public static implicit operator MicroAlgos(ulong amount)
        {
            MicroAlgos result;
            result.amount = amount;
            return result;
        }

        public static implicit operator ulong(MicroAlgos microAlgos) => microAlgos.Amount;
    }
}
