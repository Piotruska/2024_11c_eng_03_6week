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
        private AudioManeger _audioManeger;
        private Collider2D _collider2D;

        
        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _blueDiamondDisplay = FindObjectOfType<BlueDiamondDisplay>();
            _audioManeger = GameObject.FindWithTag("AudioManager").GetComponent<AudioManeger>();
            _collider2D = GetComponent<Collider2D>();
        }
        
        protected override void Collect()
        {
            _audioManeger.PlayCollectableSFX(_audioManeger.diamondPickup);
            _collider2D.enabled = false;
            _blueDiamondDisplay.SetCollected();
            PlayerCollectibles.GetDiamond1();
            _animator.SetTrigger(_collectTrigger);
        }
    }
}
