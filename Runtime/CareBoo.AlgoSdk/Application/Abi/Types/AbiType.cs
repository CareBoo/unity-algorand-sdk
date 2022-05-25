using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using AlgoSdk.Json;
using AlgoSdk.MessagePack;
using UnityEngine;

namespace AlgoSdk.Abi
{
    /// <summary>
    /// Represents the type of an ABI value.
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

        public string Name => name;

        public AbiValueType ValueType => valueType;

        public bool IsStatic => isStatic;

        public int StaticLength => staticLength;

        public int N => n;

        public int M => m;

        public AbiType[] NestedTypes => nestedTypes;

        public AbiType ElementType => nestedTypes[0];

        public int ArrayLength => n;

        public bool IsFixedArray => ArrayLength >= 0;

        public bool IsReference => name == "account"
            || name == "asset"
            || name == "application"
            ;

        public override string ToString()
        {
            return Name;
        }

        public bool Equals(AbiType other)
        {
            return string.Equals(name, other.name);
        }

        public static AbiType Byte => new AbiType
        {
            name = "byte",
            valueType = AbiValueType.UIntN,
            isStatic = true,
            staticLength = 1,
            n = 8
        };

        public static AbiType Address => new AbiType
        {
            name = "address",
            valueType = AbiValueType.Array,
            isStatic = true,
            staticLength = 4,
            n = 32,
            nestedTypes = new[] { Byte }
        };

        public static AbiType String => new AbiType
        {
            name = "string",
            valueType = AbiValueType.Array,
            isStatic = false,
            nestedTypes = new[] { Byte }
        };

        public static AbiType Account => new AbiType
        {
            name = "account",
            valueType = AbiValueType.UIntN,
            isStatic = true,
            staticLength = 1,
            n = 8
        };

        public static AbiType Asset => new AbiType
        {
            name = "asset",
            valueType = AbiValueType.UIntN,
            isStatic = true,
            staticLength = 1,
            n = 8
        };

        public static AbiType Application => new AbiType
        {
            name = "application",
            valueType = AbiValueType.UIntN,
            isStatic = true,
            staticLength = 1,
            n = 8
        };

        public static AbiType Boolean => new AbiType
        {
            name = "bool",
            valueType = AbiValueType.Boolean,
            isStatic = true,
            staticLength = 1,
        };

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

        public static AbiType UFixedNxM(int n, int m)
        {
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

        public static AbiType Array(AbiType elementType, int length = -1)
        {
            var isStatic = length >= 0 && elementType.IsStatic;
            if (elementType.ValueType == AbiValueType.Boolean)
            {
                length += 7;
                length /= 8;
            }
            return new AbiType
            {
                name = $"{elementType.Name}[{(isStatic ? length.ToString() : "")}]",
                valueType = AbiValueType.Array,
                isStatic = isStatic,
                staticLength = isStatic ? length * elementType.StaticLength : 0,
                nestedTypes = new[] { elementType },
                n = length
            };
        }

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

        public static AbiType Parse(string type)
        {
            if (!TryParse(type, out AbiType abiType))
                throw new System.ArgumentException($"Could not parse {type}", nameof(type));
            return abiType;
        }

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
                    abiType = Account;
                    return true;
                case "asset":
                    abiType = Asset;
                    return true;
                case "application":
                    abiType = Application;
                    return true;
                case "bool":
                    abiType = Boolean;
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
            abiType = Array(elementAbiType, length);
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
