using Audio;
using Collectables.Configurations;
using Collectables.Interfaces;
using Player;
using UnityEngine;

namespace Collectables
{
    public class GoldCoin : ICollectible
    {
        private AudioManeger _audioManeger;
        
        [SerializeField] private CoinConfig _coinConfig;
        private Collider2D _collider2D;

        private void Awake()
        {
            _audioManeger = GameObject.FindWithTag("AudioManager").GetComponent<AudioManeger>();
            _animator = GetComponent<Animator>();
            _collider2D = GetComponent<Collider2D>();
        }

        protected override void Collect()
        {
            _audioManeger.PlayCollectableSFX(_audioManeger.coidPickup);
            _collider2D.enabled = false;
            PlayerCollectibles.IncreaseCoinCount(_coinConfig.goldValue);
            _animator.SetTrigger(_collectTrigger);
        }
    }
}
