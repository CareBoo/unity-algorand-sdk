using UnityEngine;

namespace Algorand.Unity
{
    /// <summary>
    /// Utility attribute used to restrict an integer field by min, max, and mod (multiple of) values.
    /// Similar to the <see cref="RangeAttribute"/> but extended with mod.
    /// </summary>
    public class ModRangeAttribute : PropertyAttribute
    {
        private readonly int min;
        private readonly int max;
        private readonly int mod;

        public ModRangeAttribute(int min, int max, int mod)
        {
            this.min = min;
            this.max = max;
            this.mod = mod;
        }

        /// <summary>
        /// The min value this drawer allows.
        /// </summary>
        public int Min => min;

        /// <summary>
        /// The max value this drawer allows.
        /// </summary>
        public int Max => max;

        /// <summary>
        /// All values this drawer allows must be a multiple of this value.
        /// </summary>
        public int Mod => mod;
    }
}
