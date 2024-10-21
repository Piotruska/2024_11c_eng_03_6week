using UnityEngine;

namespace Collectibles
{
    public class Key : ICollectible
    {
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
        
        private void Key_Despawn()
        {
            Destroy(gameObject);
        }
    }
}
