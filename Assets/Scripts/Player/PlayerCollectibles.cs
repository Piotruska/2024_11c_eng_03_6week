using System;
using Collectables;
using UI;
using UnityEngine;

namespace Player
{
    public class PlayerCollectibles : MonoBehaviour
    {
        private static AudioManeger _audioManeger;

        private static int _coinCount = 0;
        private static int _keyCount = 0;
        private static int _redPotionCount = 0;
        private static int _bluePotionCount = 0;
        private static bool _hasDiamond1 = false;
        private static bool _hasDiamond2 = false;
        private static bool _hasDiamond3 = false;
        
        private static BlueDiamondDisplay _blueDiamondDisplay;
        private static RedDiamondDisplay _redDiamondDisplay;
        private static GreenDiamondDisplay _greenDiamondDisplay;

        private void Awake()
        {
            _audioManeger = GameObject.FindWithTag("AudioManager").GetComponent<AudioManeger>();
            _blueDiamondDisplay = FindObjectOfType<BlueDiamondDisplay>();
            _greenDiamondDisplay = FindObjectOfType<GreenDiamondDisplay>();
            _redDiamondDisplay = FindObjectOfType<RedDiamondDisplay>();
        }

        public static void ResetDiamonds()
        {
            _hasDiamond1 = false;
            _hasDiamond2 = false;
            _hasDiamond3 = false;
            _blueDiamondDisplay.Reset();
            _redDiamondDisplay.Reset();
            _greenDiamondDisplay.Reset();
        }

        public static bool HasAllDiamonds()
        {
            return _hasDiamond1 && _hasDiamond2 && _hasDiamond3;  
        }

        public void DiamondReset()
        {
            _hasDiamond1 = false;
            _hasDiamond2 = false;
            _hasDiamond3 = false;
        }
        public void Reset()
        { 
            _coinCount = 0; 
            _keyCount = 0; 
            _redPotionCount = 0; 
            _bluePotionCount = 0;
            _hasDiamond1 = false;
            _hasDiamond2 = false; 
            _hasDiamond3 = false;
        }

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
            _audioManeger.PlayCollectableSFX(_audioManeger.potionUse);
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

        public static void GetDiamond1()
        {
            _hasDiamond1 = true;
        }
        
        public static void GetDiamond2()
        {
            _hasDiamond2 = true;
        }
        
        public static void GetDiamond3()
        {
            _hasDiamond3 = true;
        }
    }
}
