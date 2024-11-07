using Audio;
using Collectables.Configurations;
using Collectables.Interfaces;
using Player;
using UI;
using UnityEngine;

namespace Collectables
{
    public class GreenDiamond : ICollectible
    {
        [SerializeField] private DiamondConfig _diamondConfig;
        private GreenDiamondDisplay _greenDiamondDisplay;
        private AudioManeger _audioManeger;
        private Collider2D _collider2D;

        
        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _greenDiamondDisplay = FindObjectOfType<GreenDiamondDisplay>();
            _audioManeger = GameObject.FindWithTag("AudioManager").GetComponent<AudioManeger>();
            _collider2D = GetComponent<Collider2D>();
        }
        
        protected override void Collect()
        {
            _audioManeger.PlayCollectableSFX(_audioManeger.diamondPickup);
            _collider2D.enabled = false;
            _greenDiamondDisplay.SetCollected();
            PlayerCollectibles.GetDiamond3();
            _animator.SetTrigger(_collectTrigger);
        }
    }
}
