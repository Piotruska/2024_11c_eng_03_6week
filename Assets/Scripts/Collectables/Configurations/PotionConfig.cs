using UnityEngine;

namespace Collectibles.Configurations
{
    [CreateAssetMenu(menuName = "Scriptable Objects/PotionConfig",fileName = "PotionConfig")]
    public class PotionConfig : ScriptableObject
    {
        [Header("Potion Parameters")]
        [Header("Red Potion")]
        public int healthRestore = 20;
        [Header("Blue Potion")]
        public int speedBoostEffect = 2;
        public int speedBoostDuration = 10;
    }
}