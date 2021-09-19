using System;
using Unity.Collections;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct DryrunSource
        : IEquatable<DryrunSource>
    {
        [AlgoApiKey("app-index")]
        public ulong AppIndex;
        [AlgoApiKey("field-name")]
        public FixedString32Bytes FieldName;
        [AlgoApiKey("source")]
        public string Source;
        [AlgoApiKey("txn-index")]
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
