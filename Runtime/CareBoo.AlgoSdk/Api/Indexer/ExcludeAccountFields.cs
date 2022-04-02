using System;
using Unity.Collections;

namespace AlgoSdk
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

        public const string AssetsString = "assets";

        public const string CreatedAssetsString = "created-assets";

        public const string AppsLocalStateString = "apps-local-state";

        public const string CreatedAppsString = "created-apps";

        public const string NoneString = "none";

        public static string ToString(this ExcludeAccountFields exclude)
        {
            switch (exclude)
            {
                case ExcludeAccountFields.All:
                    return AllString;
                case ExcludeAccountFields.None:
                    return NoneString;
                case ExcludeAccountFields.Unknown:
                    return string.Empty;
            }

            var text = new NativeText(Allocator.Temp);
            try
            {
                if (exclude.HasFlag(ExcludeAccountFields.Assets))
                {
                    text.Append(AssetsString);
                }
                if (exclude.HasFlag(ExcludeAccountFields.CreatedAssets))
                {
                    if (text.Length > 0)
                        text.Append(",");
                    text.Append(CreatedAssetsString);
                }
                if (exclude.HasFlag(ExcludeAccountFields.AppsLocalState))
                {
                    if (text.Length > 0)
                        text.Append(",");
                    text.Append(AppsLocalStateString);
                }
                if (exclude.HasFlag(ExcludeAccountFields.CreatedApps))
                {
                    if (text.Length > 0)
                        text.Append(",");
                    text.Append(CreatedAppsString);
                }
                return text.ToString();
            }
            finally
            {
                text.Dispose();
            }
        }
    }
}
