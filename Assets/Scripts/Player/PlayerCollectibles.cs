using UnityEngine;

namespace Player
{
    public class PlayerCollectibles : MonoBehaviour
    {
        private static int _coinCount = 0;
        private static int _keyCount = 0;
        private static int _redPotionCount = 0;
        private static int _bluePotionCount = 0;

        public static int GetCoinCount()
        {
            return _coinCount;
        }
        public static void IncreaseCoinCount(int amount)
        {
            _coinCount+=amount;
        }
        
        public static void DecreaseCoinCount(int amount)
        {
            _coinCount-=amount;
        }

        public static int GetKeyCount()
        {
            return _keyCount;
        }
        
        public static void IncreaseKeyCount(int amount)
        {
            _keyCount+=amount;
        }
        
        public static void DecreaseKeyCount(int amount)
        {
            _keyCount-=amount;
        }
        
        public static int GetRedPotionCount()
        {
            return _redPotionCount;
        }
        
        public static void IncreaseRedPotionCount(int amount)
        {
            _redPotionCount+=amount;
        }
        
        public static void DecreaseRedPotionCount(int amount)
        {
            _redPotionCount-=amount;
        }
        
        public static int GetBluePotionCount()
        {
            return _bluePotionCount;
        }
        
        public static void IncreaseBluePotionCount(int amount)
        {
            _bluePotionCount+=amount;
        }
        
        public static void DecreaseBluePotionCount(int amount)
        {
            _bluePotionCount-=amount;
        }
    }
}
