using System.Collections.Generic;
using Algorand.Unity.LowLevel;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;

namespace Algorand.Unity.Experimental.Abi
{
    /// <summary>
    /// A utility struct that is used in building an <see cref="AppCallTxn"/> from
    /// ABI method spec and arguments.
    /// </summary>
    /// <typeparam name="TArguments">The type of the arguments provided.</typeparam>
    public struct MethodCallBuilder<TArguments>
        : INativeDisposable
        where TArguments : struct, IArgEnumerator<TArguments>
    {
        public const int MaxAbiTxnArguments = AppCallTxn.MaxNumAppArguments - 1;

        private readonly Address sender;
        private readonly TransactionParams txnParams;
        private readonly AppIndex applicationId;
        private readonly TArguments argValues;
        private readonly OnCompletion onComplete;

        private readonly MethodSelector methodSelector;
        private readonly IAbiType[] argTypes;

        private NativeIndexer<IAbiType> txnArgTypes;
        private NativeIndexer<IAbiType> appArgTypes;


        public MethodCallBuilder(
            Address sender,
            TransactionParams txnParams,
            AppIndex applicationId,
            Abi.Method method,
            in TArguments argValues,
            OnCompletion onComplete,
            Allocator allocator
        )
        {
            this.sender = sender;
            this.txnParams = txnParams;
            this.applicationId = applicationId;
            this.argValues = argValues;
            this.onComplete = onComplete;

            this.methodSelector = new MethodSelector(method);
            this.argTypes = new IAbiType[method.Arguments?.Length ?? 0];
            for (var i = 0; i < this.argTypes.Length; i++)
                this.argTypes[i] = method.Arguments[i].Type;

            this.txnArgTypes = new NativeIndexer<IAbiType>(this.argTypes, allocator);
            this.appArgTypes = new NativeIndexer<IAbiType>(this.argTypes, allocator);
            for (var i = 0; i < argTypes.Length; i++)
            {
                if (argTypes[i].IsTransaction())
                    this.txnArgTypes.Add(i);
                else
                    this.appArgTypes.Add(i);
            }
        }

        /// <summary>
        /// Validates that the given transactions match any ABI transaction arguments.
        /// See <see href="https://github.com/algorandfoundation/ARCs/blob/main/ARCs/arc-0004.md#transaction-types">Algorand's ABI specifications on Transaction Types</see>.
        /// </summary>
        /// <typeparam name="TTxns">Type of the given list of transactions</typeparam>
        /// <param name="prevTxns">Previous transactions that are available in the atomic transaction group.</param>
        /// <exception cref="System.InvalidOperationException">Not enough transactions were given to call the method.</exception>
        /// <exception cref="System.InvalidCastException">The given transactions were not the correct transaction type to call the method.</exception>
        public void ValidateTxnArgs<TTxns>(TTxns prevTxns)
            where TTxns : IReadOnlyList<Transaction>
        {
            var p = prevTxns.Count - txnArgTypes.Count;
            if (p < 0)
                throw new System.InvalidOperationException("Not enough transactions available to call method");
            for (var i = 0; i < txnArgTypes.Count; i++)
            {
                var abiTxnType = txnArgTypes[i].TransactionType;
                var prevTxnType = prevTxns[p + i].TransactionType;
                if (abiTxnType != AbiTransactionType.txn && (byte)abiTxnType != (byte)prevTxnType)
                    throw new System.InvalidCastException($"The given txn of type {prevTxnType} does not match required txn type of method abi: {abiTxnType}");
            }
        }

        /// <summary>
        /// Build the <see cref="AppCallTxn"/> from the arguments provided in creating this struct.
        /// </summary>
        /// <returns>An <see cref="AppCallTxn"/>.</returns>
        /// <exception cref="System.ArgumentException">Not enough arguments given to call this method.</exception>
        public AppCallTxn BuildTxn()
        {
            using var appArguments = new NativeListOfList<byte>(Allocator.Temp);
            using var references = new AbiReferences(sender, applicationId, Allocator.Temp);

            appArguments.AddArray(methodSelector);
            var args = this.argValues;
            var numAppArgs = math.min(MaxAbiTxnArguments, appArgTypes.Count);
            for (var i = 0; i < numAppArgs; i++)
            {
                if (i > 0 && !args.TryNext(out args))
                    throw new System.ArgumentException($"Not enough args given for method abi.");

                if (i == MaxAbiTxnArguments - 1 && appArgTypes.Count > MaxAbiTxnArguments)
                {
                    using var remainingBytes = Tuple
                        .Of(args)
                        .Encode(appArgTypes.Slice(i), references, Allocator.Temp);
                    appArguments.Add(remainingBytes.Bytes);
                }
                else
                {
                    var type = appArgTypes[i];
                    using var bytes = args.EncodeCurrent(type, references, Allocator.Temp);
                    appArguments.Add(bytes);
                }
            }

            var tealAppArguments = new CompiledTeal[appArguments.Length];
            for (var i = 0; i < appArguments.Length; i++)
                tealAppArguments[i] = appArguments[i].ToArray();
            return Transaction.AppCall(
                    sender: sender,
                    txnParams: txnParams,
                    applicationId: applicationId,
                    onComplete: onComplete,
                    appArguments: tealAppArguments,
                    accounts: references.GetForeignAccounts(),
                    foreignApps: references.GetForeignApps(),
                    foreignAssets: references.GetForeignAssets()
                );
        }

        ///<inheritdoc />
        public JobHandle Dispose(JobHandle inputDeps)
        {
            return JobHandle.CombineDependencies(
                txnArgTypes.Dispose(inputDeps),
                appArgTypes.Dispose(inputDeps)
            );
        }

        ///<inheritdoc />
        public void Dispose()
        {
            txnArgTypes.Dispose();
            appArgTypes.Dispose();
        }
    }
}
