using Player;
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
        
        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }
        
        public void OnInteractAction()
        {
            if (PlayerCollectibles.GetKeyCount() > 0 && !_isUnlocked)
            {
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
