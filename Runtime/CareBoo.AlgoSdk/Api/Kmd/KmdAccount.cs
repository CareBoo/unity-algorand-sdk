using System;
using System.Threading;
using AlgoSdk.Kmd;
using Cysharp.Threading.Tasks;
using Unity.Collections;
using UnityEngine;

namespace AlgoSdk
{
    public struct KmdAccount
        : IAccount
        , IAsyncSigner
    {
        readonly KmdClient client;
        readonly FixedString128Bytes walletId;
        readonly FixedString128Bytes walletPassword;
        readonly Address address;

        FixedString128Bytes walletHandleToken;

        public KmdAccount(
            KmdClient client,
            FixedString128Bytes walletId,
            FixedString128Bytes walletPassword,
            Address address,
            FixedString128Bytes walletHandleToken = default
        )
        {
            this.client = client;
            this.walletId = walletId;
            this.walletPassword = walletPassword;
            this.address = address;

            this.walletHandleToken = walletHandleToken;
        }

        /// <inheritdoc />
        public Address Address => address;

        public async UniTask<FixedString128Bytes> RefreshWalletHandleToken(CancellationToken cancellationToken = default)
        {
            if (walletHandleToken.Length > 0)
            {
                var (renewErr, renewResponse) = await client.RenewWalletHandleToken(walletHandleToken).WithCancellation(cancellationToken);
                if (renewErr)
                {
                    Debug.LogError(renewErr);
                }
            }
            else
            {
                var (initErr, initResponse) = await client.InitWalletHandleToken(walletId, walletPassword).WithCancellation(cancellationToken);
                if (initErr)
                {
                    Debug.LogError(initErr);
                }
                walletHandleToken = initResponse.WalletHandleToken;
            }
            return walletHandleToken;
        }

        /// <inheritdoc />
        public async UniTask<SignedTxn<T>[]> SignTxnsAsync<T>(T[] txns, TxnIndices txnsToSign, CancellationToken cancellationToken = default)
            where T : ITransaction, IEquatable<T>
        {
            var result = new SignedTxn<T>[txns.Length];
            var indexEnum = txnsToSign.GetEnumerator();
            ErrorResponse signErr;
            SignTransactionResponse signResp;
            while (indexEnum.MoveNext())
            {
                var i = indexEnum.Current;
                (signErr, signResp) = await client.SignTransaction(Address, AlgoApiSerializer.SerializeMessagePack(txns[i]), walletHandleToken, walletPassword);
                if (signErr)
                {
                    Debug.LogError(signErr);
                }
                else
                {
                    result[i] = AlgoApiSerializer.DeserializeMessagePack<SignedTxn<T>>(signResp.SignedTransaction);
                }
            }
            return result;
        }
    }
}
