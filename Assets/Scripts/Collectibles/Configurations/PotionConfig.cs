using UnityEngine;

namespace Collectibles.Configurations
{
    [CreateAssetMenu(menuName = "Scriptable Objects/PotionConfig",fileName = "PotionConfig")]
    public class PotionConfig : ScriptableObject
    {
        [Header("Potion Parameters")]
        private int healthRestore = 20;
        private int speedBoost = 50;
    }
}
