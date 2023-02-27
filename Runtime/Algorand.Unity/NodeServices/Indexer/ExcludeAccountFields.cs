using System;
using Unity.Collections;

namespace Algorand.Unity
{
    [Flags]
    public enum ExcludeAccountFields : byte
    {
        Unknown = 0,
        None = 1 << 0,
        Assets = 1 << 1,
        CreatedAssets = 1 << 2,
        AppsLocalState = 1 << 3,
        CreatedApps = 1 << 4,
        All = Assets | CreatedAssets | AppsLocalState | CreatedApps,
    }

    public static class ExcludeAccountFieldsExtensions
    {
        public const string AllString = "all";

        public static readonly FixedString32Bytes AllFixedString = AllString;

        public const string AssetsString = "assets";

        public static readonly FixedString32Bytes AssetsFixedString = AssetsString;

        public const string CreatedAssetsString = "created-assets";

        public static readonly FixedString32Bytes CreatedAssetsFixedString = CreatedAssetsString;

        public const string AppsLocalStateString = "apps-local-state";

        public static readonly FixedString32Bytes AppsLocalStateFixedString = AppsLocalStateString;

        public const string CreatedAppsString = "created-apps";

        public static readonly FixedString32Bytes CreatedAppsFixedString = CreatedAppsString;

        public const string NoneString = "none";

        public static readonly FixedString32Bytes NoneFixedString = NoneString;

        public static FixedString128Bytes ToFixedString(this ExcludeAccountFields exclude)
        {
            FixedString128Bytes result = default;
            switch (exclude)
            {
                case ExcludeAccountFields.All:
                    return AllFixedString;
                case ExcludeAccountFields.None:
                    return NoneFixedString;
                case ExcludeAccountFields.Unknown:
                    return result;
            }

            if (exclude.HasFlag(ExcludeAccountFields.Assets))
            {
                result.Append(AssetsString);
            }
            if (exclude.HasFlag(ExcludeAccountFields.CreatedAssets))
            {
                if (result.Length > 0)
                    result.Append(",");
                result.Append(CreatedAssetsString);
            }
            if (exclude.HasFlag(ExcludeAccountFields.AppsLocalState))
            {
                if (result.Length > 0)
                    result.Append(",");
                result.Append(AppsLocalStateString);
            }
            if (exclude.HasFlag(ExcludeAccountFields.CreatedApps))
            {
                if (result.Length > 0)
                    result.Append(",");
                result.Append(CreatedAppsString);
            }
            return result;
        }

        public static Optional<FixedString128Bytes> ToOptionalFixedString(this ExcludeAccountFields exclude)
        {
            var fs = exclude.ToFixedString();
            if (fs.Length == 0)
                return default;
            return fs;
        }
    }
}
