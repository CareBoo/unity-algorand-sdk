using System.Collections.Generic;
using Algorand.Unity.Crypto;
using Algorand.Unity.Experimental.Abi;
using Algorand.Unity.MessagePack;
using Unity.Collections;

namespace Algorand.Unity
{
    public static partial class AtomicTxn
    {
        /// <summary>
        /// Represents an Atomic Txn group that is currently being built up with more transactions.
        /// </summary>
        /// <remarks>
        /// Once you are done building this txn group, use <see cref="Build"/> to prepare the group
        /// for signing.
        /// </remarks>
        public partial struct Building
        {
            private List<Transaction> _txns;

            private List<(int, Method)> _methodIndices;

            /// <summary>
            /// The current number of transactions in this group.
            /// </summary>
            public int TxnCount => _txns.Count;

            /// <summary>
            /// Get the transaction in this group at the given index.
            /// </summary>
            public Transaction this[int i] => _txns[i];

            private List<Transaction> txns
            {
                get
                {
                    if (_txns == null)
                        _txns = new List<Transaction>(4);

                    return _txns;
                }
            }

            internal List<(int, Method)> methodIndices
            {
                get
                {
                    if (_methodIndices == null)
                        _methodIndices = new List<(int, Method)>();

                    return _methodIndices;
                }
            }

            public IReadOnlyList<Transaction> Txns => txns;

            /// <summary>
            /// Add a transaction to this group.
            /// </summary>
            /// <remarks>
            /// The transaction must not have its <see cref="ITransaction.Group"/> property set.
            /// Cannot add more transactions than <see cref="MaxTxnCount"/>.
            /// </remarks>
            /// <param name="txn">The transaction to add to this group, with a zeroed-out <see cref="ITransaction.Group"/> property.</param>
            /// <typeparam name="T">The type of the transaction.</typeparam>
            /// <returns>
            /// An Atomic Transaction in the Building state, ready to add more transactions or build.
            /// </returns>
            public Building AddTxn<T>(T txn)
                where T : ITransaction
            {
                if (!txn.Group.Equals(default))
                    throw new System.ArgumentException("The given transaction must have its Group field unset.", nameof(txn));
                if (txns.Count == MaxNumTxns)
                    throw new System.NotSupportedException($"Atomic Transaction Groups cannot be larger than {MaxNumTxns} transactions.");

                Transaction raw = default;
                txn.CopyTo(ref raw);
                txns.Add(raw);

                return this;
            }

            /// <summary>
            /// Encode and apply ABI Method arguments to an <see cref="AppCallTxn"/> then add the transaction to this group.
            /// </summary>
            /// <param name="sender">The address of the account that pays the fee and amount.</param>
            /// <param name="txnParams">See <see cref="TransactionParams"/></param>
            /// <param name="applicationId">ID of the application being configured.</param>
            /// <param name="method">The ABI method definition.</param>
            /// <param name="onComplete">Defines what additional actions occur with the transaction.</param>
            /// <param name="methodArgs">The list of arguments to encode.</param>
            /// <typeparam name="T">The type of arg enumerator.</typeparam>
            /// <returns>An Atomic Transaction in the Building state, ready to add more transactions or build.</returns>
            public Building AddMethodCall<T>(
                Address sender,
                TransactionParams txnParams,
                AppIndex applicationId,
                OnCompletion onComplete,
                Method method,
                in T methodArgs
            )
                where T : struct, IArgEnumerator<T>
            {
                using var methodCallBuilder = new MethodCallBuilder<T>(
                    sender,
                    txnParams,
                    applicationId,
                    method,
                    in methodArgs,
                    onComplete,
                    Allocator.Temp
                );
                methodCallBuilder.ValidateTxnArgs(Txns);
                var txn = methodCallBuilder.BuildTxn();
                var result = AddTxn(txn);
                methodIndices.Add((result.TxnCount - 1, method));
                return result;
            }

            /// <summary>
            /// Encode and apply ABI Method arguments to an <see cref="AppCallTxn"/> then add the transaction to this group.
            /// </summary>
            /// <param name="sender">The address of the account that pays the fee and amount.</param>
            /// <param name="txnParams">See <see cref="TransactionParams"/></param>
            /// <param name="applicationId">ID of the application being configured.</param>
            /// <param name="method">The ABI method definition.</param>
            /// <param name="methodArgs">The list of arguments to encode.</param>
            /// <typeparam name="T">The type of arg enumerator.</typeparam>
            /// <returns>An Atomic Transaction in the Building state, ready to add more transactions or build.</returns>
            public Building AddMethodCall<T>(
                Address sender,
                TransactionParams txnParams,
                AppIndex applicationId,
                Method method,
                in T methodArgs
            )
                where T : struct, IArgEnumerator<T>
            {
                return AddMethodCall(sender, txnParams, applicationId, OnCompletion.NoOp, method, in methodArgs);
            }

            /// <summary>
            /// Encode and apply ABI Method arguments to an <see cref="AppCallTxn"/> then add the transaction to this group.
            /// </summary>
            /// <param name="sender">The address of the account that pays the fee and amount.</param>
            /// <param name="txnParams">See <see cref="TransactionParams"/></param>
            /// <param name="applicationId">ID of the application being configured.</param>
            /// <param name="method">The ABI method definition.</param>
            /// <param name="onComplete">Defines what additional actions occur with the transaction.</param>
            /// <param name="methodArgs">The list of arguments to encode.</param>
            /// <returns>An Atomic Transaction in the Building state, ready to add more transactions or build.</returns>
            public Building AddMethodCall(
                Address sender,
                TransactionParams txnParams,
                AppIndex applicationId,
                OnCompletion onComplete,
                Method method,
                params IAbiValue[] methodArgsParams
            )
            {
                var methodArgs = new ArgsArray(methodArgsParams, 0);
                return AddMethodCall(sender, txnParams, applicationId, onComplete, method, methodArgs);
            }

            /// <summary>
            /// Encode and apply ABI Method arguments to an <see cref="AppCallTxn"/> then add the transaction to this group.
            /// </summary>
            /// <param name="sender">The address of the account that pays the fee and amount.</param>
            /// <param name="txnParams">See <see cref="TransactionParams"/></param>
            /// <param name="applicationId">ID of the application being configured.</param>
            /// <param name="method">The ABI method definition.</param>
            /// <param name="methodArgs">The list of arguments to encode.</param>
            /// <returns>An Atomic Transaction in the Building state, ready to add more transactions or build.</returns>
            public Building AddMethodCall(
                Address sender,
                TransactionParams txnParams,
                AppIndex applicationId,
                Method method,
                params IAbiValue[] methodArgsParams
            )
            {
                return AddMethodCall(sender, txnParams, applicationId, OnCompletion.NoOp, method, methodArgsParams);
            }

            /// <summary>
            /// Builds the current Atomic Transaction, generating a group ID and
            /// assigning it to all transactions in this group.
            /// </summary>
            /// <returns>
            /// An Atomic Transaction that's ready to be signed by
            /// different signers.
            /// </returns>
            public Signing Build()
            {
                var txnsArr = AssignGroupIds();
                return new Signing(txnsArr, methodIndices?.ToArray());
            }

            private Transaction[] AssignGroupIds()
            {
                var txnsArr = txns.ToArray();
                if (txnsArr.Length < 2)
                {
                    return txnsArr;
                }
                var writer = new MessagePackWriter(Allocator.Temp);
                try
                {
                    for (var i = 0; i < IdPrefix.Length; i++)
                        writer.Data.Add(IdPrefix[i]);
                    writer.WriteMapHeader(1);
                    writer.WriteString(txnGroupKey);
                    writer.WriteArrayHeader(txns.Count);
                    for (var i = 0; i < txns.Count; i++)
                    {
                        AlgoApiFormatterCache<TransactionId>.Formatter.Serialize(ref writer, txns[i].GetId());
                    }

                    TransactionId groupId = Sha512.Hash256Truncated(writer.Data.AsArray().AsReadOnly());
                    for (var i = 0; i < txns.Count; i++)
                    {
                        txnsArr[i].Group = groupId;
                    }
                    return txnsArr;
                }
                finally
                {
                    writer.Dispose();
                }
            }
        }
    }
}
