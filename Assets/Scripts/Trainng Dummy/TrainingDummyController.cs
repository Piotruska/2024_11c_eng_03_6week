using System;
using Player;
using UnityEngine;

namespace Trainng_Dummy
{
    public class TrainingDummyController  : MonoBehaviour, IDamageable
    {
        [SerializeField] private float _maxHealth = 2f;
        private float _currentHealth;
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _currentHealth = _maxHealth;
        }

        public void Hit(float damageAmount)
        {
            _animator.SetTrigger("Hit");
            _currentHealth -= damageAmount;
        }
    }
}