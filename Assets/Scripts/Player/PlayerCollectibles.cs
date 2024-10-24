using UnityEngine;

namespace Player
{
    public class PlayerCollectibles : MonoBehaviour
    {
        private static int _coinCount = 0;
        private static int _redPotionCount = 0;
        private static int _bluePotionCount = 0;

        public static void IncreaseCoinCount(int amount)
        {
            _coinCount+=amount;
        }
        
        public static void IncreaseRedPotionCount(int amount)
        {
            _redPotionCount+=amount;
        }
        
        public static void IncreaseBluePotionCount(int amount)
        {
            _bluePotionCount+=amount;
        }

        public static int GetCoinCount()
        {
            return _coinCount;
        }
        
        public static int GetRedPotionCount()
        {
            return _redPotionCount;
        }
        
        public static int GetBluePotionCount()
        {
            return _bluePotionCount;
        }
    }
}
