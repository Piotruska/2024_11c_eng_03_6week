using System.Collections;
using UnityEngine;

namespace UI
{
    public class GameTitleDispaly : MonoBehaviour
    {
        private CanvasGroup _gameTitle;
        private float alpha;
    
        private void Awake()
        {
            _gameTitle = GetComponent<CanvasGroup>();
        }
        
        public IEnumerator FadeIn(float duration)
        {
            if (_gameTitle == null) yield break;

            float elapsedTime = 0f;
            alpha = 0;
            _gameTitle.alpha = alpha;
        
            while (elapsedTime < duration)
            {
                if (_gameTitle == null) yield break;
                elapsedTime += Time.deltaTime;
                alpha = Mathf.Clamp01(elapsedTime / duration);
                _gameTitle.alpha = alpha;
                yield return null;
            }
        }
        
        public IEnumerator FadeOut(float duration)
        {
            if (_gameTitle == null) yield break;

            float elapsedTime = 0f;
            alpha = _gameTitle.alpha;
        
            while (elapsedTime < duration)
            {
                if (_gameTitle == null) yield break;
                elapsedTime += Time.deltaTime;
                alpha = Mathf.Clamp01(1 - (elapsedTime / duration));
                _gameTitle.alpha = alpha;
                yield return null;
            }
        }
    }
}
