using System;
using Unity.Collections;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct DryrunSource
        : IEquatable<DryrunSource>
    {
        [AlgoApiField("app-index", null)]
        public ulong AppIndex;

        [AlgoApiField("field-name", null)]
        public FixedString32Bytes FieldName;

        [AlgoApiField("source", null)]
        public string Source;

        [AlgoApiField("txn-index", null)]
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
