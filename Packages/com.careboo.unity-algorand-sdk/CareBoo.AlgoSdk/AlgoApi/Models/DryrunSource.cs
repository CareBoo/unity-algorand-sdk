using System;
using Unity.Collections;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct DryrunSource
        : IEquatable<DryrunSource>
    {
        [AlgoApiKey("app-index", null)]
        public ulong AppIndex;
        [AlgoApiKey("field-name", null)]
        public FixedString32Bytes FieldName;
        [AlgoApiKey("source", null)]
        public string Source;
        [AlgoApiKey("txn-index", null)]
        public ulong TransactionIndex;

        public bool Equals(DryrunSource other)
        {
            return AppIndex.Equals(other.AppIndex)
                && FieldName.Equals(other.FieldName)
                && StringComparer.Equals(Source, other.Source)
                && TransactionIndex.Equals(other.TransactionIndex)
                ;
        }
    }
}
