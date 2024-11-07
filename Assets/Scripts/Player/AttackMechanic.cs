using System.Collections.Generic;
using Player.Interfaces;
using UnityEngine;

namespace Player
{
    public class AttackMechanic : MonoBehaviour, ICanAttack
    {
        [Header("Ground Attack Config")] 
        [SerializeField] private CapsuleCollider2D _groundAttackCollider; 
        [Header("Air Attack Config")] 
        [SerializeField] private CapsuleCollider2D _airAttackCollider; 
        [Header("Enemy Identification")] 
        [SerializeField] private LayerMask _enemies;
        [Header("Player Configurations")] 
        [SerializeField] private PlayerConfig _config;

    
        public void GroundAttackEnemies()
        {
            List<Collider2D> colliders = new List<Collider2D>();
            ContactFilter2D filter = new ContactFilter2D();
            filter.SetLayerMask(_enemies); 
            filter.useTriggers = true; 

            _groundAttackCollider.OverlapCollider(filter, colliders);
        
            ApplyEffects(colliders.ToArray());
        }

        public void AirAttackEnemies()
        {

            List<Collider2D> colliders = new List<Collider2D>();
            ContactFilter2D filter = new ContactFilter2D();
            filter.SetLayerMask(_enemies); 
            filter.useTriggers = true; 

            _airAttackCollider.OverlapCollider(filter, colliders);
        
            ApplyEffects(colliders.ToArray());
        }

        private void ApplyEffects(Collider2D[] enemies)
        {
            foreach (Collider2D collider in enemies)
            {
                IDamageable iDamageable = collider.gameObject.GetComponent<IDamageable>();
            
                if (iDamageable != null) 
                { 
                    if(iDamageable.isDead()) return;
                    iDamageable.Hit(_config.dammageAmount);
                    Rigidbody2D enemyRb = collider.GetComponent<Rigidbody2D>(); 
                    enemyRb.bodyType = RigidbodyType2D.Dynamic;
                    enemyRb.velocity = new Vector2(0, 0);
                    Vector2 knockbackDirection = (collider.transform.position - transform.position).normalized;
                    knockbackDirection.y += _config.yAxisKnockbackStrength; 
                    knockbackDirection.Normalize();
                    enemyRb.AddForce(knockbackDirection * _config.generalKnockbackStrength, ForceMode2D.Impulse);
                }
            }
        }
    }
}
