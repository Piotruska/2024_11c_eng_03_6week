using System.Collections;
using UnityEngine;

namespace UI.MenuController
{
    public class PauseMenuController : MonoBehaviour
    {
        [Header("Menu Displays")]
        [SerializeField] private GameObject pauseMenu;
        [SerializeField] private GameObject soundSetting;
        [SerializeField] private GameObject playerControls;
        [SerializeField] private GameObject darkPannel;

        public IMenuDisplay PauseMenu => pauseMenu.GetComponent<IMenuDisplay>();
        public IMenuDisplay SoundSetting => soundSetting.GetComponent<IMenuDisplay>();
        public IMenuDisplay PlayerControls => playerControls.GetComponent<IMenuDisplay>();  
        public CanvasGroup DarkPannel => darkPannel.GetComponent<CanvasGroup>();

        private AudioManeger _audioManeger;

        
        private bool _isPaused = false;
        private bool _pauseInput;

        private void Start()
        {
            PauseMenu.HideDisplay();
            SoundSetting.HideDisplay();
            PlayerControls.HideDisplay();
            DarkPannel.alpha = 0;
            _audioManeger = GameObject.FindWithTag("AudioManager")?.GetComponent<AudioManeger>();

        }

        private void Update()
        {
            _pauseInput = Input.GetButtonDown(InputManager.PauseInput);

            if (_pauseInput && !_isPaused)
            {
                _audioManeger.PlayMenuSFX(_audioManeger.menuClick);
                StartCoroutine(TogglePause(true));
            }
        }

        private IEnumerator TogglePause(bool isPausing)
        {
            if (isPausing)
            {
                InputManager.PlayerDisable();
                InputManager.MenuEnable();
                PauseMenu.ShowDisplay();
                DarkPannel.alpha = 1;
                _isPaused = true;
                Time.timeScale = 0f;
            }
            else
            {
                Time.timeScale = 1f;
                yield return new WaitForSecondsRealtime(0.1f);
                InputManager.MenuDisable();
                InputManager.PlayerEnable();
                PauseMenu.HideDisplay();
                DarkPannel.alpha = 0;
                _isPaused = false;
            }
        }

        public void DeactivatePause()
        {
            StartCoroutine(TogglePause(false));
        }

        public void ChangeMenu(IMenuDisplay display)
        {
            PauseMenu.HideDisplay();
            SoundSetting.HideDisplay();
            PlayerControls.HideDisplay();
            display.ShowDisplay();
        }
    }
}
