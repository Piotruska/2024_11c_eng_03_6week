using System.Collections;
using System.Collections.Generic;
using Collectibles;
using UnityEngine;

public class SwordCollectable : ICollectible
{
    [SerializeField] private PlayerController _playerController;

    private void Awake()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            _playerController = playerObject.GetComponent<PlayerController>();
        }
    }

    protected override void Collect()
    {
        _playerController.HasSword(true);
        Destroy(gameObject);
    }
}
