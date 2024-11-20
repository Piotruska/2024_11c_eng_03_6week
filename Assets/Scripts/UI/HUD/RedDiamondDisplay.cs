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
            tempColor.a = 1f;
            _diamondImage.color = tempColor;
        }
        
        public void Reset()
        {
            var tempColor = _diamondImage.color;
            tempColor.a = 0.3f;
            _diamondImage.color = tempColor;
        }
    }
}
