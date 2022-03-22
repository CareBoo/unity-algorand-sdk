using System;
using Cysharp.Threading.Tasks;
using Unity.Collections;

namespace AlgoSdk
{
    public interface IAccount
    {
        /// <summary>
        /// Address of this account
        /// </summary>
        Address Address { get; }
    }

    /// <summary>
    /// A local, in-memory account.
    /// </summary>
    public readonly struct Account
        : IAccount
        , ISigner
        , IAsyncSigner
    {
        readonly PrivateKey privateKey;

        readonly Address address;

        readonly bool isRekeyed;

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
            var privateKey = AlgoSdk.Crypto.Random.Bytes<PrivateKey>();
            return new Account(privateKey);
        }

        public void Deconstruct(out PrivateKey privateKey, out Address address)
        {
            privateKey = this.privateKey;
            address = this.address;
        }

        public UniTask<SignedTxn<T>> SignTxnAsync<T>(T txn) where T : ITransaction, IEquatable<T>
        {
            return UniTask.FromResult(SignTxn(txn));
        }

        public UniTask<SignedTxn<T>[]> SignTxnsAsync<T>(T[] txns) where T : ITransaction, IEquatable<T>
        {
            return UniTask.FromResult(SignTxns(txns));
        }

        public SignedTxn<T> SignTxn<T>(T txn) where T : ITransaction, IEquatable<T>
        {
            using var kp = privateKey.ToKeyPair();
            using var msg = txn.ToSignatureMessage(Allocator.Temp);
            var sig = kp.SecretKey.Sign(msg);
            return new SignedTxn<T> { Txn = txn, Sig = sig };
        }

        public SignedTxn<T>[] SignTxns<T>(T[] txns) where T : ITransaction, IEquatable<T>
        {
            var groupId = TransactionGroup.Of(txns).GetId();
            var signedTxns = new SignedTxn<T>[txns.Length];
            for (var i = 0; i < txns.Length; i++)
            {
                var txn = txns[i];
                txn.Group = groupId;
                signedTxns[i] = txn.Sender.Equals(Address)
                    ? SignTxn(txn)
                    : new SignedTxn<T> { Txn = txn }
                    ;
            }
            return signedTxns;
        }
    }
}
