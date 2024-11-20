using Player.Interfaces;
using UnityEngine;

namespace NPC.Enemy.Trainng_Dummy
{
    public class TrainingDummyController  : MonoBehaviour, IDamageable
    {
        [Header("Health")] 
        [SerializeField] private float _maxHealth = 20.0f;
        private float _currentHealth;
        private Animator _animator;
        private bool _isAlive = true;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _currentHealth = _maxHealth;
        }

        private void FixedUpdate()
        {
            if (_currentHealth <= 0)
            {
                Die();
            }
        }

        public void Hit(float damageAmount)
        {
            _animator.SetTrigger("Hit");
            _currentHealth -= damageAmount;
        }

        public bool isDead()
        {
            return !_isAlive;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("InstantDeath"))
            {
                Die();
            }
        }

        private void Die()
        {
            _isAlive = false;
            Destroy(gameObject);
        }
    }
}