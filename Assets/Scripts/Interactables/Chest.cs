using UnityEngine;

namespace Interactables
{
    public class Chest : MonoBehaviour, IInteractable
    {
        private bool _isUnlocked = false;
        
        public void OnInteractAction()
        {
            _isUnlocked = true;
            // Play animation and get some items
        }
    }
}
