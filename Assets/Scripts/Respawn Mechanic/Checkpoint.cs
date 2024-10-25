using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private PlayerHealthScript _playerHealthScript;
    [SerializeField] private Transform _respawnPoint;
    private Collider2D _collider2D;

    private void Awake()
    {
        _playerHealthScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealthScript>();
        _collider2D = gameObject.GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _playerHealthScript.UpdateCheckpointPosition(_respawnPoint.position);
            _collider2D.enabled = false; //Disables collider so that player wont respawn in previous checkpoints if they go back
        }
    }
}
