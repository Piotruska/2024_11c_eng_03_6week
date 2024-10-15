using UnityEngine;
using UnityEngine.Serialization;

namespace Collectibles
{
    [CreateAssetMenu(menuName = "Scriptable Objects/CoinConfig",fileName = "CoinConfig")]
    public class CoinConfig : ScriptableObject
    {
        public float value = 10;
    }
}