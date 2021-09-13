using System;
using System.Collections.Generic;

namespace AlgoSdk
{
    public static class AlgoApiFormatterCache<T>
    {
        public static readonly IAlgoApiFormatter<T> Formatter;

        static AlgoApiFormatterCache()
        {
            Formatter = AlgoApiFormatterLookup.GetFormatter<T>();
        }
    }

    public partial class AlgoApiFormatterLookup
    {
        public const string EnsureLookupMethodName = nameof(EnsureLookupInitialized);
        public const string LookupFieldName = nameof(lookup);
        public const string InitLookupMethodName = nameof(InitLookup);

        static Dictionary<Type, object> lookup;

        static void InitLookup()
        {
            lookup = new Dictionary<Type, object>()
            {

            };
        }

        public static IAlgoApiFormatter<T> GetFormatter<T>()
        {
            EnsureLookupInitialized();
            return lookup != null && lookup.TryGetValue(typeof(T), out var formatterTypeObj)
                ? (IAlgoApiFormatter<T>)formatterTypeObj
                : null;
        }
    }
}
