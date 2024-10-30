using System.Collections;
using System.Collections.Generic;
using Collectibles;
using UnityEngine;

public class SwordCollectable : ICollectible
{
    [SerializeField] private PlayerController _playerController;
    private AudioManeger _audioManeger;
    private Collider2D _collider2D;


    private void Awake()
    {
        _collider2D = GetComponent<Collider2D>();
        _audioManeger = GameObject.FindWithTag("AudioManager").GetComponent<AudioManeger>();
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            _playerController = playerObject.GetComponent<PlayerController>();
        }
    }

    protected override void Collect()
    {
        _collider2D.enabled = false;
        _audioManeger.PlayCollectableSFX(_audioManeger.swordPickup);
        _playerController.HasSword(true);
        Destroy(gameObject);
    }
}
