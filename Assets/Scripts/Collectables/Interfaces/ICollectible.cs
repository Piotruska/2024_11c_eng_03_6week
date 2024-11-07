using UnityEngine;

namespace Collectables.Interfaces
{
    [RequireComponent(typeof(Collider2D))]
    public abstract class ICollectible : MonoBehaviour
    {
        protected Animator _animator;
        protected string _collectTrigger = "Collect";
        protected abstract void Collect();

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                Collect();
            }
        }

        private void DespawnCollectible()
        {
            Destroy(gameObject);
        }
    }
}
