using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMenu : MonoBehaviour, IMenuDisplay
{
    private bool _displayActive;
    private bool _exitInput;
    private CanvasGroup _canvasGroup;
    private MenuController _menuController;

    private void Awake()
    {
        _canvasGroup = gameObject.GetComponent<CanvasGroup>();
        _menuController = FindObjectOfType<MenuController>();
    }

    private void Update()
    {
        if(!_displayActive) return;
        _exitInput = Input.GetButtonDown("Cancel");
        if (_exitInput)
        {
            HideDisplay();
            _menuController.ChangeMenu(_menuController.MainMenu);
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
}
