using System.Collections;
using UnityEngine;

namespace GameController
{
    public class ShowKeyBind : MonoBehaviour
    {
        [SerializeField] private GameObject keyBindPrefab; 
        [SerializeField] private Transform position; 
        [SerializeField] private float fadeDuration = 0.5f; 

        private GameObject _currentKeyBind;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("InteractionHitbox") && _currentKeyBind == null)
            {
                _currentKeyBind = Instantiate(keyBindPrefab, position.position, Quaternion.identity);
                StartCoroutine(FadeIn(_currentKeyBind));
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("InteractionHitbox") && _currentKeyBind != null)
            {
                StartCoroutine(FadeOutAndDestroy(_currentKeyBind));
                _currentKeyBind = null; 
            }
        }

        public void ForceDestroy()
        {
            StartCoroutine(FadeOutAndDestroy(_currentKeyBind));
            _currentKeyBind = null;
        }

        private IEnumerator FadeIn(GameObject keyBind)
        {
            if (keyBind == null) yield break;

            SpriteRenderer spriteRenderer = keyBind.GetComponent<SpriteRenderer>();
            float elapsedTime = 0f;
            Color color = spriteRenderer.color;
            color.a = 0;
            spriteRenderer.color = color;
        
            while (elapsedTime < fadeDuration)
            {
                if (keyBind == null) yield break;
                elapsedTime += Time.deltaTime;
                color.a = Mathf.Clamp01(elapsedTime / fadeDuration);
                spriteRenderer.color = color;
                yield return null;
            }
        }

        private IEnumerator FadeOutAndDestroy(GameObject keyBind)
        {
            if (keyBind == null) yield break;

            SpriteRenderer spriteRenderer = keyBind.GetComponent<SpriteRenderer>();
            float elapsedTime = 0f;
            Color color = spriteRenderer.color;
        
            while (elapsedTime < fadeDuration)
            {
                if (keyBind == null) yield break;
                elapsedTime += Time.deltaTime;
                color.a = Mathf.Clamp01(1 - (elapsedTime / fadeDuration));
                spriteRenderer.color = color;
                yield return null;
            }

            Destroy(keyBind);
        }

        private void OnDisable()
        {
            StopAllCoroutines();
            if (_currentKeyBind != null)
            {
                Destroy(_currentKeyBind);
                _currentKeyBind = null;
            }
        }
    }
}
