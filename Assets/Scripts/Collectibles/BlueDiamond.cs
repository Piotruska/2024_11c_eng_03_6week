using Unity.VisualScripting;
using UnityEngine;

namespace Collectibles
{
    public class BlueDiamond : Collectible
    {
        [SerializeField] private DiamondConfig _diamondConfig;
        protected override void Collect()
        {
            Destroy(gameObject);
        }
    }
}
