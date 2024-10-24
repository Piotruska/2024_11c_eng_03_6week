using UnityEngine;

namespace Player
{
    public class PlayerCollectibles : MonoBehaviour
    {
        private static int _coinCount = 0;

        public static void IncreaseCoinCount(int amount)
        {
            _coinCount+=amount;
        }

        public static int GetCoinCount()
        {
            return _coinCount;
        }
    }
}
