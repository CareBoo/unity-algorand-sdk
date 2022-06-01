using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace AlgoSdk.WalletConnect
{
    [CreateAssetMenu(fileName = "NewWalletConnectAccount", menuName = "AlgoSdk/Accounts/WalletConnectAccount")]
    public class WalletConnectAccountAsset
        : ScriptableObject
        , IAccount
        , IAsyncSigner
    {
        [SerializeField]
        public WalletConnectAccount Account;

        public Address Address => Account.Address;

        public UniTask<SignedTxn<T>[]> SignTxnsAsync<T>(
            T[] txns, TxnIndices txnsToSign,
            CancellationToken cancellationToken = default
            )
            where T : ITransaction, IEquatable<T>
        {
            return Account.SignTxnsAsync(txns, txnsToSign, cancellationToken);
        }
    }
}
