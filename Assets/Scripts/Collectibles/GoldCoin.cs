using Unity.VisualScripting;
using UnityEngine;

namespace Collectibles
{
    public class GoldCoin : ICollectible
    {
        [SerializeField] private CoinConfig _coinConfig;
        protected override void Collect()
        {
            Destroy(gameObject);
        }
    }
}
