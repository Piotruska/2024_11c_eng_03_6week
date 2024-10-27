using Player;
using UnityEngine;

namespace Interactables
{
    public class ChestController : MonoBehaviour, IInteractable
    {
        private Animator _animator;
        private bool _isUnlocked = false;
        
        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }
        
        public void OnInteractAction()
        {
            if (PlayerCollectibles.GetKeyCount() > 0)
            {
                PlayerCollectibles.DecreaseKeyCount(1);
                _animator.SetTrigger("Unlock");
                _isUnlocked = true;
            }
        }
    }
}
