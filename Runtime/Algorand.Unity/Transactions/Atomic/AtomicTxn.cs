using System.Text;
using Unity.Collections;

namespace Algorand.Unity
{
    public partial struct Transaction
    {
        /// <summary>
        /// Initialize a new Atomic Txn in the building stage.
        /// </summary>
        /// <returns>
        /// An Atomic Txn in the building stage.
        /// See <see cref="AtomicTxn.Building.AddTxn{T}(T)"/> for adding transactions to this transaction group.
        /// </returns>
        public static AtomicTxn.Building Atomic() => new AtomicTxn.Building();
    }

    public static partial class AtomicTxn
    {
        /// <summary>
        /// Max number of allowed transactions in an atomic transaction.
        /// </summary>
        public const int MaxNumTxns = 16;

        /// <summary>
        /// The prefix to use when converting this group of transactions to bytes.
        /// </summary>
        public static readonly byte[] IdPrefix = Encoding.UTF8.GetBytes("TG");

        private static readonly FixedString32Bytes txnGroupKey = "txlist";
    }
}
