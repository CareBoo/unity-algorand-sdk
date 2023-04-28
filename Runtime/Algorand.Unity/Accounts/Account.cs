using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Unity.Collections;
using Random = Algorand.Unity.Crypto.Random;

namespace Algorand.Unity
{
    /// <summary>
    ///     An Algorand Account which has an <see cref="Address" /> to identify it.
    /// </summary>
    public interface IAccount
    {
        /// <summary>
        ///     Address of this account
        /// </summary>
        Address Address { get; }
    }

    /// <summary>
    ///     A signer for a particular <see cref="Address" />. There is only one valid Signer for an <see cref="Address" />.
    /// </summary>
    public interface IAccountSigner : IAccount, ISigner
    {
    }

    /// <summary>
    ///     An asynchronous signer for a particular <see cref="Address" />.
    /// </summary>
    public interface IAsyncAccountSigner : IAccount, IAsyncSigner
    {
    }

    /// <summary>
    ///     An asynchronous signer for a particular <see cref="Address" /> that contains signing functions that support
    ///     progress updates.
    /// </summary>
    public interface IAsyncAccountSignerWithProgress : IAccount, IAsyncSignerWithProgress
    {
    }

    /// <summary>
    ///     A insecure, local, in-memory account. This is useful for creating quick accounts for developer testing, and it
    ///     should not be used in production.
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

        /// <inheritdoc />
        public Address Address { get; }

        public bool IsRekeyed { get; }

        /// <summary>
        ///     Instantiate an in-memory account.
        /// </summary>
        /// <param name="privateKey">The private key of the account.</param>
        public Account(PrivateKey privateKey)
        {
            this.privateKey = privateKey;
            Address = privateKey.ToAddress();
            IsRekeyed = false;
        }

        /// <summary>
        ///     Instantiate an in-memory account that is rekeyed.
        /// </summary>
        /// <param name="privateKey">The private key of the account.</param>
        /// <param name="address">The address of this account.</param>
        public Account(PrivateKey privateKey, Address address)
        {
            this.privateKey = privateKey;
            Address = address;
            IsRekeyed = !address.Equals(privateKey.ToAddress());
        }

        /// <summary>
        ///     Instantiate an in-memory account from a mnemonic.
        /// </summary>
        /// <param name="mnemonic">The 25 word mnemonic to instantiate the account with.</param>
        public Account(Mnemonic mnemonic) : this(mnemonic.ToPrivateKey())
        {
        }

        /// <summary>
        ///     Instantiate a rekeyed, in-memory account from a mnemonic.
        /// </summary>
        /// <param name="mnemonic">The mnemonic representing this account's private key.</param>
        /// <param name="address">The address this account is rekeyed to.</param>
        public Account(Mnemonic mnemonic, Address address) : this(mnemonic.ToPrivateKey(), address)
        {
        }

        /// <summary>
        ///     Instantiate an in-memory account from a string that can be either a private key or mnemonic.
        /// </summary>
        /// <param name="privateKeyOrMnemonic">A string that is either an mnemonic or a private key.</param>
        /// <exception cref="ArgumentException">The given string is not a mnemonic or a private key.</exception>
        public Account(string privateKeyOrMnemonic)
        {
            var mnemonicParseError = Mnemonic.TryParse(privateKeyOrMnemonic, out var mnemonic);
            if (mnemonicParseError > 0)
            {
                var pkParseError = PrivateKey.TryParse(privateKeyOrMnemonic, out privateKey);
                if (pkParseError > 0)
                    throw new ArgumentException("Invalid private key or mnemonic", nameof(privateKeyOrMnemonic));
            }
            else
            {
                privateKey = mnemonic.ToPrivateKey();
            }

            Address = privateKey.ToAddress();
            IsRekeyed = false;
        }

        /// <summary>
        ///     Instantiate a rekeyed, in-memory account from a string that can be either a private key or mnemonic.
        /// </summary>
        /// <param name="privateKeyOrMnemonic">A string that is either an mnemonic or a private key.</param>
        /// <param name="address">The address this account is rekeyed to.</param>
        /// <exception cref="ArgumentException">The given string is not a mnemonic or a private key.</exception>
        public Account(string privateKeyOrMnemonic, Address address) : this(privateKeyOrMnemonic)
        {
            IsRekeyed = !address.Equals(Address);
            Address = address;
        }

        /// <summary>
        ///     Generate a random, fresh account
        /// </summary>
        public static Account GenerateAccount()
        {
            var privateKey = Random.Bytes<PrivateKey>();
            return new Account(privateKey);
        }

        /// <summary>
        ///     Represent this account as its private key and public key.
        /// </summary>
        /// <param name="privateKey">The private key this account uses to sign transactions.</param>
        /// <param name="address">The public key or address of this account.</param>
        public void Deconstruct(out PrivateKey privateKey, out Address address)
        {
            privateKey = this.privateKey;
            address = Address;
        }

        /// <summary>
        ///     Sign a single transaction.
        /// </summary>
        /// <param name="txn">The transaction to sign.</param>
        /// <typeparam name="T">The type of the transaction.</typeparam>
        /// <returns>A signed transaction, signed with this account's <see cref="PrivateKey" />.</returns>
        public SignedTxn<T> SignTxn<T>(T txn) where T : ITransaction, IEquatable<T>
        {
            using var kp = privateKey.ToKeyPair();
            using var msg = txn.ToSignatureMessage(Allocator.Temp);
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
        public UniTask<SignedTxn<T>[]> SignTxnsAsync<T>(T[] txns, TxnIndices txnsToSign,
            CancellationToken cancellationToken = default)
            where T : ITransaction, IEquatable<T>
        {
            return UniTask.FromResult(SignTxns(txns, txnsToSign));
        }

        /// <inheritdoc />
        public UniTask<SignedTxn<T>[]> SignTxnsAsync<T, TProgress>(T[] txns, TxnIndices txnsToSign, TProgress progress,
            CancellationToken cancellationToken = default)
            where T : ITransaction, IEquatable<T>
            where TProgress : IProgress<float>
        {
            progress.Report(1f);
            return SignTxnsAsync(txns, txnsToSign);
        }
    }
}