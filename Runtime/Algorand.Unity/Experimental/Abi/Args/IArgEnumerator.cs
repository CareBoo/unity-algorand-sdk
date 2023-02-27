using Unity.Collections;

namespace Algorand.Unity.Experimental.Abi
{
    /// <summary>
    /// An ABI arg enumerator.
    /// </summary>
    /// <typeparam name="T">The type of the enumerator; used for going to the next/prev state of the enumerator.</typeparam>
    public interface IArgEnumerator<T>
        where T : IArgEnumerator<T>
    {
        /// <summary>
        /// Try setting current to the next argument in the enumerator.
        /// </summary>
        /// <param name="next">The enumerator with the next argument set as the current one.</param>
        /// <returns><c>true</c> if there is a next argument, else <c>false</c>.</returns>
        public bool TryNext(out T next);

        /// <summary>
        /// Try setting current to the previous argument in the enumerator.
        /// </summary>
        /// <param name="next">The enumerator with the previous argument set as the current one.</param>
        /// <returns><c>true</c> if there is a previous argument, else <c>false</c>.</returns>
        public bool TryPrev(out T prev);

        /// <summary>
        /// Encode the current argument given a spec defined from an <see cref="AbiType"/>.
        /// </summary>
        /// <param name="type">The type of the argument that the ABI is expecting.</param>
        /// <param name="references">Stores account, application, and asset references that are intended to be included in <see cref="AppCallTxn.Accounts"/>, <see cref="AppCallTxn.ForeignApps"/>, and <see cref="AppCallTxn.ForeignAssets"/> arguments.</param>
        /// <param name="allocator">The memory allocator to use.</param>
        /// <returns>An <see cref="EncodedAbiArg"/>.</returns>
        EncodedAbiArg EncodeCurrent(IAbiType type, AbiReferences references, Allocator allocator);

        /// <summary>
        /// Returns the length of the result of encoding the current argument as <see cref="AbiType"/>.
        /// </summary>
        /// <param name="type">The type to encode this value.</param>
        /// <returns>The length in bytes of the encoded value.</returns>
        int LengthOfCurrent(IAbiType type);

        /// <summary>
        /// Returns the number of args in this enumerator
        /// </summary>
        int Count { get; }
    }
}
