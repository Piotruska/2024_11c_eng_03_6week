using Player;
using UnityEngine;

namespace Collectibles
{
    public class Key : ICollectible
    {
        private Collider2D _collider2D;
        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _collider2D = GetComponent<Collider2D>();
        }
        
        protected override void Collect()
        {
            _collider2D.enabled = false;
            PlayerCollectibles.IncreaseKeyCount(1);
            _animator.SetTrigger(_collectTrigger);
        }
    }
}
