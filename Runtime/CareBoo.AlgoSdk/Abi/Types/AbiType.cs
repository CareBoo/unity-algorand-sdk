using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using AlgoSdk.Json;
using AlgoSdk.MessagePack;
using UnityEngine;

namespace AlgoSdk.Abi
{
    /// <summary>
    /// The type of an ABI value.
    /// </summary>
    [Serializable]
    [AlgoApiFormatter(typeof(AbiType.Formatter))]
    public partial struct AbiType
        : IEquatable<AbiType>
    {
        [SerializeField]
        string name;

        [SerializeField]
        AbiValueType valueType;

        [SerializeField]
        bool isStatic;

        [SerializeField]
        int staticLength;

        [SerializeField]
        int n;

        [SerializeField]
        int m;

        [SerializeField]
        AbiType[] nestedTypes;

        [SerializeField]
        AbiTransactionType txnType;

        [SerializeField]
        AbiReferenceType referenceType;

        /// <summary>
        /// Name of the ABI Type. See <see href="https://github.com/algorandfoundation/ARCs/blob/main/ARCs/arc-0004.md#types">this list of possible ABI types</see>.
        /// </summary>
        public string Name => name;

        /// <summary>
        /// The type of the value this ABI will encode. Used to determine how this type is represented.
        /// </summary>
        public AbiValueType ValueType => valueType;

        /// <summary>
        /// Is the type static or dynamic length?
        /// </summary>
        public bool IsStatic => isStatic;

        /// <summary>
        /// The length of this type in bytes if it's static, otherwise undefined.
        /// </summary>
        public int StaticLength => staticLength;

        /// <summary>
        /// Represents the "N" in "uint{N}", "ufixed{N}x{M}", or "{type}[N]"
        /// </summary>
        public int N => n;

        /// <summary>
        /// Represents the "M" in "ufixed{N}x{M}"
        /// </summary>
        public int M => m;

        /// <summary>
        /// Represents the Element type of an array, or the list of types in a tuple.
        /// </summary>
        public AbiType[] NestedTypes => nestedTypes;

        /// <summary>
        /// The element type if this is an array, undefined otherwise.
        /// </summary>
        public AbiType ElementType => nestedTypes[0];

        /// <summary>
        /// The length of the array if this is fixed, undefined otherwise.
        /// </summary>
        public int ArrayLength => n;

        /// <summary>
        /// <c>true</c> if this is a fixed array (has a fixed number of elements).
        /// </summary>
        public bool IsFixedArray => ArrayLength >= 0;

        /// <summary>
        /// <c>true</c> if this represents a reference to an account, application, or asset in other parts of the transaction.
        /// </summary>
        public bool IsReference => referenceType > 0;

        /// <summary>
        /// <c>true</c> if this type represents a reference to a transaction that is previously set in the Atomic Transaction Group.
        /// </summary>
        public bool IsTxn => txnType > 0;

        /// <summary>
        /// If <see cref="IsTxn"/>, then this represents the type of the transaction.
        /// </summary>
        public AbiTransactionType TransactionType => txnType;

        /// <summary>
        /// If <see cref="IsReference"/>, then this represents the type of the reference.
        /// </summary>
        public AbiReferenceType ReferenceType => referenceType;

        public override string ToString()
        {
            return Name;
        }

        public bool Equals(AbiType other)
        {
            return string.Equals(name, other.name);
        }

        /// <summary>
        /// Represents a "byte".
        /// </summary>
        public static AbiType Byte => new AbiType
        {
            name = "byte",
            valueType = AbiValueType.UIntN,
            isStatic = true,
            staticLength = 1,
            n = 8
        };

        /// <summary>
        /// Represents an "address".
        /// </summary>
        public static AbiType Address => new AbiType
        {
            name = "address",
            valueType = AbiValueType.Array,
            isStatic = true,
            staticLength = 4,
            n = 32,
            nestedTypes = new[] { Byte }
        };

        /// <summary>
        /// Represents a "string".
        /// </summary>
        public static AbiType String => new AbiType
        {
            name = "string",
            valueType = AbiValueType.Array,
            isStatic = false,
            nestedTypes = new[] { Byte }
        };

        /// <summary>
        /// Represents an "account".
        /// </summary>
        public static AbiType AccountReference => new AbiType
        {
            name = "account",
            valueType = AbiValueType.UIntN,
            isStatic = true,
            staticLength = 1,
            n = 8,
            referenceType = AbiReferenceType.Account
        };

        /// <summary>
        /// Represents an "asset".
        /// </summary>
        public static AbiType AssetReference => new AbiType
        {
            name = "asset",
            valueType = AbiValueType.UIntN,
            isStatic = true,
            staticLength = 1,
            n = 8,
            referenceType = AbiReferenceType.Asset
        };

        /// <summary>
        /// Represents an "application".
        /// </summary>
        public static AbiType ApplicationReference => new AbiType
        {
            name = "application",
            valueType = AbiValueType.UIntN,
            isStatic = true,
            staticLength = 1,
            n = 8,
            referenceType = AbiReferenceType.Application
        };

        /// <summary>
        /// Represents a "bool".
        /// </summary>
        public static AbiType Boolean => new AbiType
        {
            name = "bool",
            valueType = AbiValueType.Boolean,
            isStatic = true,
            staticLength = 1,
        };

        /// <summary>
        /// Represents a Transaction type.
        /// </summary>
        /// <param name="txnType">The type of the transaction.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">Thrown when the given transaction type is <see cref="AbiTransactionType.None"/>.</exception>
        public static AbiType Transaction(AbiTransactionType txnType)
        {
            if (txnType == AbiTransactionType.None)
                throw new System.ArgumentOutOfRangeException(nameof(txnType));

            return new AbiType
            {
                name = txnType.ToString(),
                txnType = txnType
            };
        }

        /// <summary>
        /// Represents an "uint{N}".
        /// </summary>
        /// <param name="n">The number of bits in this uint.</param>
        /// <exception cref="System.ArgumentException">Thrown when <paramref name="n"/> is not a multiple of 8, or in the range [8, 512].</exception>
        public static AbiType UIntN(int n)
        {
            if (n % 8 > 0)
                throw new System.ArgumentException($"{n} is not a multiple of 8", nameof(n));
            if (n < 8 || n > 512)
                throw new System.ArgumentException($"{n} is not in the range [8, 512]", nameof(n));
            return new AbiType
            {
                name = $"uint{n}",
                valueType = AbiValueType.UIntN,
                isStatic = true,
                staticLength = n / 8,
                n = n
            };
        }

        /// <summary>
        /// Represents an "ufixed{N}x{M}".
        /// </summary>
        /// <param name="n">The number of bits in this ufixed.</param>
        /// <param name="m">The precision of this ufixed (number of digits after the decimal).</param>
        /// <exception cref="System.ArgumentException">Thrown when <paramref name="m"/> is not in the range (0, 160].</exception>
        public static AbiType UFixedNxM(int n, int m)
        {
            if (m <= 0 || m > 160)
                throw new System.ArgumentException($"{m} is not in the range (0, 160]", nameof(m));
            return new AbiType
            {
                name = $"ufixed{n}x{m}",
                valueType = AbiValueType.UFixedNxM,
                isStatic = true,
                staticLength = n / 8,
                n = n,
                m = m
            };
        }

        /// <summary>
        /// Represents a "{type}[]" (variable array).
        /// </summary>
        /// <param name="elementType">The type of the elements in this array.</param>
        public static AbiType VariableArray(AbiType elementType)
        {
            return new AbiType
            {
                name = $"{elementType.Name}[]",
                valueType = AbiValueType.Array,
                isStatic = false,
                nestedTypes = new[] { elementType },
                n = -1
            };
        }

        /// <summary>
        /// Represents a "{type}[N]" (fixed array).
        /// </summary>
        /// <param name="elementType">The type of the elements in this array.</param>
        /// <param name="length">The length of this array. Must be larger than 0.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">thrown when <paramref name="length"/> is negative.</exception>
        public static AbiType FixedArray(AbiType elementType, int length)
        {
            if (length < 0)
                throw new System.ArgumentOutOfRangeException(nameof(length));
            var isStatic = elementType.IsStatic;
            var staticLength = elementType.ValueType == AbiValueType.Boolean
                ? (length + 7) / 8
                : length
                ;
            return new AbiType
            {
                name = $"{elementType.Name}[{length}]",
                valueType = AbiValueType.Array,
                isStatic = isStatic,
                staticLength = staticLength,
                nestedTypes = new[] { elementType },
                n = length
            };
        }

        /// <summary>
        /// Represents a "({T1},{T2},...,{TN})" (tuple).
        /// </summary>
        /// <param name="nestedTypes">The types that make up this tuple.</param>
        public static AbiType Tuple(AbiType[] nestedTypes)
        {
            var isStatic = true;
            var staticLength = 0;
            var boolCount = 0;

            for (var i = 0; i < nestedTypes.Length; i++)
            {
                var type = nestedTypes[i];
                if (!type.IsStatic)
                {
                    isStatic = false;
                    staticLength = 0;
                    break;
                }

                boolCount = type.ValueType == AbiValueType.Boolean
                    ? boolCount + 1
                    : 0
                    ;
                if (boolCount % 8 == 0)
                    staticLength += type.StaticLength;
            }

            return new AbiType
            {
                name = $"({string.Join(",", nestedTypes)})",
                valueType = AbiValueType.Tuple,
                isStatic = isStatic,
                staticLength = staticLength,
                nestedTypes = nestedTypes
            };
        }

        /// <summary>
        /// Parse a type string into an <see cref="AbiType"/>.
        /// </summary>
        /// <param name="type">The typename to parse.</param>
        /// <exception cref="System.ArgumentException">Thrown when the given type cannot be parsed.</exception>
        public static AbiType Parse(string type)
        {
            if (!TryParse(type, out AbiType abiType))
                throw new System.ArgumentException($"Could not parse {type}", nameof(type));
            return abiType;
        }

        /// <summary>
        /// Try parsing a type string into an <see cref="AbiType"/>.
        /// </summary>
        /// <param name="type">The typename to parse.</param>
        /// <param name="abiType">The returned <see cref="AbiType"/> if the typename was parsed.</param>
        /// <returns><c>true</c> if the typename could be parsed, <c>false</c> otherwise.</returns>
        public static bool TryParse(string type, out AbiType abiType)
        {
            switch (type)
            {
                case "byte":
                    abiType = Byte;
                    return true;
                case "address":
                    abiType = Address;
                    return true;
                case "string":
                    abiType = String;
                    return true;
                case "account":
                    abiType = AccountReference;
                    return true;
                case "asset":
                    abiType = AssetReference;
                    return true;
                case "application":
                    abiType = ApplicationReference;
                    return true;
                case "bool":
                    abiType = Boolean;
                    return true;
            }

            var txnType = AbiTransactionTypeExtensions.Parse(type);
            if (txnType != AbiTransactionType.None)
            {
                abiType = Transaction(txnType);
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

        static bool TryParseUIntN(Match match, out AbiType abiType)
        {
            var nGroup = match.Groups["N"];
            if (!nGroup.Success
                || !int.TryParse(nGroup.Value, out var n))
            {
                abiType = default;
                return false;
            }
            abiType = UIntN(n);
            return true;
        }

        static bool TryParseBoolean(Match match, out AbiType abiType)
        {
            abiType = Boolean;
            return true;
        }

        static bool TryParseUFixedNxM(Match match, out AbiType abiType)
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
            abiType = UFixedNxM(n, m);
            return true;
        }

        static bool TryParseArray(Match match, out AbiType abiType)
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
                ? VariableArray(elementAbiType)
                : FixedArray(elementAbiType, length)
                ;
            return true;
        }

        static bool TryParseTuple(Match match, out AbiType abiType)
        {
            var typeStr = match.Value.TrimStart('(').TrimEnd(')');
            var nestedTypes = new List<AbiType>();
            var start = 0;
            var i = 0;
            var tupleDepth = 0;
            string childType;
            AbiType childAbiType;
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

        public struct Formatter : IAlgoApiFormatter<AbiType>
        {
            public AbiType Deserialize(ref JsonReader reader)
            {
                return AbiType.TryParse(AlgoApiFormatterCache<string>.Formatter.Deserialize(ref reader), out var abiType)
                    ? abiType
                    : default
                    ;
            }

            public AbiType Deserialize(ref MessagePackReader reader)
            {
                return AbiType.TryParse(AlgoApiFormatterCache<string>.Formatter.Deserialize(ref reader), out var abiType)
                    ? abiType
                    : default
                    ;
            }

            public void Serialize(ref JsonWriter writer, AbiType value)
            {
                AlgoApiFormatterCache<string>.Formatter.Serialize(ref writer, value.Name);
            }

            public void Serialize(ref MessagePackWriter writer, AbiType value)
            {
                AlgoApiFormatterCache<string>.Formatter.Serialize(ref writer, value.Name);
            }
        }
    }
}
