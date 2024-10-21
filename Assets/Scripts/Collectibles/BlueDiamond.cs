using Unity.VisualScripting;
using UnityEngine;

namespace Collectibles
{
    public class BlueDiamond : ICollectible
    {
        [SerializeField] private DiamondConfig _diamondConfig;
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
        
        private void BlueDiamond_Despawn()
        {
            Destroy(gameObject);
        }
    }
}
