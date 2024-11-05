using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class HealthBarDisplay : MonoBehaviour
    {
        private Image _healthBar;
        private PlayerHealthScript _playerHealthController;
        //[SerializeField] private PlayerConfig _playerConfig;
        //private float _maxHealth;

        private void Awake()
        {
            _healthBar = GetComponent<Image>();
            //_maxHealth = _playerConfig.maxHealth;
            //_playerHealthController = FindObjectOfType<PlayerHealthScript>();
        }

        private void Update()
        {
            
        }

        public void ResetHealthBar()
        {
            _healthBar.fillAmount = 1;
        }

        public void UpdateHealthBar(float currentHealth, float maxHealth)
        {
            _healthBar.fillAmount = currentHealth / maxHealth;
        }
    }
}
