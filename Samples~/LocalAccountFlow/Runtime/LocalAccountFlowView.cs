using System;
using Algorand.Unity.Crypto;
using UnityEngine;
using UnityEngine.UIElements;

namespace Algorand.Unity.Samples.LocalAccountFlow
{
    [RequireComponent(typeof(UIDocument))]
    public class LocalAccountFlowView : MonoBehaviour
    {
        public string playerPrefsPath = Guid.NewGuid().ToString();

        private CreateWalletView createWalletView;
        private LoginView loginView;
        private WalletView walletView;
        private NewAccountView newAccountView;
        private ImportAccountView importAccountView;

        public LocalAccountStore accountStore;

        private void OnValidate()
        {
            if (string.IsNullOrEmpty(playerPrefsPath))
                playerPrefsPath = Guid.NewGuid().ToString();
        }

        private void OnEnable()
        {
            var document = GetComponent<UIDocument>();
            if (!document) return;
            var root = document.rootVisualElement;
            if (root == null) return;

            loginView = new LoginView(root.Q<VisualElement>("login-page"));
            walletView = new WalletView(root.Q<VisualElement>("wallet-page"));
            createWalletView = new CreateWalletView(root.Q<VisualElement>("create-wallet-page"));
            newAccountView = new NewAccountView(root.Q<VisualElement>("new-account-page"));
            importAccountView = new ImportAccountView(root.Q<VisualElement>("import-account-page"));

            loginView.unlockButton.clicked += () => Unlock(loginView.passwordField.text);
            loginView.newWalletButton.clicked += () =>
            {
                loginView.Close();
                createWalletView.Open();
            };
            walletView.newAccountButton.clicked += () =>
            {
                walletView.Close();
                newAccountView.Open();
            };
            walletView.importAccountButton.clicked += () =>
            {
                walletView.Close();
                importAccountView.Open();
            };
            createWalletView.confirmButton.clicked += () => ConfirmNewWallet();

            newAccountView.cancelButton.clicked += () =>
            {
                newAccountView.Close();
                walletView.Open(accountStore);
            };

            newAccountView.confirmButton.clicked += ConfirmNewAccount;
            importAccountView.confirmButton.clicked += ImportAccount;
            importAccountView.cancelButton.clicked += () =>
            {
                importAccountView.Close();
                walletView.Open(accountStore);
            };

            var encryptedAccountStore = PlayerPrefs.GetString(playerPrefsPath, null);
            if (string.IsNullOrEmpty(encryptedAccountStore))
            {
                createWalletView.Open();
                loginView.Close();
            }
            else
            {
                loginView.Open();
                createWalletView.Close();
            }

            walletView.Close();
            newAccountView.Close();
            importAccountView.Close();
        }

        public void ConfirmNewWallet()
        {
            var password = createWalletView.passwordField.value;
            if (string.IsNullOrEmpty(password) || password.Length < 8)
            {
                createWalletView.errorLabel.text = "Password must be at least 8 characters.";
                return;
            }

            var confirmPassword = createWalletView.confirmPasswordField.value;
            if (password != confirmPassword)
            {
                createWalletView.errorLabel.text = "Passwords do not match.";
                return;
            }

            createWalletView.errorLabel.text = string.Empty;
            using var securePassword = new SodiumString(password);
            accountStore = new LocalAccountStore(securePassword);
            Save();
            createWalletView.Close();
            walletView.Open(accountStore);
        }

        public void ConfirmNewAccount()
        {
            using var seedRef = SodiumReference<Ed25519.Seed>.Alloc();
            newAccountView.mnemonic.RefValue.ToPrivateKey(seedRef.AsSpan());
            using var secretKeyRef = SodiumReference<Ed25519.SecretKey>.Alloc();
            var pk = default(Ed25519.PublicKey);
            Ed25519.GenKeyPair(ref seedRef.RefValue, ref secretKeyRef.RefValue, ref pk);
            accountStore = accountStore.Add(ref secretKeyRef.RefValue);
            newAccountView.Close();
            Save();
            walletView.Open(accountStore);
        }

        public void ImportAccount()
        {
            using var mnemonic = importAccountView.ReadMnemonic();
            if (!mnemonic.RefValue.IsValid())
            {
                importAccountView.errorLabel.text = "Invalid mnemonic";
                return;
            }
            using var seedRef = SodiumReference<Ed25519.Seed>.Alloc();
            mnemonic.RefValue.ToPrivateKey(seedRef.AsSpan());
            using var secretKeyRef = SodiumReference<Ed25519.SecretKey>.Alloc();
            var pk = default(Ed25519.PublicKey);
            Ed25519.GenKeyPair(ref seedRef.RefValue, ref secretKeyRef.RefValue, ref pk);
            accountStore = accountStore.Add(ref secretKeyRef.RefValue);
            importAccountView.Close();
            Save();
            walletView.Open(accountStore);
        }

        public void Unlock(string password)
        {
            var encryptedAccountStore = PlayerPrefs.GetString(playerPrefsPath, null);
            if (string.IsNullOrEmpty(encryptedAccountStore))
            {
                loginView.Close();
                createWalletView.Open();
                createWalletView.errorLabel.text = "No wallet found.";
                return;
            }
            using var securePassword = new SodiumString(password);
            var decryptError = LocalAccountStore.Decrypt(encryptedAccountStore, securePassword, out accountStore);
            if (decryptError == LocalAccountStore.DecryptError.InvalidFormat)
            {
                PlayerPrefs.DeleteKey(playerPrefsPath);
                PlayerPrefs.Save();
                loginView.Close();
                createWalletView.Open();
                createWalletView.errorLabel.text = "Wallet corrupted, please create a new one.";
                return;
            }

            if (decryptError == LocalAccountStore.DecryptError.InvalidPassword)
            {
                loginView.errorLabel.text = "Invalid password.";
                return;
            }

            loginView.Close();
            walletView.Open(accountStore);
        }

        public void Save()
        {
            if (accountStore.IsCreated)
            {
                var encryptError = accountStore.Encrypt(out var encryptedString);
                if (encryptError != LocalAccountStore.EncryptError.None)
                {
                    Debug.LogError($"Failed to encrypt account store: {encryptError}");
                    return;
                }
                PlayerPrefs.SetString(playerPrefsPath, encryptedString);
                PlayerPrefs.Save();
            }
        }

        private void OnDisable()
        {
            createWalletView.Close();
            loginView.Close();
            newAccountView.Close();
            walletView.Close();

            createWalletView = null;
            loginView = null;
            newAccountView = null;
            walletView = null;


            if (accountStore.IsCreated)
            {
                Save();
                accountStore.Dispose();
            }
        }
    }
}
