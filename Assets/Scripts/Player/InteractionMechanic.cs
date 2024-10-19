using Player.Interfaces;
using UnityEngine;

namespace Player
{
    public class InteractionMechanic : MonoBehaviour, ICanInteract
    {
        [Header("Interaction Range Config")]
        [SerializeField] private BoxCollider2D _interactionCollider;
        [Header("Interactable Identification")] 
        [SerializeField] public LayerMask _interactableLayer;
        
        public void InteractStorage()
        {
        }
    }
}
