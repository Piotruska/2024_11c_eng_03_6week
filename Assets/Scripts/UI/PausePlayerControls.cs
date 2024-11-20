using UI.MenuController;
using UnityEngine;
using System.Collections;

namespace UI
{
    public class PausePlayerControlsDisplay : MonoBehaviour, IMenuDisplay
    {
        private bool _displayActive;
        private CanvasGroup _canvasGroup;
        private PauseMenuController _pauseMenuController;
        private AudioManeger _audioManeger;
        
        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            _pauseMenuController = FindObjectOfType<PauseMenuController>();
            _audioManeger = GameObject.FindWithTag("AudioManager")?.GetComponent<AudioManeger>();
        }

        private void Update()
        {
            if (!_displayActive) return;

            if (Input.GetButtonDown(InputManager.PauseInput))
            {
                _audioManeger.PlayMenuSFX(_audioManeger.menuClick);
                HideDisplay();
                _pauseMenuController.ChangeMenu(_pauseMenuController.PauseMenu);
            }
        }

        public void ShowDisplay()
        {
            StartCoroutine(FadeCanvasGroup(_canvasGroup, 1f, true));
        }

        public void HideDisplay()
        {
            StartCoroutine(FadeCanvasGroup(_canvasGroup, 0f, false));
        }

        private IEnumerator FadeCanvasGroup(CanvasGroup canvasGroup, float endValue, bool setActive)
        {
            float startValue = canvasGroup.alpha;
            float elapsed = 0f;
            const float duration = 0.2f;

            while (elapsed < duration)
            {
                canvasGroup.alpha = Mathf.Lerp(startValue, endValue, elapsed / duration);
                elapsed += Time.unscaledDeltaTime;
                yield return null;
            }

            canvasGroup.alpha = endValue;
            _displayActive = setActive;
            canvasGroup.interactable = setActive;
            canvasGroup.blocksRaycasts = setActive;
        }
    }
}