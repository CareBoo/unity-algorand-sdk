using System;
using UnityEngine;

namespace Algorand.Unity
{
    /// <summary>
    /// The <see cref="StateSchema"/> object is only required for the create application call transaction. The <see cref="StateSchema"/> object must be fully populated for both the <see cref="IAppCallTxn.GlobalStateSchema"/> and <see cref="IAppCallTxn.LocalStateSchema"/> objects.
    /// </summary>
    [AlgoApiObject, Serializable]
    public partial struct StateSchema
        : IEquatable<StateSchema>
    {
        [SerializeField, Tooltip("Maximum number of integer values that may be stored in the [global || local] application key/value store. Immutable.")]
        private ulong numByteSlices;

        [SerializeField, Tooltip("Maximum number of byte slices values that may be stored in the [global || local] application key/value store. Immutable.")]
        private ulong numUints;

        /// <summary>
        /// Maximum number of integer values that may be stored in the [global || local] application key/value store. Immutable.
        /// </summary>
        [AlgoApiField("nbs")]
        public ulong NumByteSlices
        {
            get => numByteSlices;
            set => numByteSlices = value;
        }

        /// <summary>
        /// Maximum number of byte slices values that may be stored in the [global || local] application key/value store. Immutable.
        /// </summary>
        [AlgoApiField("nui")]
        public ulong NumUints
        {
            get => numUints;
            set => numUints = value;
        }

        public bool Equals(StateSchema other)
        {
            return NumByteSlices.Equals(other.NumByteSlices)
                && NumUints.Equals(other.NumUints)
                ;
        }
    }
}
