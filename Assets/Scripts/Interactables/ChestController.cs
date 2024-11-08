using System;
using System.Text;
using GameController;
using Interactables.Interfaces;
using Player;
using Unity.VisualScripting;
using UnityEngine;

namespace Interactables
{
    public class ChestController : MonoBehaviour, IInteractable
    {
        private Animator _animator;
        private bool _isUnlocked = false;
        [Header("Item to spawn")]
        [SerializeField]
        private GameObject _item;
        [SerializeField]
        private Transform _itemSpawnPoint;
        private AudioManeger _audioManeger;
        private ShowKeyBind _showKeyBind;
        private Collider2D _collider2D;
        
        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void Start()
        {
            _audioManeger = GameObject.FindWithTag("AudioManager").GetComponent<AudioManeger>();
            _showKeyBind = GetComponent<ShowKeyBind>();
            _collider2D = GetComponent<Collider2D>();
        }

        public void OnInteractAction()
        {
            if (PlayerCollectibles.GetKeyCount() > 0 && !_isUnlocked)
            {
                _audioManeger.PlayInteractableSFX(_audioManeger.chestOpen);
                _collider2D.enabled = false;
                Destroy(_showKeyBind._currentKeyBind);
                PlayerCollectibles.DecreaseKeyCount(1);
                _animator.SetTrigger("Unlock");
                _isUnlocked = true;
            }
        }

        private void SpawnItem()
        {
            Instantiate(_item, _itemSpawnPoint.position, _itemSpawnPoint.rotation);
        }
    }
}
