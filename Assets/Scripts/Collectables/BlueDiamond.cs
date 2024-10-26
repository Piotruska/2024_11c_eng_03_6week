using Collectibles;
using Player;
using UI;
using UnityEngine;

namespace Collectables
{
    public class BlueDiamond : ICollectible
    {
        [SerializeField] private DiamondConfig _diamondConfig;
        private BlueDiamondDisplay _blueDiamondDisplay;
        
        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _blueDiamondDisplay = FindObjectOfType<BlueDiamondDisplay>();
        }
        
        protected override void Collect()
        {
            _blueDiamondDisplay.SetCollected();
            PlayerCollectibles.GetDiamond1();
            _animator.SetTrigger(_collectTrigger);
        }
    }
}
