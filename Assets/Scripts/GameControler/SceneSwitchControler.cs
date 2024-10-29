using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitchControler : MonoBehaviour
{
    public int sceneBuildingIndex;
    public bool levelComplete;

    private bool _isPlayerInTrigger = false;

    private void Awake()
    {
        levelComplete = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _isPlayerInTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _isPlayerInTrigger = false;
        }
    }

    private void Update()
    {
        if (_isPlayerInTrigger && levelComplete && Input.GetButtonDown("Interact"))
        {
            SceneManager.LoadScene(sceneBuildingIndex, LoadSceneMode.Single);
        }
    }
}