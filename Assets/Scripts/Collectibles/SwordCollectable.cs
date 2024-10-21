using System.Collections;
using System.Collections.Generic;
using Collectibles;
using UnityEngine;

public class SwordCollectable : ICollectible
{
    [SerializeField] private PlayerController _playerController;
    protected override void Collect()
    {
        _playerController.HasSword(true);
        Destroy(gameObject);
    }
}
