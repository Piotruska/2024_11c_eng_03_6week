using Unity.VisualScripting;
using UnityEngine;

namespace Collectibles
{
    public class GoldCoin : Collectible
    {
        [SerializeField] private CoinConfig _coinConfig;
        protected override void Collect()
        {
            Destroy(gameObject);
        }
    }
}
