using System.Collections;
using System.Collections.Generic;
using Player;
using Trainng_Dummy;
using UnityEngine;

public class AttackMechanic : MonoBehaviour, ICanAttack
{
    [Header("Ground Attack Config")] 
    [SerializeField] private CapsuleCollider2D _groundAttackCollider; // Reference to the Ground CapsuleCollider2D
    [Header("Air Attack Config")] 
    [SerializeField] private CapsuleCollider2D _airAttackCollider; // Reference to the Air CapsuleCollider2D
    [Header("Enemy Identification")] 
    [SerializeField] private LayerMask _enemies;
    [Header("Values")] 
    [SerializeField] private float _dammageAmount = 1;
    [SerializeField] private float _knockbackStrength = 2;
    [SerializeField] private float _upwardKnockbackStrength = 2;

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
                iDamageable.Hit(_dammageAmount);
                Rigidbody2D enemyRb = collider.GetComponent<Rigidbody2D>();
                enemyRb.bodyType = RigidbodyType2D.Dynamic;
                enemyRb.velocity = new Vector2(0, 0);
                Vector2 knockbackDirection = (collider.transform.position - transform.position).normalized;
                knockbackDirection.y += _upwardKnockbackStrength; 
                knockbackDirection.Normalize();
                enemyRb.AddForce(knockbackDirection * _knockbackStrength, ForceMode2D.Impulse);
            }
        }
    }
}
