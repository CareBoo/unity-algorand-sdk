using System;
using UnityEngine.UIElements;

namespace Algorand.Unity.Samples.LocalAccountFlow
{
    public class CreateWalletView
    {
        public readonly VisualElement root;
        public readonly Label errorLabel;
        public readonly TextField passwordField;
        public readonly TextField confirmPasswordField;
        public readonly Button confirmButton;

        public CreateWalletView(VisualElement root)
        {
            this.root = root;
            errorLabel = root.Q<Label>("error-label");
            passwordField = root.Q<TextField>("password-field");
            confirmPasswordField = root.Q<TextField>("confirm-password-field");
            confirmButton = root.Q<Button>("confirm-button");
        }

        public void Open()
        {
            root.style.display = DisplayStyle.Flex;
        }

        public void Close()
        {
            root.style.display = DisplayStyle.None;
            passwordField.SetValueWithoutNotify(string.Empty);
            confirmPasswordField.SetValueWithoutNotify(string.Empty);
        }
    }
}
