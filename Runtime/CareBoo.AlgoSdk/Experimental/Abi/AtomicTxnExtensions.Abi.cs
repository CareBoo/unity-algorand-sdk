using Unity.Collections;
using static AlgoSdk.AtomicTxn;

namespace AlgoSdk.Experimental.Abi
{
    public static partial class AtomicTxnExtensions
    {
        /// <summary>
        /// Encode and apply ABI Method arguments to an <see cref="AppCallTxn"/> then add the transaction to this group.
        /// </summary>
        /// <param name="atomicTxn">The atomic transaction to add a method to.</param>
        /// <param name="sender">The address of the account that pays the fee and amount.</param>
        /// <param name="txnParams">See <see cref="TransactionParams"/></param>
        /// <param name="applicationId">ID of the application being configured.</param>
        /// <param name="method">The ABI method definition.</param>
        /// <param name="onComplete">Defines what additional actions occur with the transaction.</param>
        /// <param name="methodArgs">The list of arguments to encode.</param>
        /// <typeparam name="T">The type of arg enumerator.</typeparam>
        /// <returns>An Atomic Transaction in the Building state, ready to add more transactions or build.</returns>
        public static Building AddMethodCall<T>(
            this Building atomicTxn,
            Address sender,
            TransactionParams txnParams,
            AppIndex applicationId,
            OnCompletion onComplete,
            Abi.Method method,
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
                Allocator.Persistent
            );
            methodCallBuilder.ValidateTxnArgs(atomicTxn.Txns);
            var txn = methodCallBuilder.BuildTxn();
            return atomicTxn.AddTxn(txn);
        }

        /// <summary>
        /// Encode and apply ABI Method arguments to an <see cref="AppCallTxn"/> then add the transaction to this group.
        /// </summary>
        /// <param name="atomicTxn">The atomic transaction to add a method to.</param>
        /// <param name="sender">The address of the account that pays the fee and amount.</param>
        /// <param name="txnParams">See <see cref="TransactionParams"/></param>
        /// <param name="applicationId">ID of the application being configured.</param>
        /// <param name="method">The ABI method definition.</param>
        /// <param name="methodArgs">The list of arguments to encode.</param>
        /// <typeparam name="T">The type of arg enumerator.</typeparam>
        /// <returns>An Atomic Transaction in the Building state, ready to add more transactions or build.</returns>
        public static Building AddMethodCall<T>(
            this Building atomicTxn,
            Address sender,
            TransactionParams txnParams,
            AppIndex applicationId,
            Abi.Method method,
            in T methodArgs
        )
            where T : struct, IArgEnumerator<T>
        {
            return atomicTxn.AddMethodCall(sender, txnParams, applicationId, OnCompletion.NoOp, method, in methodArgs);
        }

        /// <summary>
        /// Encode and apply ABI Method arguments to an <see cref="AppCallTxn"/> then add the transaction to this group.
        /// </summary>
        /// <param name="atomicTxn">The atomic transaction to add a method to.</param>
        /// <param name="sender">The address of the account that pays the fee and amount.</param>
        /// <param name="txnParams">See <see cref="TransactionParams"/></param>
        /// <param name="applicationId">ID of the application being configured.</param>
        /// <param name="method">The ABI method definition.</param>
        /// <param name="onComplete">Defines what additional actions occur with the transaction.</param>
        /// <param name="methodArgs">The list of arguments to encode.</param>
        /// <returns>An Atomic Transaction in the Building state, ready to add more transactions or build.</returns>
        public static Building AddMethodCall(
            this Building atomicTxn,
            Address sender,
            TransactionParams txnParams,
            AppIndex applicationId,
            OnCompletion onComplete,
            Abi.Method method,
            params IAbiValue[] methodArgsParams
        )
        {
            var methodArgs = new ArgsArray(methodArgsParams, 0);
            return atomicTxn.AddMethodCall(sender, txnParams, applicationId, onComplete, method, methodArgs);
        }

        /// <summary>
        /// Encode and apply ABI Method arguments to an <see cref="AppCallTxn"/> then add the transaction to this group.
        /// </summary>
        /// <param name="atomicTxn">The atomic transaction to add a method to.</param>
        /// <param name="sender">The address of the account that pays the fee and amount.</param>
        /// <param name="txnParams">See <see cref="TransactionParams"/></param>
        /// <param name="applicationId">ID of the application being configured.</param>
        /// <param name="method">The ABI method definition.</param>
        /// <param name="methodArgs">The list of arguments to encode.</param>
        /// <returns>An Atomic Transaction in the Building state, ready to add more transactions or build.</returns>
        public static Building AddMethodCall(
            this Building atomicTxn,
            Address sender,
            TransactionParams txnParams,
            AppIndex applicationId,
            Abi.Method method,
            params IAbiValue[] methodArgsParams
        )
        {
            return atomicTxn.AddMethodCall(sender, txnParams, applicationId, OnCompletion.NoOp, method, methodArgsParams);
        }
    }
}
