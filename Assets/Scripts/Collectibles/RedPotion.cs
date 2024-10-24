using UnityEngine;

namespace Collectibles
{
    public class RedPotion : ICollectible
    {
        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }
        protected override void Collect()
        {
            _animator.SetTrigger(_collectTrigger);
        }
    }
}
