using UnityEngine;

namespace UI.MenuController
{
    public class PauseMenuController : MonoBehaviour
    {
        [Header("Menu Displays")]
        [SerializeField] private GameObject pauseMenu;
        [SerializeField] private GameObject soundSetting;
        [SerializeField] private GameObject playerControls;

        public IMenuDisplay PauseMenu => pauseMenu.GetComponent<IMenuDisplay>();
        public IMenuDisplay SoundSetting => soundSetting.GetComponent<IMenuDisplay>();
        public IMenuDisplay PlayerControls => playerControls.GetComponent<IMenuDisplay>();

        private void Start()
        {
            PauseMenu.HideDisplay();
            SoundSetting.HideDisplay();
            PlayerControls.HideDisplay();
        }

        public void ChangeMenu(IMenuDisplay display)
        {
            display.ShowDisplay();
        }
    }
}
