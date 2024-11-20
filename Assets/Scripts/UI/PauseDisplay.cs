using System.Collections;
using Level_Control;
using Player;
using UI.MenuController;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class PauseDisplay : MonoBehaviour, IMenuDisplay
    {
        private CanvasGroup _canvasGroup;
        private Image _selectionPanel1;
        private Image _selectionPanel2;
        private Image _selectionPanel3;
        private LevelLoader _levelLoader;


        private bool _displayActive;
        private bool _canExitPause;  
        private int _currentSelection;

        private AudioManeger _audioManeger;
        private PauseMenuController _pauseMenuController;
        private GameObject _player;
        private PlayerController _playerController;


        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            _selectionPanel1 = GameObject.Find("SelectionPanel1").GetComponent<Image>();
            _selectionPanel2 = GameObject.Find("SelectionPanel2").GetComponent<Image>();
            _selectionPanel3 = GameObject.Find("SelectionPanel3").GetComponent<Image>();

            _audioManeger = GameObject.FindWithTag("AudioManager")?.GetComponent<AudioManeger>();
            _pauseMenuController = FindObjectOfType<PauseMenuController>();

            SetVisible(_selectionPanel1, true);
            SetVisible(_selectionPanel2, false);
            SetVisible(_selectionPanel3, false);
            _currentSelection = 0;
            _player = GameObject.Find("Player");
            _playerController = _player.GetComponent<PlayerController>();
            _levelLoader = FindObjectOfType<LevelLoader>();

        }

        private void Update()
        {
            if (!_displayActive) return;

            if (_canExitPause && Input.GetButtonDown(InputManager.PauseInput))
            {
                _audioManeger.PlayMenuSFX(_audioManeger.menuClick);
                _pauseMenuController.DeactivatePause();
                return;
            }

            if (Input.GetButtonDown(InputManager.YMenuNavigation))
            {
                float yAxisInput = Input.GetAxisRaw(InputManager.YMenuNavigation);

                if (yAxisInput > 0)
                {
                    _currentSelection = Mathf.Max(0, _currentSelection - 1); 
                }
                else if (yAxisInput < 0)
                {
                    _currentSelection = Mathf.Min(2, _currentSelection + 1); 
                }

                _audioManeger.PlayMenuSFX(_audioManeger.menuClick);
                UpdateSelectionPanels();
            }

            if (Input.GetButtonDown(InputManager.Confirm))
            {
                _audioManeger.PlayMenuSFX(_audioManeger.menuClick);
                HideDisplay();

                if (_currentSelection == 0)
                {
                    _pauseMenuController.ChangeMenu(_pauseMenuController.SoundSetting);
                }
                else if (_currentSelection == 1)
                {
                    _pauseMenuController.ChangeMenu(_pauseMenuController.PlayerControls);
                }else if (_currentSelection == 2)
                {
                    Time.timeScale = 1f;
                    _playerController.FullGameReset();
                    InputManager.PlayerDisable();
                    InputManager.MenuEnable();
                    _levelLoader.LoadNextLevel(0);
                }
            }   
        }

        private void UpdateSelectionPanels()
        {
            SetVisible(_selectionPanel1, _currentSelection == 0);
            SetVisible(_selectionPanel2, _currentSelection == 1);
            SetVisible(_selectionPanel3, _currentSelection == 2);
        }

        private void SetVisible(Image image, bool visible)
        {
            var tempColor = image.color;
            tempColor.a = visible ? 0.6f : 0.2f;
            image.color = tempColor;
        }

        public void ShowDisplay()
        {
            StartCoroutine(FadeCanvasGroup(_canvasGroup, 1f, true));
            StartCoroutine(EnableExitAfterDelay()); 
        }

        public void HideDisplay()
        {
            _canExitPause = false;
            StartCoroutine(FadeCanvasGroup(_canvasGroup, 0f, false));
        }

        private IEnumerator EnableExitAfterDelay()
        {
            yield return new WaitForSecondsRealtime(0.2f);
            _canExitPause = true;
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

            if (setActive)
            {
                UpdateSelectionPanels();
            }
        }
    }
}
