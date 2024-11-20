using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class BlackScreen : MonoBehaviour
    {
        private Image _blackScreen;
        private void Awake()
        {
            _blackScreen = GetComponent<Image>();
        }
        
        public IEnumerator FadeIn(float duration)
        {
            if (_blackScreen == null) yield break;

            float elapsedTime = 0f;
            Color color = _blackScreen.color;
            color.a = 0;
            _blackScreen.color = color;
        
            while (elapsedTime < duration)
            {
                if (_blackScreen == null) yield break;
                elapsedTime += Time.deltaTime;
                color.a = Mathf.Clamp01(elapsedTime / duration);
                _blackScreen.color = color;
                yield return null;
            }
        }
        
        public IEnumerator FadeOut(float duration)
        {
            if (_blackScreen == null) yield break;

            float elapsedTime = 0f;
            Color color = _blackScreen.color;
        
            while (elapsedTime < duration)
            {
                if (_blackScreen == null) yield break;
                elapsedTime += Time.deltaTime;
                color.a = Mathf.Clamp01(1 - (elapsedTime / duration));
                _blackScreen.color = color;
                yield return null;
            }
        }
    }
}
