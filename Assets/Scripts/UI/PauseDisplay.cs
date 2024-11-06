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
            _pauseMenu.enabled = false;
        }
        
        public void Show() {
            _pauseMenu.enabled = true;
        }
    }
}
