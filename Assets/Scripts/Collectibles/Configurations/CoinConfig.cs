using UnityEngine;
using UnityEngine.Serialization;

namespace Collectibles
{
    [CreateAssetMenu(menuName = "Scriptable Objects/CoinConfig",fileName = "CoinConfig")]
    public class CoinConfig : ScriptableObject
    {
        [Header("Coin Values")]
        public int goldValue = 10;
    }
}
