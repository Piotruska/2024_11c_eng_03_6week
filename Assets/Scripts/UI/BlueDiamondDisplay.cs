using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class BlueDiamondDisplay : MonoBehaviour
    {
        private Image _diamondImage;
        private void Awake()
        {
            _diamondImage = gameObject.GetComponent<Image>();
        }

        public void SetCollected()
        {
            var tempColor = _diamondImage.color;
            tempColor.a = 255;
            _diamondImage.color = tempColor;
        }
    }
}
