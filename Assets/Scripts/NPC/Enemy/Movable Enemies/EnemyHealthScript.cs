using System;
using NPC.Enemy.Movable_Enemies.Interfaces;
using Player;
using UnityEngine;

namespace NPC.Enemy.Movable_Enemies
{
    public class EnemyHealthScript : MonoBehaviour,IDamageable
    {
        private Rigidbody2D _rb;
        private IEnemyController _enemyController;
        private IEnemyAnimator _enemyAnimator;

        [SerializeField] private float _health = 20f;
        [SerializeField] private float _stunnTime = 2;
        
        
        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _enemyController = GetComponent<IEnemyController>();
            _enemyAnimator = GetComponent<IEnemyAnimator>();
        }
        


        public void Hit(float damageAmount)
        {
            DecreaseHealth(damageAmount);
            if(_health <= 0) Die();
            else
            {
                _enemyAnimator.HitAnimation();
                _enemyController.Stunn(_stunnTime);
            }
            
            
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("InstantDeath"))
            {
                Die();
            }
        }


        public void DecreaseHealth(float amount)
        {
            _health -= amount;
        }


        private void Die()
        {
            if(_enemyController.GetState() == EnemyState.Die) return;
            _enemyAnimator.DeadHitAnimation();
            _enemyController.ChangeState(EnemyState.Die);
        }
    }
}