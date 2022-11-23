namespace Algorand.Unity.Experimental.Abi
{
    /// <summary>
    /// The untagged part of the tagged <see cref="AbiType"/> union.
    /// </summary>
    public enum AbiTypeCode
    {
        /// <summary>
        /// Default code
        /// </summary>
        None,

        /// <summary>
        /// A reference to an account, asset, or application represented as a uint8.
        /// </summary>
        Reference,

        /// <summary>
        /// A transaction type that represents a previous transaction in the group.
        /// </summary>
        Transaction,

        /// <summary>
        /// An account address
        /// </summary>
        Address,

        /// <summary>
        /// A boolean value, true or false
        /// </summary>
        Bool,

        /// <summary>
        /// Alias of uint8.
        /// </summary>
        Byte,

        /// <summary>
        /// Fixed sized array of any other type, T[N].
        /// </summary>
        FixedArray,

        /// <summary>
        /// Variable sized array of any other type, T[].
        /// </summary>
        VariableArray,

        /// <summary>
        /// An alias of byte[] (variable sized array of bytes).
        /// </summary>
        String,

        /// <summary>
        /// A group of ordered abi types, (T1, T2, T3, ..., TN)
        /// </summary>
        Tuple,

        /// <summary>
        /// A fixed-length, decimal precision number, ufixedNxM.
        /// </summary>
        UFixed,

        /// <summary>
        /// A fixed-length integer, uintN.
        /// </summary>
        UInt,

        /// <summary>
        /// Used to find the count of AbiTypes - 1 (because this enum's starting index is 1).
        /// </summary>
        AbiTypeCount
    }
}
