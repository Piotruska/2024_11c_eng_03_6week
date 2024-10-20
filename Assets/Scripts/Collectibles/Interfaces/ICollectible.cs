using UnityEngine;

namespace Collectibles
{
    [RequireComponent(typeof(Collider2D))]
    public abstract class ICollectible : MonoBehaviour
    {
        protected abstract void Collect();

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                Collect();
            }
        }
    }
}
