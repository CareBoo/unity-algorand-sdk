using UnityEngine.UIElements;

namespace AlgoSdk.Editor
{
    public static class VisualElementExtensions
    {
        public static void SetDisplayed(this VisualElement visualElement, bool isDisplayed)
        {
            if (visualElement == null)
                return;
            visualElement.style.display = isDisplayed ? DisplayStyle.Flex : DisplayStyle.None;
        }
    }
}
