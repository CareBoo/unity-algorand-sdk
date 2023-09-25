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
        private MnemonicView mnemonicView;

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
            mnemonicView = new MnemonicView(root.Q<VisualElement>("mnemonic-page"));

            loginView.unlockButton.clicked += () => Unlock(loginView.passwordField.text);
            walletView.newAccountButton.clicked += () => mnemonicView.Open();
            createWalletView.confirmButton.clicked += () => ConfirmNewWallet();

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
            mnemonicView.Close();
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
            var encryptError = accountStore.Encrypt(out var encryptedString);
            if (encryptError != LocalAccountStore.EncryptError.None)
            {
                Debug.LogError($"Failed to encrypt account store: {encryptError}");
                return;
            }

            createWalletView.Close();
            PlayerPrefs.SetString(playerPrefsPath, encryptedString);
            PlayerPrefs.Save();
            walletView.Open(accountStore);
        }

        public void Unlock(string password)
        {
            var encryptedAccountStore = PlayerPrefs.GetString(playerPrefsPath, null);
            if (string.IsNullOrEmpty(encryptedAccountStore))
            {
                loginView.Close();
                createWalletView.Open();
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
                return;
            }

            if (decryptError == LocalAccountStore.DecryptError.InvalidPassword)
            {
                loginView.errorLabel.text = "Invalid password.";
                return;
            }

            createWalletView.Close();
            walletView.Open(accountStore);
        }

        private void OnDisable()
        {
            createWalletView.Close();
            loginView.Close();
            mnemonicView.Close();
            walletView.Close();

            createWalletView = null;
            loginView = null;
            mnemonicView = null;
            walletView = null;

            if (accountStore.IsCreated) accountStore.Dispose();
        }
    }
}
