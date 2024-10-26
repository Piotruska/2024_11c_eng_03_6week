using UnityEngine;

namespace Collectibles.Configurations
{
    [CreateAssetMenu(menuName = "Scriptable Objects/CoinConfig",fileName = "CoinConfig")]
    public class CoinConfig : ScriptableObject
    {
        [Header("Coin Values")]
        public int goldValue = 10;
    }
}
