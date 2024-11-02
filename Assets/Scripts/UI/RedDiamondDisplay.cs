using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class RedDiamondDisplay : MonoBehaviour
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
        
        public void Reset()
        {
            var tempColor = _diamondImage.color;
            tempColor.a = 75;
            _diamondImage.color = tempColor;
        }
    }
}
