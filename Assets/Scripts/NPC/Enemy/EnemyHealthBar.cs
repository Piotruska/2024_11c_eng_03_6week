using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace NPC.Enemy
{
    public class EnemyHealthBar : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private float displayDuration = 2f; 
        [SerializeField] private float fadeDuration = 0.5f; 
        private Coroutine fadeCoroutine;

        private void Awake()
        {
            if (_canvasGroup == null)
            {
                _canvasGroup = _slider.GetComponentInParent<CanvasGroup>();
            }
            _canvasGroup.alpha = 0f;
        }

        public void UpdateHealthBar(float currentValue, float maxValue)
        {
            _slider.value = currentValue / maxValue;

            if (fadeCoroutine != null)
            {
                StopCoroutine(fadeCoroutine);
            }
            fadeCoroutine = StartCoroutine(FadeInThenOut());
        }

        private IEnumerator FadeInThenOut()
        {
            yield return StartCoroutine(Fade(1f, fadeDuration));
        
            yield return new WaitForSeconds(displayDuration);
        
            yield return StartCoroutine(Fade(0f, fadeDuration));
        }

        private IEnumerator Fade(float targetAlpha, float duration)
        {
            float startAlpha = _canvasGroup.alpha;
            float elapsed = 0f;

            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                _canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsed / duration);
                yield return null;
            }

            _canvasGroup.alpha = targetAlpha;
        }
    }
}