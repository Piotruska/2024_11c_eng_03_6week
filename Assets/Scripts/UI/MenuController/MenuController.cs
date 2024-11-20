using UnityEngine;

namespace UI.MenuController
{
    public class MenuController : MonoBehaviour
    {
        [Header("Menu Displays")]
        [SerializeField] private GameObject mainMenu;
        [SerializeField] private GameObject soundSetting;
        [SerializeField] private GameObject playerControls;

        public IMenuDisplay MainMenu => mainMenu.GetComponent<IMenuDisplay>();
        public IMenuDisplay SoundSetting => soundSetting.GetComponent<IMenuDisplay>();
        public IMenuDisplay PlayerControls => playerControls.GetComponent<IMenuDisplay>();

        private void Start()
        {
            MainMenu.ShowDisplay();
            SoundSetting.HideDisplay();
            PlayerControls.HideDisplay();
        }

        public void ChangeMenu(IMenuDisplay display)
        {
            display.ShowDisplay();
        }
    }
}
