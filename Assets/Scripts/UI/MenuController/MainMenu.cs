using Level_Control;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



namespace UI
{
    public class MainMenu : MonoBehaviour, IMenuDisplay
    {
        private Image _selectionPanel1;
        private Image _selectionPanel2;
        private Image _selectionPanel3;
        private Image _selectionPanel4;
        // Inputs
        private float _yAxisInput;
        private bool _yInput;
        private bool _confirmInput;
        
        private int _currentSelection;
        private LevelLoader _levelLoader;

        private MenuController _menuController;
        private bool _displayActive;
        private CanvasGroup _canvasGroup;
        private AudioManeger _audioManeger;
        private void Awake()
        {
            _selectionPanel1 = GameObject.Find("SelectionPanel1").GetComponent<Image>();
            _selectionPanel2 = GameObject.Find("SelectionPanel2").GetComponent<Image>();
            _selectionPanel3 = GameObject.Find("SelectionPanel3").GetComponent<Image>();
            _selectionPanel4 = GameObject.Find("SelectionPanel4").GetComponent<Image>();
            _menuController = FindObjectOfType<MenuController>();
            _levelLoader = FindObjectOfType<LevelLoader>();
            _canvasGroup = gameObject.GetComponent<CanvasGroup>();
            _audioManeger = GameObject.FindWithTag("AudioManager")?.GetComponent<AudioManeger>();
        
            SetVisible(_selectionPanel1, true);
            SetVisible(_selectionPanel2, false);
            SetVisible(_selectionPanel3, false);
            SetVisible(_selectionPanel4, false);

            _currentSelection = 0;
        }

        private void Update()
        {
            if(!_displayActive) return;
            _yAxisInput = Input.GetAxis("Vertical");
            _yInput = Input.GetButtonDown("Vertical");
            _confirmInput = Input.GetButtonDown("Confirm");

            if (_yInput && _yAxisInput > 0)
            {
                _audioManeger.PlayMenuSFX(_audioManeger.menuClick);
                if (_currentSelection == 0)
                {
                    _currentSelection = 3;
                }
                else
                {
                    _currentSelection--;
                }
            }
        
            if (_yInput && _yAxisInput < 0)
            {
                _audioManeger.PlayMenuSFX(_audioManeger.menuClick);
                if (_currentSelection == 3)
                {
                    _currentSelection = 0;
                }
                else
                {
                    _currentSelection++;
                }
            }

            switch (_currentSelection)
            {
                case 0: //new game
                    SetVisible(_selectionPanel1, true);
                    SetVisible(_selectionPanel2, false);
                    SetVisible(_selectionPanel3, false);
                    SetVisible(_selectionPanel4, false);
                    if (_confirmInput)
                    {
                        _audioManeger.PlayMenuSFX(_audioManeger.menuClick);
                        _levelLoader.LoadNextLevel(1);
                    }
                    break;
                case 1: // settings
                    SetVisible(_selectionPanel1, false);
                    SetVisible(_selectionPanel2, true);
                    SetVisible(_selectionPanel3, false);
                    SetVisible(_selectionPanel4, false);
                    if (_confirmInput)
                    {
                        _audioManeger.PlayMenuSFX(_audioManeger.menuClick);
                        HideDisplay();
                        _menuController.ChangeMenu(_menuController.SoundSetting);
                    }
                    break;
                case 2:
                    SetVisible(_selectionPanel1, false);
                    SetVisible(_selectionPanel2, false);
                    SetVisible(_selectionPanel3, true);
                    SetVisible(_selectionPanel4, false);
                    break;
                case 3:
                    SetVisible(_selectionPanel1, false);
                    SetVisible(_selectionPanel2, false);
                    SetVisible(_selectionPanel3, false);
                    SetVisible(_selectionPanel4, true);
                    if (_confirmInput)
                    {
                        _audioManeger.PlayMenuSFX(_audioManeger.menuClick);
                        ExitGame();
                    }
                    break;
            } 
        }

        private void SetVisible(Image image, bool visible)
        {
            if (visible)
            {
                var tempColor = image.color;
                tempColor.a = 0.6f;
                image.color = tempColor;
            }
            else
            {
                var tempColor = image.color;
                tempColor.a = 0.2f;
                image.color = tempColor;
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
        
        public void ExitGame()
        {
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif
        }       
    }
}
