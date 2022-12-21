using System;
using System.Threading;
using Algorand.Unity.LowLevel;
using Cysharp.Threading.Tasks;
using Unity.Collections;

namespace Algorand.Unity
{
    public interface IAccount
    {
        /// <summary>
        /// Address of this account
        /// </summary>
        Address Address { get; }
    }

    public interface IAccountSigner : IAccount, ISigner
    {
    }

    public interface IAsyncAccountSigner : IAccount, IAsyncSigner
    {
    }

    public interface IAsyncAccountSignerWithProgress : IAccount, IAsyncSignerWithProgress
    {
    }

    /// <summary>
    /// A local, in-memory account.
    /// </summary>
    public readonly struct Account
        : IAccount
        , ISigner
        , IAsyncSigner
        , IAsyncSignerWithProgress
        , IAccountSigner
        , IAsyncAccountSigner
        , IAsyncAccountSignerWithProgress
    {
        private readonly PrivateKey privateKey;

        private readonly Address address;

        private readonly bool isRekeyed;

        /// <inheritdoc />
        public Address Address => address;

        public bool IsRekeyed => isRekeyed;

        /// <summary>
        /// Instantiate an in-memory account.
        /// </summary>
        /// <param name="privateKey">The private key of the account.</param>
        public Account(PrivateKey privateKey)
        {
            this.privateKey = privateKey;
            this.address = privateKey.ToAddress();
            this.isRekeyed = false;
        }

        /// <summary>
        /// Instantiate an in-memory account that is rekeyed.
        /// </summary>
        /// <param name="privateKey">The private key of the account.</param>
        /// <param name="address">The address of this account.</param>
        public Account(PrivateKey privateKey, Address address)
        {
            this.privateKey = privateKey;
            this.address = address;
            this.isRekeyed = !address.Equals(privateKey.ToAddress());
        }

        /// <summary>
        /// Generate a random, fresh account
        /// </summary>
        public static Account GenerateAccount()
        {
            var privateKey = Algorand.Unity.Crypto.Random.Bytes<PrivateKey>();
            return new Account(privateKey);
        }

        public void Deconstruct(out PrivateKey privateKey, out Address address)
        {
            privateKey = this.privateKey;
            address = this.address;
        }

        public SignedTxn<T> SignTxn<T>(T txn) where T : ITransaction, IEquatable<T>
        {
            using var kp = privateKey.ToKeyPair();
            using var msg = txn.ToSignatureMessage(Allocator.Persistent);
            var sig = kp.SecretKey.Sign(msg);
            return new SignedTxn<T> { Txn = txn, Sig = sig };
        }

        /// <inheritdoc />
        public SignedTxn<T>[] SignTxns<T>(T[] txns, TxnIndices txnsToSign)
            where T : ITransaction, IEquatable<T>
        {
            var signed = new SignedTxn<T>[txns.Length];
            var txnIndices = txnsToSign.GetEnumerator();
            while (txnIndices.MoveNext())
            {
                var i = txnIndices.Current;
                signed[i] = SignTxn(txns[i]);
            }

            return signed;
        }

        /// <inheritdoc />
        public UniTask<SignedTxn<T>[]> SignTxnsAsync<T>(T[] txns, TxnIndices txnsToSign, CancellationToken cancellationToken = default)
            where T : ITransaction, IEquatable<T>
        {
            return UniTask.FromResult(SignTxns(txns, txnsToSign));
        }

        /// <inheritdoc />
        public UniTask<SignedTxn<T>[]> SignTxnsAsync<T, TProgress>(T[] txns, TxnIndices txnsToSign, TProgress progress, CancellationToken cancellationToken = default)
            where T : ITransaction, IEquatable<T>
            where TProgress : IProgress<float>
        {
            progress.Report(1f);
            return SignTxnsAsync(txns, txnsToSign);
        }
    }
}
