using Player;
using UnityEngine;

namespace Collectibles
{
    public class BluePotion : ICollectible
    {
        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }
        protected override void Collect()
        {
            PlayerCollectibles.IncreaseBluePotionCount(1);
            _animator.SetTrigger(_collectTrigger);
        }
    }
}
