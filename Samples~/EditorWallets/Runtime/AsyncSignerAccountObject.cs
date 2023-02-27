using System;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace Algorand.Unity
{
    public abstract class AsyncSignerAccountObject
        : BaseAccountObject
        , IAsyncSigner
    {
        public abstract UniTask<SignedTxn<T>[]> SignTxnsAsync<T>(T[] txns, TxnIndices txnsToSign, CancellationToken cancellationToken = default) where T : ITransaction, IEquatable<T>;
    }
}
