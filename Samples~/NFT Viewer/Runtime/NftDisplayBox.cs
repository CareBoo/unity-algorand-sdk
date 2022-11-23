using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Algorand.Unity.Samples.NftViewer
{
    public class NftDisplayBox : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private TextMeshProUGUI nameText, idText;

        public void SetFields(Texture texture, string name, string id)
        {
            nameText.text = name;
            idText.text = id;

            Sprite sprite = Sprite.Create((Texture2D)texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);

            image.sprite = sprite;
        }
    }
}
