using Algorand.Unity.Crypto;
using UnityEngine.UIElements;

namespace Algorand.Unity.Samples.LocalAccountFlow
{
    public class ImportAccountView
    {
        public readonly VisualElement root;
        public readonly Label errorLabel;
        public readonly Button confirmButton;
        public readonly Button cancelButton;
        public readonly MnemonicWordView[] wordViews;

        public ImportAccountView(VisualElement root)
        {
            this.root = root;
            errorLabel = root.Q<Label>("error-label");
            confirmButton = root.Q<Button>("confirm-button");
            cancelButton = root.Q<Button>("cancel-button");
            wordViews = new MnemonicWordView[Mnemonic.Length];
            for (int i = 0; i < wordViews.Length; i++)
            {
                wordViews[i] = new MnemonicWordView(root.Q<VisualElement>($"mnemonic-word-{i}"))
                {
                    Id = $"{i + 1}.)",
                    IsReadOnly = false
                };
            }
        }

        public SodiumReference<Mnemonic> ReadMnemonic()
        {
            var mnemonic = SodiumReference<Mnemonic>.Alloc();
            mnemonic.Value = new Mnemonic();
            for (var i = 0; i < wordViews.Length; i++)
            {
                mnemonic.RefValue[i] = Mnemonic.ParseWord(wordViews[i].Text);
            }
            return mnemonic;
        }

        public void Open()
        {
            for (var i = 0; i < wordViews.Length; i++)
            {
                wordViews[i].Text = string.Empty;
            }
            root.style.display = DisplayStyle.Flex;
        }

        public void Close()
        {
            root.style.display = DisplayStyle.None;
            for (var i = 0; i < wordViews.Length; i++)
            {
                wordViews[i].Text = string.Empty;
            }
            errorLabel.text = "";
        }
    }
}
