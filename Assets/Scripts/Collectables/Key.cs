using Collectables.Interfaces;
using Player;
using UnityEngine;

namespace Collectables
{
    public class Key : ICollectible
    {
        private Collider2D _collider2D;
        private AudioManeger _audioManeger;
        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _collider2D = GetComponent<Collider2D>();
            _audioManeger = GameObject.FindWithTag("AudioManager").GetComponent<AudioManeger>();
        }
        
        protected override void Collect()
        {
            _audioManeger.PlayCollectableSFX(_audioManeger.keyGet);
            _collider2D.enabled = false;
            PlayerCollectibles.IncreaseKeyCount(1);
            _animator.SetTrigger(_collectTrigger);
        }
    }
}
