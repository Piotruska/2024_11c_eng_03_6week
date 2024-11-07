using Audio;
using Collectables.Interfaces;
using Player;
using UnityEngine;

namespace Collectables
{
    public class BluePotion : ICollectible
    {
        private AudioManeger _audioManeger;
        private Collider2D _collider2D;
        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _audioManeger = GameObject.FindWithTag("AudioManager").GetComponent<AudioManeger>();
            _collider2D = GetComponent<Collider2D>();
        }
        protected override void Collect()
        {
            _audioManeger.PlayCollectableSFX(_audioManeger.potionPickup);
            _collider2D.enabled = false;
            PlayerCollectibles.IncreaseBluePotionCount(1);
            _animator.SetTrigger(_collectTrigger);
        }
    }
}
