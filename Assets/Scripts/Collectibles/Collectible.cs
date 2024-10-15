using UnityEngine;

namespace Collectibles
{
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class Collectible : MonoBehaviour
    {
        protected abstract void Collect();

        private void OnTriggerEnter2D(Collider2D other)
        {
            Collect();
        }
    }
}
