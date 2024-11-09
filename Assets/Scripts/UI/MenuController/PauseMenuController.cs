using UnityEngine;

namespace UI.MenuController
{
    public class PauseMenuController : MonoBehaviour
    {
        [Header("Menu Displays")]
        [SerializeField] private GameObject pauseMenu;
        [SerializeField] private GameObject soundSetting;

        public IMenuDisplay PauseMenu => pauseMenu.GetComponent<IMenuDisplay>();
        public IMenuDisplay SoundSetting => soundSetting.GetComponent<IMenuDisplay>();

        private void Start()
        {
            PauseMenu.HideDisplay();
            SoundSetting.HideDisplay();
        }

        public void ChangeMenu(IMenuDisplay display)
        {
            display.ShowDisplay();
        }
    }
}
