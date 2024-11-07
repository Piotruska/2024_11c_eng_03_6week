using NPC.Enemy.Movable_Enemies.Interfaces;
using Player.Interfaces;
using UnityEngine;

namespace NPC.Enemy.Movable_Enemies
{
    public class EnemyCombat : MonoBehaviour
    {
        private IEnemyController _enemyController;
        private IEnemyAnimator _enemyAnimator;
        private bool _isGrounded;
        private float attackCooldownTimer = 0f;

        [Header("Attack Configuration")]
        [SerializeField]
        private bool _canAttack;
        [SerializeField]
        private Transform _centerPoint; 
        [SerializeField]
        private Vector2 _boxSize = new Vector2(2f, 2f); 
        [SerializeField]
        private float _damage = 4;
        [SerializeField]
        private float _yAxisKnockbackStrength = 4;
        [SerializeField]
        private float _generalKnockbackForce = 4;

        [Header("Damageable Detection")]
        [SerializeField]
        private LayerMask[] _damageLayer;
        [SerializeField]
        private LayerMask[] _attackLayers;

        [Header("Attack Cooldown")]
        [SerializeField]
        private float attackCooldown = 5f;
    
        [Header("Gizmos Options")]
        [SerializeField] private bool _showMovementGizmos = true;

        private Collider2D[] colliders = null;

        private void Awake()
        {
            _enemyController = GetComponent<IEnemyController>();
            _enemyAnimator = GetComponent<IEnemyAnimator>();
        }

        private void FixedUpdate()
        {
            if(_enemyController.GetState() == EnemyState.Die || !_enemyController.isPlayerAlive()) return;
            _isGrounded = _enemyController.isGrounded();

            if (attackCooldownTimer > 0)
            {
                attackCooldownTimer -= Time.deltaTime;
            }

            bool shouldAttack = attackCooldownTimer <= 0 && _isGrounded && IsPlayerInAttackRange() && _canAttack && _enemyController.GetState() != EnemyState.Die;

            if (shouldAttack)
            {
                Attack();
                attackCooldownTimer = attackCooldown;
            }
        }

        private bool IsPlayerInAttackRange()
        {
            colliders = Physics2D.OverlapBoxAll(_centerPoint.position, _boxSize, 0f, GetCombinedLayerMask(_attackLayers));
            return colliders.Length > 0;
        }

        private void Attack()
        {
            colliders = null;
            _enemyAnimator.AttackAnimation();
        }
    
        private void ApplyEffects()
        {
            if(_enemyController.GetState() == EnemyState.Die) return;
            colliders = Physics2D.OverlapBoxAll(_centerPoint.position, _boxSize, 0f, GetCombinedLayerMask(_damageLayer));
            foreach (Collider2D collider in colliders)
            {
                IDamageable iDamageable = collider.gameObject.GetComponent<IDamageable>();

                if (iDamageable != null)
                {
                    if(iDamageable.isDead()) return;
                    iDamageable.Hit(_damage);
                    Rigidbody2D playerRb = collider.GetComponent<Rigidbody2D>();
                    playerRb.bodyType = RigidbodyType2D.Dynamic;
                    playerRb.velocity = Vector2.zero;

                    Vector2 knockbackDirection = (collider.transform.position - transform.position).normalized;
                    knockbackDirection.y += _yAxisKnockbackStrength;
                    knockbackDirection.Normalize();

                    playerRb.AddForce(knockbackDirection * _generalKnockbackForce, ForceMode2D.Impulse);
                }
            }
        }

        private LayerMask GetCombinedLayerMask(LayerMask[] layers)
        {
            LayerMask combinedMask = 0;
            foreach (var layer in layers)
            {
                combinedMask |= layer;
            }
            return combinedMask;
        }


        private void OnDrawGizmosSelected()
        {
            if (_showMovementGizmos)
            {
                if (_centerPoint != null)
                {
                    Gizmos.color = Color.red;
                    Gizmos.DrawWireCube(_centerPoint.position, _boxSize);
                } 
            }
        
        }
    }
}
