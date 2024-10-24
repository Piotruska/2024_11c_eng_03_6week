using UnityEngine;

namespace Player
{
    public class PlayerCollectibles : MonoBehaviour
    {
        private int _coinCount = 0;

        public void IncreaseCoinCount(int amount)
        {
            _coinCount+=amount;
        }
    }
}
