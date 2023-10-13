using Algorand.Unity.Crypto;
using UnityEngine.UIElements;
using Random = Algorand.Unity.Crypto.Random;

namespace Algorand.Unity.Samples.LocalAccountFlow
{
    public class NewAccountView
    {
        public SodiumReference<Mnemonic> mnemonic;
        public readonly VisualElement root;
        public readonly Button confirmButton;
        public readonly Button cancelButton;
        public readonly MnemonicWordView[] wordViews;

        public NewAccountView(VisualElement root)
        {
            this.root = root;
            confirmButton = root.Q<Button>("confirm-button");
            cancelButton = root.Q<Button>("cancel-button");
            wordViews = new MnemonicWordView[Mnemonic.Length];
            for (int i = 0; i < wordViews.Length; i++)
            {
                wordViews[i] = new MnemonicWordView(root.Q<VisualElement>($"mnemonic-word-{i}"))
                {
                    Id = $"{i + 1}.)"
                };
            }
        }

        public void Open()
        {
            mnemonic = SodiumReference<Mnemonic>.Alloc();
            mnemonic.Value = Mnemonic.FromKey(Random.Bytes<PrivateKey>());

            for (var i = 0; i < wordViews.Length; i++)
            {
                wordViews[i].Text = mnemonic.Value[i].ToString();
            }
            root.style.display = DisplayStyle.Flex;
        }

        public void Close()
        {
            root.style.display = DisplayStyle.None;
            if (mnemonic.IsCreated) mnemonic.Dispose();
            mnemonic = default;

            for (var i = 0; i < wordViews.Length; i++)
            {
                wordViews[i].Text = null;
            }
        }
    }
}
