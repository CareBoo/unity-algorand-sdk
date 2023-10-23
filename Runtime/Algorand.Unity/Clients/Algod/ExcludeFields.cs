using Unity.Collections;

namespace Algorand.Unity
{
    public enum ExcludeFields : byte
    {
        Unknown,
        All,
        None
    }

    public static class ExcludeFieldsExtensions
    {
        public const string AllString = "all";

        public const string NoneString = "none";

        public static readonly FixedString32Bytes AllFixedString = AllString;

        public static readonly FixedString32Bytes NoneFixedString = NoneString;

        public static readonly string[] TypeToString = new string[]
        {
            string.Empty,
            AllString,
            NoneString
        };

        public static string ToString(this ExcludeFields excludeFields)
        {
            switch (excludeFields)
            {
                case ExcludeFields.All:
                    return AllString;
                case ExcludeFields.None:
                    return NoneString;
                default:
                    return default;
            }
        }

        public static FixedString32Bytes ToFixedString(this ExcludeFields excludeFields)
        {
            switch (excludeFields)
            {
                case ExcludeFields.All:
                    return AllFixedString;
                case ExcludeFields.None:
                    return NoneFixedString;
            }
            return default;
        }

        public static Optional<FixedString32Bytes> ToOptionalFixedString(this ExcludeFields excludeFields)
        {
            if (excludeFields == ExcludeFields.Unknown)
                return default;
            return excludeFields.ToFixedString();
        }
    }
}
