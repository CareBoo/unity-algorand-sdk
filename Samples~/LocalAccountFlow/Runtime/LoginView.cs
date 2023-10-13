using UnityEngine.UIElements;

namespace Algorand.Unity.Samples.LocalAccountFlow
{
    public class LoginView
    {
        public readonly VisualElement root;
        public readonly Label errorLabel;
        public readonly TextField passwordField;
        public readonly Button unlockButton;
        public readonly Button newWalletButton;

        public LoginView(VisualElement root)
        {
            this.root = root;

            errorLabel = root.Q<Label>("error-label");
            errorLabel.text = string.Empty;

            passwordField = root.Q<TextField>("password-field");

            unlockButton = root.Q<Button>("unlock-button");
            newWalletButton = root.Q<Button>("new-wallet-button");
        }

        public void Open()
        {
            root.style.display = DisplayStyle.Flex;
            passwordField.SetValueWithoutNotify(string.Empty);
        }

        public void Close()
        {
            root.style.display = DisplayStyle.None;
            passwordField.SetValueWithoutNotify(string.Empty);
        }
    }
}
