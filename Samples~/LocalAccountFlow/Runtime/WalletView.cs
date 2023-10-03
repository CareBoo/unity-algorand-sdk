using System.Collections.Generic;
using UnityEngine.UIElements;

namespace Algorand.Unity.Samples.LocalAccountFlow
{
    public class WalletView
    {
        public readonly VisualElement root;
        public readonly Button newAccountButton;
        public readonly Button importAccountButton;
        public readonly ListView accountList;
        public readonly List<Address> walletAddresses = new();

        public WalletView(VisualElement root)
        {
            this.root = root;
            newAccountButton = root.Q<Button>("new-account-button");
            importAccountButton = root.Q<Button>("import-account-button");
            accountList = root.Q<ListView>("account-list");

            accountList.itemsSource = walletAddresses;
            accountList.makeItem = () => new Label();
            accountList.bindItem = (element, i) =>
            {
                var address = walletAddresses[i];
                var label = (Label)element;
                label.text = address.ToString();
            };
        }

        public void Open(LocalAccountStore accountStore)
        {
            root.style.display = DisplayStyle.Flex;

            for (var i = 0; i < accountStore.Length; i++)
            {
                var account = accountStore[i];
                walletAddresses.Add(account.Address);
            }
            accountList.Rebuild();
        }

        public void Close()
        {
            root.style.display = DisplayStyle.None;
            walletAddresses.Clear();
        }
    }
}
