using System;
using Unity.Collections;

namespace AlgoSdk
{
    /// <summary>
    /// DryrunSource is TEAL source text that gets uploaded, compiled, and inserted into transactions or application state.
    /// </summary>
    [AlgoApiObject]
    public struct DryrunSource
        : IEquatable<DryrunSource>
    {
        [AlgoApiField("app-index", null)]
        public ulong AppIndex;

        /// <summary>
        /// <see cref="FieldName"/> is what kind of sources this is. If lsig then it goes into the <see cref="TransactionSignature.LogicSig"/> of the <see cref="Transaction"/> at <see cref="TransactionIndex"/> in <see cref="DryrunRequest.Transactions"/>. If approv or clearp it goes into the Approval Program or Clear State Program of <see cref="Application"/> at <see cref="AppIndex"/> in <see cref="DryrunRequest.Applications"/>.
        /// </summary>
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
