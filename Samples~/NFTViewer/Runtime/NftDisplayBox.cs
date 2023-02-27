using UnityEngine;
using UnityEngine.UI;

namespace Algorand.Unity.Samples.NftViewer
{
    public class NftDisplayBox : MonoBehaviour
    {
        public Image image;
        public Text nameText;
        public Text idText;

        public void SetFields(Texture texture, string name, string id)
        {
            nameText.text = name;
            idText.text = id;
            image.sprite = Sprite.Create(
                (Texture2D)texture,
                new Rect(0, 0, texture.width, texture.height),
                Vector2.zero);
        }
    }
}
