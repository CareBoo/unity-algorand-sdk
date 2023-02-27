using Unity.Collections;

namespace Algorand.Unity.Experimental.Abi
{
    /// <summary>
    /// Represents a value that can be encoded as an ABI argument when given
    /// an <see cref="AbiType"/> specification.
    /// </summary>
    public interface IAbiValue
    {
        /// <summary>
        /// Encode this value given a spec defined from an <see cref="AbiType"/>.
        /// </summary>
        /// <param name="type">The type of the argument that the ABI is expecting.</param>
        /// <param name="references">Stores account, application, and asset references that are intended to be included in <see cref="AppCallTxn.Accounts"/>, <see cref="AppCallTxn.ForeignApps"/>, and <see cref="AppCallTxn.ForeignAssets"/> arguments.</param>
        /// <param name="allocator">The memory allocator to use.</param>
        /// <returns>An <see cref="EncodedAbiArg"/>.</returns>
        EncodedAbiArg Encode(IAbiType type, AbiReferences references, Allocator allocator);

        /// <summary>
        /// Returns the length of the result of encoding this value as <see cref="AbiType"/>.
        /// </summary>
        /// <param name="type">The type to encode this value.</param>
        /// <returns>The length in bytes of the encoded value.</returns>
        int Length(IAbiType type);
    }
}
