using System;
using Collectibles.Configurations;
using Player;
using Unity.VisualScripting;
using UnityEngine;

namespace Collectibles
{
    public class GoldCoin : ICollectible
    {
        [SerializeField] private CoinConfig _coinConfig;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        protected override void Collect()
        {
            PlayerCollectibles.IncreaseCoinCount(_coinConfig.goldValue);
            _animator.SetTrigger(_collectTrigger);
        }
    }
}
