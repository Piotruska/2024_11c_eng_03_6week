using System;
using Player;
using Unity.VisualScripting;
using UnityEngine;

namespace Collectibles
{
    public class GoldCoin : ICollectible
    {
        [SerializeField] private CoinConfig _coinConfig;
        private Animator _animator;
        private string _collectTrigger = "Collect";

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        protected override void Collect()
        {
            PlayerCollectibles.IncreaseCoinCount(_coinConfig.goldValue);
            _animator.SetTrigger(_collectTrigger);
            Debug.Log("Count" + PlayerCollectibles.GetCoinCount());
        }

        private void RemoveCoin()
        {
            Destroy(gameObject);
        }
    }
}
