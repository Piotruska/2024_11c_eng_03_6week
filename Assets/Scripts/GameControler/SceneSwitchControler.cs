using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitchControler : MonoBehaviour
{
    public int sceneBuildingIndex;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (Input.GetButtonDown("Interact"))
            {
                SceneManager.LoadScene(sceneBuildingIndex, LoadSceneMode.Single);
            }
            
        }
    }
}
