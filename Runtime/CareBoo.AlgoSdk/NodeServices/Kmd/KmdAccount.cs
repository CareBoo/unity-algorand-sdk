using System;
using System.Threading;
using AlgoSdk.Kmd;
using Cysharp.Threading.Tasks;
using Unity.Collections;
using UnityEngine;

namespace AlgoSdk
{
    [Serializable]
    public struct KmdAccount
        : IAccount
        , IAsyncSigner
    {
        [SerializeField]
        KmdClient client;

        [SerializeField]
        FixedString128Bytes walletId;

        [SerializeField]
        FixedString128Bytes walletPassword;

        [SerializeField]
        Address address;

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
