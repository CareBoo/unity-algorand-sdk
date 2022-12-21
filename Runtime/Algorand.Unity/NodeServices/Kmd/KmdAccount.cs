using System;
using System.Threading;
using Algorand.Unity.Kmd;
using Cysharp.Threading.Tasks;
using Unity.Collections;
using UnityEngine;

namespace Algorand.Unity
{
    [Serializable]
    public struct KmdAccount
        : IAsyncAccountSigner
    {
        [SerializeField] private KmdClient client;

        [SerializeField] private FixedString128Bytes walletId;

        [SerializeField] private FixedString128Bytes walletPassword;

        [SerializeField] private Address address;

        public KmdAccount(
            KmdClient client,
            FixedString128Bytes walletId,
            FixedString128Bytes walletPassword,
            Address address
        )
        {
            this.client = client;
            this.walletId = walletId;
            this.walletPassword = walletPassword;
            this.address = address;
        }

        /// <inheritdoc />
        public Address Address => address;

        /// <inheritdoc />
        public async UniTask<SignedTxn<T>[]> SignTxnsAsync<T>(T[] txns, TxnIndices txnsToSign, CancellationToken cancellationToken = default)
            where T : ITransaction, IEquatable<T>
        {
            var (initErr, initResponse) = await client.InitWalletHandleToken(walletId, walletPassword)
                .WithCancellation(cancellationToken);
            initErr.ThrowIfError();
            var walletHandleToken = initResponse.WalletHandleToken;
            try
            {
                var result = new SignedTxn<T>[txns.Length];
                var indexEnum = txnsToSign.GetEnumerator();
                ErrorResponse signErr;
                SignTransactionResponse signResp;
                while (indexEnum.MoveNext())
                {
                    var i = indexEnum.Current;
                    var serializedTxn = AlgoApiSerializer.SerializeMessagePack(txns[i]);
                    (signErr, signResp) = await client.SignTransaction(Address, serializedTxn, walletHandleToken, walletPassword);
                    signErr.ThrowIfError();
                    result[i] = AlgoApiSerializer.DeserializeMessagePack<SignedTxn<T>>(signResp.SignedTransaction);
                }
                return result;
            }
            finally
            {
                await client.ReleaseWalletHandleToken(walletHandleToken);
            }
        }
    }
}
