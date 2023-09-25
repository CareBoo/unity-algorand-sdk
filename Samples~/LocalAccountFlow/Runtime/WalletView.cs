using UnityEngine.UIElements;

namespace Algorand.Unity.Samples.LocalAccountFlow
{
    public class WalletView
    {
        public readonly VisualElement root;
        public readonly Button newAccountButton;
        public readonly Button importAccountButton;
        public readonly ListView accountList;

        public WalletView(VisualElement root)
        {
            this.root = root;
            newAccountButton = root.Q<Button>("new-account-button");
            importAccountButton = root.Q<Button>("import-account-button");
            accountList = root.Q<ListView>("account-list");
        }

        public void Open(LocalAccountStore accountStore)
        {
            root.style.display = DisplayStyle.Flex;

            for (var i = 0; i < accountStore.Length; i++)
            {
                var account = accountStore[i];
                accountList.Add(new Label(account.Address.ToString()));
            }
        }

        public void Close()
        {
            root.style.display = DisplayStyle.None;
            accountList.Clear();
        }
    }
}
