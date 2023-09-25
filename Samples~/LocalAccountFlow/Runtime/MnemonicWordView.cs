using UnityEngine.UIElements;

namespace Algorand.Unity.Samples.LocalAccountFlow
{
    public class MnemonicWordView
    {
        private readonly Label wordIdLabel;
        private readonly TextField wordTextField;

        public string Id
        {
            get => wordIdLabel.text;
            set => wordIdLabel.text = value;
        }

        public string Text
        {
            get => wordTextField.value;
            set => wordTextField.value = value;
        }

        public MnemonicWordView(VisualElement mnemonicWord)
        {
            wordIdLabel = mnemonicWord.Q<Label>("word-id");
            wordTextField = mnemonicWord.Q<TextField>();
        }
    }
}
