using Player;
using UnityEngine;

namespace Collectibles
{
    public class Key : ICollectible
    {
        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }
        
        protected override void Collect()
        {
            PlayerCollectibles.IncreaseKeyCount(1);
            _animator.SetTrigger(_collectTrigger);
        }
    }
}
