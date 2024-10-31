using System;
using System.Collections;
using System.Collections.Generic;
using Interactables;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitchControler : MonoBehaviour,IInteractable
{
    public int sceneBuildingIndex;
    public bool levelComplete;

    private void Awake()
    {
        levelComplete = false;
    }

    public void OnInteractAction()
    {
        if (levelComplete)
        {
            SceneManager.LoadScene(sceneBuildingIndex, LoadSceneMode.Single);
        }
    }
}