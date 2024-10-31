using UnityEngine;

namespace UI
{
    public class PauseDisplay : MonoBehaviour
    {
        private Canvas _pauseMenu;
        void Awake()
        {
            _pauseMenu = GameObject.Find("PauseMenu").GetComponent<Canvas>();
            _pauseMenu.enabled = false;
        }

        public void Hide() {
            //_pauseMenu.alpha = 0f;
            //_pauseMenu.blocksRaycasts = false;
            _pauseMenu.enabled = false;
        }
        
        public void Show() {
            //_pauseMenu.alpha = 1f;
            //_pauseMenu.blocksRaycasts = true;
            _pauseMenu.enabled = true;
        }
    }
}
