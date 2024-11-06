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
    private LevelLoader _levelLoader;

    private void Awake()
    {
        levelComplete = false;
        _levelLoader = FindObjectOfType<LevelLoader>();
    }

    public void OnInteractAction()
    {
        if (levelComplete)
        {
            _levelLoader.LoadNextLevel(sceneBuildingIndex);
        }
    }
}