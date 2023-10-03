using System;
using System.Runtime.InteropServices;
using Algorand.Unity.Crypto;

namespace Algorand.Unity
{
    public readonly partial struct LocalAccountStore
    {
        /// <summary>
        /// Errors that can occur when trying to decrypt and retrieve an account store from string.
        /// </summary>
        public enum DecryptError
        {
            /// <summary>
            /// No error occurred.
            /// </summary>
            None,

            /// <summary>
            /// The store was not in the correct format.
            /// </summary>
            InvalidFormat,

            /// <summary>
            /// The password is invalid.
            /// </summary>
            InvalidPassword
        }

        /// <summary>
        /// Decrypt the encrypted store with the given password.
        /// </summary>
        /// <param name="encryptedStore"></param>
        /// <param name="password"></param>
        /// <param name="store"></param>
        /// <returns></returns>
        public static DecryptError Decrypt(string encryptedStore, SodiumString password, out LocalAccountStore store)
        {
            store = default;
            var encryptedStoreBytes = Convert.FromBase64String(encryptedStore);
            var encryptedView = new EncryptedView(encryptedStoreBytes);
            if (!encryptedView.IsValidFormat) return DecryptError.InvalidFormat;

            var decryptError = encryptedView.Decrypt(password, out var secretKeys, out var pwHash);
            if (decryptError != EncryptedView.DecryptError.None)
            {
                return DecryptError.InvalidPassword;
            }

            store = new LocalAccountStore(secretKeys, pwHash, encryptedView.header.salt);
            return DecryptError.None;
        }
    }
}
