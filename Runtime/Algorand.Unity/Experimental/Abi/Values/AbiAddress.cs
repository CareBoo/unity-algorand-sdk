using Algorand.Unity.LowLevel;
using Unity.Collections;

namespace Algorand.Unity.Experimental.Abi
{
    /// <summary>
    /// Stores data that can be converted to an address or an account reference in an ABI method call.
    /// </summary>
    public readonly struct AbiAddress
        : IAbiValue
    {
        private readonly Address value;

        public AbiAddress(Address value)
        {
            this.value = value;
        }

        /// <inheritdoc />
        public EncodedAbiArg Encode(IAbiType type, AbiReferences references, Allocator allocator)
        {
            CheckType(type);
            EncodedAbiArg result;
            if (type.IsReference())
            {
                result = new EncodedAbiArg(1, allocator);
                result.Bytes.AddNoResize(references.Encode(value));
            }
            else
            {
                result = new EncodedAbiArg(32, allocator)
                {
                    Length = 32
                };
                value.CopyTo(ref result, 0);
            }
            return result;
        }

        /// <inheritdoc />
        public int Length(IAbiType type)
        {
            CheckType(type);
            return type.IsReference()
                ? 1
                : 32
                ;
        }

        public override string ToString()
        {
            return value.ToString();
        }

        private void CheckType(IAbiType type)
        {
            switch (type.ValueType)
            {
                case AbiValueType.Array when (
                    type.ArrayLength == 32
                    && type.ElementType.ValueType == AbiValueType.UIntN
                    && type.ElementType.N == 8
                    && !type.IsReference()
                    ):
                case AbiValueType.UIntN when (
                    type.N == 8
                    && type.ReferenceType == AbiReferenceType.Account
                    ):
                    break;
                default:
                    throw new System.NotSupportedException(
                        $"Cannot encode address to type {type.Name}"
                        );
            }
        }
    }

    public static partial class Args
    {
        public static SingleArg<AbiAddress> Add(Address x) => Add(new AbiAddress(x));

        public static ArgsList<AbiAddress, T> Add<T>(this T tail, Address head)
            where T : struct, IArgEnumerator<T>
        {
            return new ArgsList<AbiAddress, T>(new AbiAddress(head), tail);
        }
    }
}
