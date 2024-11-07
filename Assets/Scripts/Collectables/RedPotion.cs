using Audio;
using Collectables.Interfaces;
using Player;
using UnityEngine;

namespace Collectables
{
    public class RedPotion : ICollectible
    {
        private AudioManeger _audioManeger;
        private Collider2D _collider2D;

        private void Awake()
        {
            _audioManeger = GameObject.FindWithTag("AudioManager").GetComponent<AudioManeger>();
            _animator = GetComponent<Animator>();
            _collider2D = GetComponent<Collider2D>();
        }
        protected override void Collect()
        {
            _collider2D.enabled = false;
            _audioManeger.PlayCollectableSFX(_audioManeger.potionPickup);
            PlayerCollectibles.IncreaseRedPotionCount(1);
            _animator.SetTrigger(_collectTrigger);
        }
    }
}
