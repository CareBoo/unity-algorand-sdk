using Algorand.Unity.Crypto;
using Algorand.Unity.Kmd;
using Unity.Collections;

namespace Algorand.Unity
{
    public interface IKmdClient : IAlgoApiClient
    {
        /// <summary>
        /// Gets the current swagger spec.
        /// </summary>
        /// <returns>The entire swagger spec in json.</returns>
        AlgoApiRequest.Sent<AlgoApiObject> GetSwaggerSpec();

        /// <summary>
        /// Generates the next key in the deterministic key sequence (as determined by the master derivation key) and adds it to the wallet, returning the public key.
        /// </summary>
        /// <param name="walletHandleToken"></param>
        /// <param name="displayMnemonic">whether or not to display the mnemonic</param>
        /// <returns></returns>
        AlgoApiRequest.Sent<GenerateKeyResponse> GenerateKey(
            FixedString128Bytes walletHandleToken,
            Optional<bool> displayMnemonic = default
        );

        /// <summary>
        /// Deletes the key with the passed public key from the wallet.
        /// </summary>
        /// <param name="address">public key of the key to delete</param>
        /// <param name="walletHandleToken"></param>
        /// <param name="walletPassword"></param>
        /// <returns></returns>
        AlgoApiRequest.Sent DeleteKey(
            Address address,
            FixedString128Bytes walletHandleToken,
            FixedString128Bytes walletPassword
        );

        /// <summary>
        /// Export the secret key associated with the passed public key.
        /// </summary>
        /// <remarks></remarks>
        /// <param name="address">public key of the key to export</param>
        /// <param name="walletHandleToken"></param>
        /// <param name="walletPassword"></param>
        /// <returns></returns>
        AlgoApiRequest.Sent<ExportKeyResponse> ExportKey(
            Address address,
            FixedString128Bytes walletHandleToken,
            FixedString128Bytes walletPassword
        );

        /// <summary>
        /// Import an externally generated key into the wallet. Note that if you wish to back up the imported key, you must do so by backing up the entire wallet database, because imported keys were not derived from the wallet's master derivation key.
        /// </summary>
        /// <param name="privateKey">key to import</param>
        /// <param name="walletPassword"></param>
        /// <returns></returns>
        AlgoApiRequest.Sent<ImportKeyResponse> ImportKey(
            PrivateKey privateKey,
            FixedString128Bytes walletPassword
        );

        /// <summary>
        /// Lists all of the public keys in this wallet. All of them have a stored private key.
        /// </summary>
        /// <param name="walletHandleToken"></param>
        /// <returns></returns>
        AlgoApiRequest.Sent<ListKeysResponse> ListKeys(
            FixedString128Bytes walletHandleToken
        );

        /// <summary>
        /// Export the master derivation key from the wallet. This key is a master "backup" key for the underlying wallet. With it, you can regenerate all of the wallets that have been generated with this wallet's POST /v1/key endpoint. This key will not allow you to recover keys imported from other wallets, however.
        /// </summary>
        /// <param name="walletHandleToken"></param>
        /// <param name="walletPassword"></param>
        /// <returns></returns>
        AlgoApiRequest.Sent<ExportMasterKeyResponse> ExportMasterKey(
            FixedString128Bytes walletHandleToken,
            FixedString128Bytes walletPassword
        );

        /// <summary>
        /// Deletes multisig preimage information for the passed address from the wallet.
        /// </summary>
        /// <param name="address">public key for the key to delete multisig preimage information</param>
        /// <param name="walletHandleToken"></param>
        /// <param name="walletPassword"></param>
        /// <returns></returns>
        AlgoApiRequest.Sent DeleteMultisig(
            Address address,
            FixedString128Bytes walletHandleToken,
            FixedString128Bytes walletPassword
        );

        /// <summary>
        /// Given a multisig address whose preimage this wallet stores, returns the information used to generate the address, including public keys, threshold, and multisig version.
        /// </summary>
        /// <param name="address">public key for the key to export multisig preimage information</param>
        /// <param name="walletHandleToken"></param>
        /// <returns></returns>
        AlgoApiRequest.Sent<ExportMultisigResponse> ExportMultisig(
            Address address,
            FixedString128Bytes walletHandleToken
        );

        /// <summary>
        /// Generates a multisig account from the passed public keys array and multisig metadata, and stores all of this in the wallet.
        /// </summary>
        /// <param name="version">Multisig version. This should always be set to 1.</param>
        /// <param name="publicKeys">Public keys for the accounts used to sign</param>
        /// <param name="threshold">Number of valid signatures required</param>
        /// <param name="walletHandleToken"></param>
        /// <returns></returns>
        AlgoApiRequest.Sent<ImportMultisigResponse> ImportMultisig(
            Ed25519.PublicKey[] publicKeys,
            byte threshold,
            FixedString128Bytes walletHandleToken,
            byte version = 1
        );

        /// <summary>
        /// Lists all of the multisig accounts whose preimages this wallet stores
        /// </summary>
        /// <param name="walletHandleToken"></param>
        /// <returns></returns>
        AlgoApiRequest.Sent<ListMultisigResponse> ListMultisig(
            FixedString128Bytes walletHandleToken
        );

        /// <summary>
        /// Sign a multisig transaction
        /// </summary>
        /// <remarks>
        /// Start a multisig signature, or add a signature to a partially completed multisig signature object.
        /// </remarks>
        /// <param name="msig">Current multisig signature object</param>
        /// <param name="publicKey">public key of the key to use to add a signature to the multisig</param>
        /// <param name="transactionData">transaction serialized as msgpack</param>
        /// <param name="walletHandleToken"></param>
        /// <param name="walletPassword"></param>
        /// <returns></returns>
        AlgoApiRequest.Sent<SignMultisigResponse> SignMultisig(
            MultisigSig msig,
            Ed25519.PublicKey publicKey,
            byte[] transactionData,
            FixedString128Bytes walletHandleToken,
            FixedString128Bytes walletPassword
        );

        /// <summary>
        /// Sign a program for a multisig account
        /// </summary>
        /// <remarks>
        /// Start a multisig signature, or add a signature to a partially completed multisig signature object.
        /// </remarks>
        /// <param name="msigAccount">The address of the multisig account. See <see href="https://developer.algorand.org/docs/get-details/accounts/create/#multisignature"/></param>
        /// <param name="programData">compiled program bytes</param>
        /// <param name="msig">current multisig signature object</param>
        /// <param name="publicKey">public key of the key to use to add a signature to the multisig</param>
        /// <param name="walletHandleToken"></param>
        /// <param name="walletPassword"></param>
        /// <returns></returns>
        AlgoApiRequest.Sent<SignProgramMultisigResponse> SignProgramMultisig(
            Address msigAccount,
            byte[] programData,
            MultisigSig msig,
            Ed25519.PublicKey publicKey,
            FixedString128Bytes walletHandleToken,
            FixedString128Bytes walletPassword
        );

        /// <summary>
        /// Sign program
        /// </summary>
        /// <remarks>
        /// Signs the passed program with a key from the wallet, determined by the account named in the request.
        /// </remarks>
        /// <param name="account">Account to sign the program with</param>
        /// <param name="programData">compiled program bytes</param>
        /// <param name="walletHandleToken"></param>
        /// <param name="walletPassword"></param>
        /// <returns></returns>
        AlgoApiRequest.Sent<SignProgramResponse> SignProgram(
            Address account,
            byte[] programData,
            FixedString128Bytes walletHandleToken,
            FixedString128Bytes walletPassword
        );

        /// <summary>
        /// Sign a transaction
        /// </summary>
        /// <param name="account">public key of the key to sign the transaction</param>
        /// <param name="transactionData">transaction serialized as msgpack</param>
        /// <param name="walletHandleToken"></param>
        /// <param name="walletPassword"></param>
        /// <returns></returns>
        AlgoApiRequest.Sent<SignTransactionResponse> SignTransaction(
            Address account,
            byte[] transactionData,
            FixedString128Bytes walletHandleToken,
            FixedString128Bytes walletPassword
        );

        /// <summary>
        /// Create a wallet
        /// </summary>
        /// <remarks>
        /// Create a new wallet (collection of keys) with the given parameters.
        /// </remarks>
        /// <param name="masterDerivationKey"></param>
        /// <param name="walletDriverName">The driver used to store the wallets. Supported values are "sqlite" and "ledger".</param>
        /// <param name="walletName"></param>
        /// <param name="walletPassword"></param>
        /// <returns></returns>
        AlgoApiRequest.Sent<CreateWalletResponse> CreateWallet(
            PrivateKey masterDerivationKey,
            FixedString128Bytes walletDriverName,
            FixedString128Bytes walletName,
            FixedString128Bytes walletPassword
        );

        /// <summary>
        /// Get wallet info
        /// </summary>
        /// <remarks>
        /// Returns information about the wallet associated with the passed wallet handle token. Additionally returns expiration information about the token itself.
        /// </remarks>
        /// <param name="walletHandleToken"></param>
        /// <returns></returns>
        AlgoApiRequest.Sent<WalletInfoResponse> WalletInfo(
            FixedString128Bytes walletHandleToken
        );

        /// <summary>
        /// Initialize a wallet handle token
        /// </summary>
        /// <remarks>
        /// Unlock the wallet and return a wallet handle token that can be used for subsequent operations. These tokens expire periodically and must be renewed.
        /// You can use <see cref="WalletInfo"/> to see how much time remains until expiration, and renew it with <see cref="RenewWalletHandleToken"/>.
        /// When you're done, you can invalidate the token with <see cref="ReleaseWalletHandleToken"/>.
        /// </remarks>
        /// <param name="walletId"></param>
        /// <param name="walletPassword"></param>
        /// <returns></returns>
        AlgoApiRequest.Sent<InitWalletHandleTokenResponse> InitWalletHandleToken(
            FixedString128Bytes walletId,
            FixedString128Bytes walletPassword
        );

        /// <summary>
        /// Release a wallet handle token
        /// </summary>
        /// <remarks>
        /// Invalidate the passed wallet handle token, making it invalid for use in subsequent requests.
        /// </remarks>
        /// <param name="walletHandleToken"></param>
        /// <returns></returns>
        AlgoApiRequest.Sent ReleaseWalletHandleToken(
            FixedString128Bytes walletHandleToken
        );

        /// <summary>
        /// Rename a wallet
        /// </summary>
        /// <remarks>
        /// Rename the underlying wallet to something else
        /// </remarks>
        /// <param name="walletId"></param>
        /// <param name="walletName"></param>
        /// <param name="walletPassword"></param>
        /// <returns></returns>
        AlgoApiRequest.Sent<RenameWalletResponse> RenameWallet(
            FixedString128Bytes walletId,
            FixedString128Bytes walletName,
            FixedString128Bytes walletPassword
        );

        /// <summary>
        /// Renew a wallet handle token
        /// </summary>
        /// <remarks>
        /// Renew a wallet handle token, increasing its expiration duration to its initial value
        /// </remarks>
        /// <param name="walletHandleToken"></param>
        /// <returns></returns>
        AlgoApiRequest.Sent<RenewWalletHandleTokenResponse> RenewWalletHandleToken(
            FixedString128Bytes walletHandleToken
        );

        /// <summary>
        /// List Wallets
        /// </summary>
        /// <remarks>
        /// Lists all of the wallets that kmd is aware of.
        /// </remarks>
        /// <returns></returns>
        AlgoApiRequest.Sent<ListWalletsResponse> ListWallets();

        /// <summary>
        /// Retrieves the current version of the kmd service
        /// </summary>
        /// <returns></returns>
        AlgoApiRequest.Sent<VersionsResponse> Versions();
    }
}
