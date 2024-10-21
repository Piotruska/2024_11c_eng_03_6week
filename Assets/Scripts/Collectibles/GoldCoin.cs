using System;
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
            _animator.SetTrigger(_collectTrigger);
        }

        private void RemoveCoin()
        {
            Destroy(gameObject);
        }
    }
}
