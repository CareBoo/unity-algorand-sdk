using Algorand.Unity.Experimental.Abi;

namespace Algorand.Unity
{
    public static partial class AtomicTxn
    {
        /// <summary>
        /// A Confirmed Atomic Transaction.
        /// </summary>
        public readonly struct Confirmed
        {
            private readonly string[] txnIds;
            private readonly MethodCallResult[] results;
            private readonly ulong confirmedRound;

            /// <summary>
            /// The confirmed transaction ids.
            /// </summary>
            public string[] TxnIds => txnIds;

            /// <summary>
            /// Results from the ABI method calls.
            /// </summary>
            public MethodCallResult[] Results => results;

            /// <summary>
            /// The round this atomic transaction was confirmed.
            /// </summary>
            public ulong ConfirmedRound => confirmedRound;

            public Confirmed(string[] txnIds, MethodCallResult[] results, ulong confirmedRound)
            {
                this.txnIds = txnIds;
                this.results = results;
                this.confirmedRound = confirmedRound;
            }
        }
    }
}
