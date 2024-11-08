using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    [Header("Menu Displays")]
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject soundSetting;

    public IMenuDisplay MainMenu => mainMenu.GetComponent<IMenuDisplay>();
    public IMenuDisplay SoundSetting => soundSetting.GetComponent<IMenuDisplay>();
    

    // private void Awake()
    // {
    //     MainMenu.ShowDisplay();
    //     SoundSetting.HideDisplay();
    // }

    private void Start()
    {
        MainMenu.ShowDisplay();
        SoundSetting.HideDisplay();
        
    }

    public void ChangeMenu(IMenuDisplay display)
    {
        display.ShowDisplay();
    }
}
