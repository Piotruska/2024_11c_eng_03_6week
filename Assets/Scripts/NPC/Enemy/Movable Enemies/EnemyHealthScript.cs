using System;
using System.Collections;
using NPC.Enemy.Movable_Enemies.Interfaces;
using Player;
using UnityEngine;

namespace NPC.Enemy.Movable_Enemies
{
    public class EnemyHealthScript : MonoBehaviour, IDamageable, IEnemieHealthScript
    {
        private Rigidbody2D _rb;
        private Collider2D _collider;
        private IEnemyController _enemyController;
        private IEnemyAnimator _enemyAnimator;
        private SpriteRenderer _spriteRenderer;

        [SerializeField] private float _health = 20f;
        [SerializeField] private float _stunnTime = 2f;
        [SerializeField] private float _despawnTime = 3f; 
        [SerializeField] private float _fadeDuration = 2f;
        [SerializeField] private LayerMask _groundLayer;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _collider = GetComponent<Collider2D>(); 
            _enemyController = GetComponent<IEnemyController>();
            _enemyAnimator = GetComponent<IEnemyAnimator>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            if (isDead() && _enemyController.isGrounded())
            {
                _rb.velocity = new Vector2(0, _rb.velocity.y);
            }
        }

        public void Hit(float damageAmount)
        {
            if(_enemyController.GetState() == EnemyState.Die) return;
            DecreaseHealth(damageAmount);
            if (_health <= 0) Die();
            else
            {
                _enemyAnimator.HitAnimation();
                _enemyController.Stunn(_stunnTime);
            }
        }

        public bool isDead()
        {
            return _enemyController.GetState() == EnemyState.Die;
        }

        public void DecreaseHealth(float amount)
        {
            _health -= amount;
        }

        public void Die()
        {
            if (_enemyController.GetState() == EnemyState.Die) return;
            _enemyAnimator.DeadHitAnimation();
            _enemyController.ChangeState(EnemyState.Die);
            StartCoroutine(DespawnCoroutine());
        }

        private IEnumerator DespawnCoroutine()
        {
            //_rb.bodyType = RigidbodyType2D.Static;
            
            int allLayers = ~0;
            _collider.isTrigger = false;
            _collider.excludeLayers = allLayers & ~_groundLayer;
            
            yield return new WaitForSeconds(_despawnTime);
            
            Color color = _spriteRenderer.color;
            float fadeSpeed = color.a / _fadeDuration;
            
            while (color.a > 0)
            {
                color.a -= fadeSpeed * Time.deltaTime; 
                _spriteRenderer.color = color;
                yield return null; 
            }
            
            Destroy(gameObject);
        }
    }
}
