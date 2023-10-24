using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Algorand.Unity
{
    public abstract class AccountAsset
        : ScriptableObject
        , IAsyncAccountSigner
    {
        public abstract Address Address { get; }
        public abstract UniTask<SignedTxn<T>[]> SignTxnsAsync<T>(T[] txns, TxnIndices txnsToSign, CancellationToken cancellationToken = default) where T : ITransaction, IEquatable<T>;
    }
}
