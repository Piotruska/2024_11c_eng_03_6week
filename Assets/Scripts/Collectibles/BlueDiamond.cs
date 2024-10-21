using Unity.VisualScripting;
using UnityEngine;

namespace Collectibles
{
    public class BlueDiamond : ICollectible
    {
        [SerializeField] private DiamondConfig _diamondConfig;
        protected override void Collect()
        {
            Destroy(gameObject);
        }
    }
}
