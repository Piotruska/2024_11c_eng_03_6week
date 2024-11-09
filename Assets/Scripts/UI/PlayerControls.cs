using UI.MenuController;
using UnityEngine;

namespace UI
{
    public class PlayerControlsDisplay : MonoBehaviour, IMenuDisplay
    {
        private bool _displayActive;
        private bool _exitInput;
        private CanvasGroup _canvasGroup;
        private MenuController.MenuController _menuController;
        private AudioManeger _audioManeger;
        private void Awake()
        {
            _canvasGroup = gameObject.GetComponent<CanvasGroup>();
            _menuController = FindObjectOfType<MenuController.MenuController>();
            _audioManeger = GameObject.FindWithTag("AudioManager")?.GetComponent<AudioManeger>();

        }

        private void Update()
        {
            if(!_displayActive) return;
            _exitInput = Input.GetButtonDown("Cancel");
            if (_exitInput)
            {
                _audioManeger.PlayMenuSFX(_audioManeger.menuClick);
                HideDisplay();
                _menuController.ChangeMenu(_menuController.MainMenu);
            }
        }

        public void ShowDisplay()
        {
            _canvasGroup.alpha = 1;
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;
            _displayActive = true;
        }

        public void HideDisplay()
        {
            _canvasGroup.alpha = 0;
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
            _displayActive = false;
        }
    }
}
