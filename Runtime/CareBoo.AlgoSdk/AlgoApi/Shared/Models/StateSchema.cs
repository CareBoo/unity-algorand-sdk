using System;
using UnityEngine;

namespace AlgoSdk
{
    /// <summary>
    /// The <see cref="StateSchema"/> object is only required for the create application call transaction. The <see cref="StateSchema"/> object must be fully populated for both the <see cref="IAppCallTxn.GlobalStateSchema"/> and <see cref="IAppCallTxn.LocalStateSchema"/> objects.
    /// </summary>
    [AlgoApiObject]
    [Serializable]
    public partial struct StateSchema
        : IEquatable<StateSchema>
    {
        /// <summary>
        /// Maximum number of integer values that may be stored in the [global || local] application key/value store. Immutable.
        /// </summary>
        [AlgoApiField("num-byte-slice", "nbs")]
        [Tooltip("Maximum number of integer values that may be stored in the [global || local] application key/value store. Immutable.")]
        public ulong NumByteSlices;

        /// <summary>
        /// Maximum number of byte slices values that may be stored in the [global || local] application key/value store. Immutable.
        /// </summary>
        [AlgoApiField("num-uint", "nui")]
        [Tooltip("Maximum number of byte slices values that may be stored in the [global || local] application key/value store. Immutable.")]
        public ulong NumUints;

        public bool Equals(StateSchema other)
        {
            return NumByteSlices.Equals(other.NumByteSlices)
                && NumUints.Equals(other.NumUints)
                ;
        }
    }
}
