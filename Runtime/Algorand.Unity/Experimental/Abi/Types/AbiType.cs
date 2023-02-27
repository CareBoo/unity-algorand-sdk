using System.Collections.Generic;
using System.Text.RegularExpressions;
using Algorand.Unity.Json;
using Algorand.Unity.MessagePack;

namespace Algorand.Unity.Experimental.Abi
{
    public partial interface IAbiType
    {
        /// <summary>
        /// Name of the ABI Type. See <see href="https://github.com/algorandfoundation/ARCs/blob/main/ARCs/arc-0004.md#types">this list of possible ABI types</see>.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// The type of the value this ABI will encode. Used to determine how this type is represented.
        /// </summary>
        AbiValueType ValueType { get; }

        /// <summary>
        /// Is the type static or dynamic length?
        /// </summary>
        bool IsStatic { get; }

        /// <summary>
        /// The length of this type in bytes if it's static, otherwise undefined.
        /// </summary>
        int StaticLength { get; }

        /// <summary>
        /// Represents the "N" in "uint{N}", "ufixed{N}x{M}", or "{type}[N]"
        /// </summary>
        int N { get; }

        /// <summary>
        /// Represents the "M" in "ufixed{N}x{M}"
        /// </summary>
        int M { get; }

        /// <summary>
        /// Represents the Element type of an array, or the list of types in a tuple.
        /// </summary>
        IAbiType[] NestedTypes { get; }

        /// <summary>
        /// The element type if this is an array, undefined otherwise.
        /// </summary>
        IAbiType ElementType { get; }

        /// <summary>
        /// The length of the array if this is fixed, undefined otherwise.
        /// </summary>
        int ArrayLength { get; }

        /// <summary>
        /// <c>true</c> if this is a fixed array (has a fixed number of elements).
        /// </summary>
        bool IsFixedArray { get; }

        /// <summary>
        /// If <see cref="IsTxn"/>, then this represents the type of the transaction.
        /// </summary>
        AbiTransactionType TransactionType { get; }

        /// <summary>
        /// If <see cref="IsReference"/>, then this represents the type of the reference.
        /// </summary>
        AbiReferenceType ReferenceType { get; }

        /// <summary>
        /// Decodes encoded ABI bytes into an <see cref="IAbiValue"/>.
        /// </summary>
        /// <param name="bytes">encoded abi bytes</param>
        /// <returns>An untyped <see cref="IAbiValue"/> that can be cast to the correct type by the caller.</returns>
        (string decodeError, IAbiValue abiValue) Decode(byte[] bytes);
    }

    public static class AbiTypeExtensions
    {
        /// <summary>
        /// Returns <c>true</c> if this is a transaction type.
        /// </summary>
        public static bool IsTransaction<T>(this T type)
            where T : IAbiType
        {
            return type.TransactionType > 0;
        }

        /// <summary>
        /// Returns <c>true</c> if this is a reference type.
        /// </summary>
        public static bool IsReference<T>(this T type)
            where T : IAbiType
        {
            return type.ReferenceType > 0;
        }

        public static string CheckDecodeLength<T>(this T type, byte[] bytes)
            where T : IAbiType
        {
            if (bytes == null)
            {
                return $"Cannot decode null to {type.Name}";
            }
            else if (type.IsStatic && bytes.Length != type.StaticLength)
            {
                return $"Cannot decode {bytes.Length} bytes to {type.Name} type";
            }
            return null;
        }
    }

    public static class AbiType
    {
        /// <summary>
        /// Represents a "byte".
        /// </summary>
        public static ByteType Byte = new ByteType();

        /// <summary>
        /// Represents an "address".
        /// </summary>
        public static AddressType Address = new AddressType();

        /// <summary>
        /// Represents a "string".
        /// </summary>
        public static StringType String = new StringType();

        /// <summary>
        /// Represents an "account".
        /// </summary>
        public static ReferenceType AccountReference => new ReferenceType(AbiReferenceType.Account);

        /// <summary>
        /// Represents an "asset".
        /// </summary>
        public static ReferenceType AssetReference => new ReferenceType(AbiReferenceType.Asset);

        /// <summary>
        /// Represents an "application".
        /// </summary>
        public static ReferenceType ApplicationReference => new ReferenceType(AbiReferenceType.Application);

        /// <summary>
        /// Represents a "bool".
        /// </summary>
        public static BoolType Bool = new BoolType();

        /// <summary>
        /// Represents a Transaction type.
        /// </summary>
        /// <param name="txnType">The type of the transaction.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">Thrown when the given transaction type is <see cref="AbiTransactionType.None"/>.</exception>
        public static TransactionReferenceType Transaction(AbiTransactionType txnType) => new TransactionReferenceType(txnType);

        /// <summary>
        /// Represents an "uint{N}".
        /// </summary>
        /// <param name="n">The number of bits in this uint.</param>
        /// <exception cref="System.ArgumentException">Thrown when <paramref name="n"/> is not a multiple of 8, or in the range [8, 512].</exception>
        public static UIntType UIntN(int n) => new UIntType(n);

        /// <summary>
        /// Represents an "ufixed{N}x{M}".
        /// </summary>
        /// <param name="n">The number of bits in this ufixed.</param>
        /// <param name="m">The precision of this ufixed (number of digits after the decimal).</param>
        /// <exception cref="System.ArgumentException">Thrown when <paramref name="m"/> is not in the range (0, 160].</exception>
        public static UFixedType UFixedNxM(int n, int m) => new UFixedType(n, m);

        /// <summary>
        /// Represents a "{type}[]" (variable array).
        /// </summary>
        /// <param name="elementType">The type of the elements in this array.</param>
        public static VariableArrayType VariableArray(IAbiType elementType) => new VariableArrayType(elementType);

        /// <summary>
        /// Represents a "{type}[N]" (fixed array).
        /// </summary>
        /// <param name="elementType">The type of the elements in this array.</param>
        /// <param name="length">The length of this array. Must be larger than 0.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">thrown when <paramref name="length"/> is negative.</exception>
        public static FixedArrayType FixedArray(IAbiType elementType, int length) => new FixedArrayType(elementType, length);

        /// <summary>
        /// Represents a "({T1},{T2},...,{TN})" (tuple).
        /// </summary>
        /// <param name="nestedTypes">The types that make up this tuple.</param>
        public static TupleType Tuple(IAbiType[] nestedTypes) => new TupleType(nestedTypes);

        /// <summary>
        /// Parse a type string into an <see cref="AbiType"/>.
        /// </summary>
        /// <param name="type">The typename to parse.</param>
        /// <exception cref="System.ArgumentException">Thrown when the given type cannot be parsed.</exception>
        public static IAbiType Parse(string type)
        {
            if (!TryParse(type, out IAbiType abiType))
                throw new System.ArgumentException($"Could not parse {type}", nameof(type));
            return abiType;
        }

        /// <summary>
        /// Try parsing a type string into an <see cref="AbiType"/>.
        /// </summary>
        /// <param name="type">The typename to parse.</param>
        /// <param name="abiType">The returned <see cref="AbiType"/> if the typename was parsed.</param>
        /// <returns><c>true</c> if the typename could be parsed, <c>false</c> otherwise.</returns>
        public static bool TryParse(string type, out IAbiType abiType)
        {
            switch (type)
            {
                case "byte":
                    abiType = AbiType.Byte;
                    return true;
                case "address":
                    abiType = AbiType.Address;
                    return true;
                case "string":
                    abiType = AbiType.String;
                    return true;
                case "account":
                    abiType = new ReferenceType(AbiReferenceType.Account);
                    return true;
                case "asset":
                    abiType = new ReferenceType(AbiReferenceType.Asset);
                    return true;
                case "application":
                    abiType = new ReferenceType(AbiReferenceType.Application);
                    return true;
                case "bool":
                    abiType = AbiType.Bool;
                    return true;
            }

            var txnType = AbiTransactionTypeExtensions.Parse(type);
            if (txnType != AbiTransactionType.None)
            {
                abiType = new TransactionReferenceType(txnType);
                return true;
            }

            var match = Patterns.UIntN.Match(type);
            if (match.Success)
                return TryParseUIntN(match, out abiType);
            match = Patterns.UFixedNxM.Match(type);
            if (match.Success)
                return TryParseUFixedNxM(match, out abiType);
            match = Patterns.Array.Match(type);
            if (match.Success)
                return TryParseArray(match, out abiType);
            match = Patterns.Tuple.Match(type);
            if (match.Success)
                return TryParseTuple(match, out abiType);

            abiType = default;
            return false;
        }

        private static bool TryParseUIntN(Match match, out IAbiType abiType)
        {
            var nGroup = match.Groups["N"];
            if (!nGroup.Success
                || !int.TryParse(nGroup.Value, out var n))
            {
                abiType = default;
                return false;
            }
            abiType = new UIntType(n);
            return true;
        }

        private static bool TryParseUFixedNxM(Match match, out IAbiType abiType)
        {
            var nGroup = match.Groups["N"];
            var mGroup = match.Groups["M"];
            if (!nGroup.Success || !mGroup.Success
                || !int.TryParse(nGroup.Value, out var n) || !int.TryParse(mGroup.Value, out var m)
                || n % 8 > 0 || n < 8 || n > 512
                || m < 0 || m > 160)
            {
                abiType = default;
                return false;
            }
            abiType = new UFixedType(n, m);
            return true;
        }

        private static bool TryParseArray(Match match, out IAbiType abiType)
        {
            var elementGroup = match.Groups["Element"];
            var lengthGroup = match.Groups["Length"];
            if (!elementGroup.Success || !lengthGroup.Success
                || !TryParse(elementGroup.Value, out var elementAbiType))
            {
                abiType = default;
                return false;
            }
            int length = -1;
            if (!string.IsNullOrEmpty(lengthGroup.Value)
                && !int.TryParse(lengthGroup.Value, out length))
            {
                abiType = default;
                return false;
            }
            abiType = length < 0
                ? (IAbiType)VariableArray(elementAbiType)
                : (IAbiType)FixedArray(elementAbiType, length)
                ;
            return true;
        }

        private static bool TryParseTuple(Match match, out IAbiType abiType)
        {
            var typeStr = match.Value.TrimStart('(').TrimEnd(')');
            var nestedTypes = new List<IAbiType>();
            var start = 0;
            var i = 0;
            var tupleDepth = 0;
            string childType;
            IAbiType childAbiType;
            foreach (var c in typeStr)
            {
                if (c == '(')
                    tupleDepth++;
                else if (c == ')')
                    tupleDepth--;
                else if (c == ',' && tupleDepth == 0)
                {
                    childType = typeStr.Substring(start, i - start);
                    if (!TryParse(childType, out childAbiType))
                    {
                        abiType = default;
                        return false;
                    }
                    start = i + 1;
                    nestedTypes.Add(childAbiType);
                }
                i++;
            }
            childType = typeStr.Substring(start, typeStr.Length - start);
            if (!TryParse(childType, out childAbiType))
            {
                abiType = default;
                return false;
            }
            nestedTypes.Add(childAbiType);


            abiType = Tuple(nestedTypes.ToArray());
            return true;
        }

        /// <summary>
        /// Regex patterns used for parsing type names.
        /// </summary>
        public static class Patterns
        {
            public const string AnyType = @"[a-z0-9,\[\]()]+";
            public static readonly Regex UIntN = new Regex("^(?<" + nameof(UIntN) + ">" + @"uint(?<N>\d{1,3})" + ")$");
            public static readonly Regex UFixedNxM = new Regex("^(?<" + nameof(UFixedNxM) + ">" + @"ufixed(?<N>\d{1,3})x(?<M>\d{1,3})" + ")$");
            public static readonly Regex Array = new Regex("^(?<" + nameof(Array) + ">" + @"(?<Element>" + AnyType + @")\[(?<Length>\d*)\]" + ")$");
            public static readonly Regex Tuple = new Regex("^(?<" + nameof(Tuple) + ">" + @"\(" + AnyType + @"\)" + ")$");
        }

        public struct Formatter : IAlgoApiFormatter<IAbiType>
        {
            public IAbiType Deserialize(ref JsonReader reader)
            {
                return AbiType.TryParse(AlgoApiFormatterCache<string>.Formatter.Deserialize(ref reader), out var abiType)
                    ? abiType
                    : default
                    ;
            }

            public IAbiType Deserialize(ref MessagePackReader reader)
            {
                return AbiType.TryParse(AlgoApiFormatterCache<string>.Formatter.Deserialize(ref reader), out var abiType)
                    ? abiType
                    : default
                    ;
            }

            public void Serialize(ref JsonWriter writer, IAbiType value)
            {
                AlgoApiFormatterCache<string>.Formatter.Serialize(ref writer, value.Name);
            }

            public void Serialize(ref MessagePackWriter writer, IAbiType value)
            {
                AlgoApiFormatterCache<string>.Formatter.Serialize(ref writer, value.Name);
            }
        }
    }
}
