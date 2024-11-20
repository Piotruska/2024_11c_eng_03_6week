using System.Collections.Generic;
using Interactables;
using Interactables.Interfaces;
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

        public void InteractAction()
        {
            List<Collider2D> colliders = new List<Collider2D>();
            ContactFilter2D filter = new ContactFilter2D();
            filter.SetLayerMask(_interactableLayer); 
            filter.useTriggers = true;

            _interactionCollider.OverlapCollider(filter, colliders);
        
            ApplyEffects(colliders.ToArray());
        }

        private void ApplyEffects(Collider2D[] interactables)
        {
            foreach (Collider2D collider in interactables)
            {
                IInteractable interactable = collider.gameObject.GetComponent<IInteractable>();
                
                if (interactable != null) 
                { 
                    interactable.OnInteractAction();
                }
            }
        }
    }
}
