using System;
using UnityEngine;

namespace Algorand.Unity.Experimental.Abi
{
    /// <summary>
    /// Defines a logically grouped set of methods in a Smart Contract ABI.
    /// </summary>
    /// <remarks>
    /// See <see href="https://github.com/algorandfoundation/ARCs/blob/2723ca7f4568c0d19c412198651404cb0a0e9dbd/ARCs/arc-0004.md#interfaces">ARC-0004</see>
    /// for details.
    /// </remarks>
    [AlgoApiObject, Serializable]
    public partial struct Interface
        : IEquatable<Interface>
    {
        [SerializeField, Tooltip("A user-friendly name for the interface.")]
        private string name;

        [SerializeField, Tooltip("Optional, user-friendly description for the interface.")]
        private string description;

        [SerializeField, Tooltip("All of the methods that the interface contains.")]
        private Method[] methods;

        /// <summary>
        /// A user-friendly name for the interface.
        /// </summary>
        [AlgoApiField("name")]
        public string Name
        {
            get => name;
            set => name = value;
        }

        /// <summary>
        /// Optional, user-friendly description for the interface.
        /// </summary>
        [AlgoApiField("desc")]
        public string Description
        {
            get => name;
            set => name = value;
        }

        /// <summary>
        /// All of the methods that the interface contains.
        /// </summary>
        [AlgoApiField("methods")]
        public Method[] Methods
        {
            get => methods;
            set => methods = value;
        }

        public bool Equals(Interface other)
        {
            return StringComparer.Equals(Name, other.Name)
                && StringComparer.Equals(Description, other.Description)
                && ArrayComparer.Equals(Methods, other.Methods)
                ;
        }
    }
}
