using UI.MenuController;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PauseDisplay : MonoBehaviour, IMenuDisplay
    {
        //private CanvasGroup _canvasGroup;
        private Canvas _pauseMenu;
        
        private Image _selectionPanel1;
        private Image _selectionPanel2;
        // Inputs
        private float _yAxisInput;
        private bool _yInput;
        private bool _confirmInput;
        
        private bool _displayActive;
        
        private int _currentSelection;
        
        private AudioManeger _audioManeger;
        
        private PauseMenuController _pauseMenuController;
        void Awake()
        {
            //_canvasGroup = gameObject.GetComponent<CanvasGroup>();
            _pauseMenu = GameObject.Find("PauseMenu").GetComponent<Canvas>();
            _selectionPanel1 = GameObject.Find("SelectionPanel1").GetComponent<Image>();
            _selectionPanel2 = GameObject.Find("SelectionPanel2").GetComponent<Image>();
            
            _audioManeger = GameObject.FindWithTag("AudioManager")?.GetComponent<AudioManeger>();
            _pauseMenuController = gameObject.GetComponent<PauseMenuController>();
            
            SetVisible(_selectionPanel1, true);
            SetVisible(_selectionPanel2, false);
            
            _currentSelection = 0;
            
            _pauseMenu.enabled = false;
            _pauseMenuController.SoundSetting.HideDisplay();
        }

        private void Update()
        {
            _yAxisInput = Input.GetAxis(InputManager.YMenuNavigation);
            _yInput = Input.GetButtonDown(InputManager.YMenuNavigation);
            _confirmInput = Input.GetButtonDown(InputManager.Confirm);
            
            if (_yInput && _yAxisInput > 0)
            {
                _audioManeger.PlayMenuSFX(_audioManeger.menuClick);
                if (_currentSelection == 0)
                {
                    _currentSelection = 1;
                }
                else
                {
                    _currentSelection--;
                }
            }
            
            if (_yInput && _yAxisInput < 0)
            {
                _audioManeger.PlayMenuSFX(_audioManeger.menuClick);
                if (_currentSelection == 1)
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
                case 0: // Settings
                    SetVisible(_selectionPanel1, true);
                    SetVisible(_selectionPanel2, false);
                    if (_confirmInput)
                    {
                        _audioManeger.PlayMenuSFX(_audioManeger.menuClick);
                        HideDisplay();
                        _pauseMenuController.ChangeMenu(_pauseMenuController.SoundSetting);
                    }
                    break;
                case 1: // Controls
                    SetVisible(_selectionPanel1, false);
                    SetVisible(_selectionPanel2, true);
                    if (_confirmInput)
                    {
                        _audioManeger.PlayMenuSFX(_audioManeger.menuClick);
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
            _pauseMenu.enabled = true;
            /*_canvas.alpha = 1;
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;
            _displayActive = true;*/
        }

        public void HideDisplay()
        {
            _pauseMenu.enabled = false;
            /*_canvasGroup.alpha = 0;
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
            _displayActive = false;*/
        }
    }
}
