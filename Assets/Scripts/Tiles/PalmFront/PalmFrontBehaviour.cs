using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PalmFrontBehaviour : MonoBehaviour
{
    private PlayerController _playerController;
    private PlatformEffector2D _platformEffector;

    private void Awake()
    {
        
        _platformEffector = GetComponent<PlatformEffector2D>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Player"))
        {
            _playerController = other.gameObject.GetComponent<PlayerController>();
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (_playerController == null)
        {
            return;
        }

        if (_playerController._fallThrough)
        {
            _platformEffector.rotationalOffset = 180;
            _playerController = null;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        _playerController = null;
        _platformEffector.rotationalOffset = 0;
    }
}
