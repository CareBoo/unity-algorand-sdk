using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Algorand.Unity.Crypto;
using Unity.Collections;
using Unity.Jobs;
using Random = Algorand.Unity.Crypto.Random;

namespace Algorand.Unity
{
    /// <summary>
    /// A memory store of Algorand accounts. Can be encrypted as a string.
    /// </summary>
    public readonly partial struct LocalAccountStore : INativeDisposable
    {
        private readonly SodiumArray<Ed25519.SecretKey> secretKeys;
        private readonly SodiumReference<SecretBox.Key> pwHash;
        private readonly PwHash.Salt salt;

        /// <summary>
        /// Whether the store has been created.
        /// </summary>
        public bool IsCreated => secretKeys.IsCreated;

        /// <summary>
        /// The number of accounts in the store.
        /// </summary>
        public int Length => secretKeys.Length;

        /// <summary>
        /// The account at the given index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns>A view into the stores memory, to perform account operations.</returns>
        public unsafe Account this[int index]
        {
            get
            {
                CheckIndexRange(index);
                ref var secretKey = ref secretKeys[index];
                fixed (Ed25519.SecretKey* sk = &secretKey)
                {
                    return new Account(sk);
                }
            }
        }

        /// <summary>
        /// Create a new store with the given secret keys.
        /// </summary>
        /// <param name="secretKeys"></param>
        /// <param name="pwHash"></param>
        /// <param name="salt"></param>
        public LocalAccountStore(
            SodiumArray<Ed25519.SecretKey> secretKeys,
            SodiumReference<SecretBox.Key> pwHash,
            PwHash.Salt salt)
        {
            this.secretKeys = secretKeys;
            this.pwHash = pwHash;
            this.salt = salt;
        }

        /// <summary>
        /// Create a new, empty store with the given password.
        /// </summary>
        /// <param name="password"></param>
        public LocalAccountStore(SodiumString password)
        {
            var newSalt = Random.Bytes<PwHash.Salt>();
            var pwHashError = PwHash.Hash(
                password,
                newSalt,
                SecretBox.Key.SizeBytes,
                out var pwHashHandle);
            if (pwHashError != PwHash.HashError.None)
            {
                throw new InvalidOperationException($"Failed to hash password with error {pwHashError}.");
            }

            secretKeys = new SodiumArray<Ed25519.SecretKey>(0);
            pwHash = new SodiumReference<SecretBox.Key>(pwHashHandle);
            salt = newSalt;
        }

        /// <summary>
        /// Create a new store with an additional account.
        /// </summary>
        /// <param name="secretKey"></param>
        /// <returns></returns>
        public LocalAccountStore Add(ref Ed25519.SecretKey secretKey)
        {
            var newSecretKeys = new SodiumArray<Ed25519.SecretKey>(Length + 1);
            secretKeys.AsSpan().CopyTo(newSecretKeys.AsSpan());
            newSecretKeys[Length] = secretKey;
            secretKeys.Dispose();
            return new LocalAccountStore(newSecretKeys, pwHash, salt);
        }

        /// <summary>
        /// Create a new store without the account at the given index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public LocalAccountStore RemoveAt(int index)
        {
            CheckIndexRange(index);
            var newSecretKeys = new SodiumArray<Ed25519.SecretKey>(Length - 1);
            secretKeys.AsSpan().Slice(0, index).CopyTo(newSecretKeys.AsSpan());
            secretKeys.AsSpan().Slice(index + 1).CopyTo(newSecretKeys.AsSpan().Slice(index));
            secretKeys.Dispose();
            return new LocalAccountStore(newSecretKeys, pwHash, salt);
        }

        /// <summary>
        /// Dispose of the store.
        /// </summary>
        /// <param name="inputDeps"></param>
        /// <returns></returns>
        public JobHandle Dispose(JobHandle inputDeps)
        {
            return JobHandle.CombineDependencies(secretKeys.Dispose(inputDeps), pwHash.Dispose(inputDeps));
        }

        /// <summary>
        /// Dispose of the store.
        /// </summary>
        public void Dispose()
        {
            secretKeys.Dispose();
            pwHash.Dispose();
        }

        [Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
        private void CheckGenKeyPairError(Ed25519.GenKeyPairError error)
        {
            if (error != Ed25519.GenKeyPairError.None)
            {
                throw new InvalidOperationException($"Failed to generate key pair with error {error}.");
            }
        }

        [Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
        private void CheckIndexRange(int index)
        {
            if (index < 0 || index >= Length)
            {
                throw new ArgumentOutOfRangeException(nameof(index), $"Index {index} is out of range of '{Length}' Length.");
            }
        }

        /// <summary>
        /// An account view into the <see cref="LocalAccountStore"/>.
        /// </summary>
        public readonly struct Account : IAccountSigner
        {
            private readonly unsafe Ed25519.SecretKey* secretKeyPtr;

            internal unsafe Account(Ed25519.SecretKey* sk)
            {
                secretKeyPtr = sk;
            }

            /// <summary>
            ///     Sign a single transaction.
            /// </summary>
            /// <param name="txn">The transaction to sign.</param>
            /// <typeparam name="T">The type of the transaction.</typeparam>
            /// <returns>A signed transaction, signed with this account's <see cref="PrivateKey" />.</returns>
            public SignedTxn<T> SignTxn<T>(T txn) where T : ITransaction, IEquatable<T>
            {
                using var msg = txn.ToSignatureMessage(Allocator.Temp);
                var sig = default(Ed25519.Signature);
                unsafe
                {
                    sodium.crypto_sign_ed25519_detached(
                        &sig,
                        out _,
                        msg.GetUnsafePtr(),
                        (ulong)msg.Length,
                        secretKeyPtr);
                }
                return new SignedTxn<T> { Txn = txn, Sig = sig };
            }

            /// <inheritdoc />
            public unsafe Address Address
            {
                get
                {
                    Ed25519.PublicKey pk = default;
                    sodium.crypto_sign_ed25519_sk_to_pk(&pk, secretKeyPtr);
                    return pk;
                }
            }

            /// <inheritdoc />
            public SignedTxn<T>[] SignTxns<T>(T[] txns, TxnIndices txnsToSign) where T : ITransaction, IEquatable<T>
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
        }
    }
}
