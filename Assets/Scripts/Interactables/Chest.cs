using UnityEngine;

namespace Interactables
{
    public class Chest : MonoBehaviour, IInteractable
    {
        private Animator _animator;
        private bool _isUnlocked = false;
        
        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }
        
        public void OnInteractAction()
        {
            _animator.SetTrigger("Unlock");
            _isUnlocked = true;
        }
    }
}
