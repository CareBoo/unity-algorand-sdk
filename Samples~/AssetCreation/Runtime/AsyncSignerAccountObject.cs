using System;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace AlgoSdk
{
    public abstract class AsyncSignerAccountObject
        : AccountObject
        , IAsyncSigner
    {
        public abstract UniTask<SignedTxn<T>[]> SignTxnsAsync<T>(T[] txns, TxnIndices txnsToSign, CancellationToken cancellationToken = default) where T : ITransaction, IEquatable<T>;
    }
}
