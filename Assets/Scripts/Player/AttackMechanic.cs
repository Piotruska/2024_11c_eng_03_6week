using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using Trainng_Dummy;
using Unity.VisualScripting;
using UnityEngine;

public class AttackMechanic : MonoBehaviour, ICanAttack
{
    [Header("Ground Attack Config")] 
    [SerializeField] private GameObject _groundAttackPoint;
    [SerializeField] private float _radiusOfGroundAttack;
    [Header("Air Attack Config")] 
    [SerializeField] private GameObject _airAttackPoint;
    [SerializeField] private float _radiusOfAirAttack;
    [Header("Enemy Identification")] 
    [SerializeField] public LayerMask _enemies;
    [Header("Values")] 
    [SerializeField] public float _dammageAmount = 1 ;
    [SerializeField] public float _knockbackStrength = 2 ;
    [SerializeField] public float _upwardKnockbackStrength = 2 ;

    public void GroundAttackEnemies()
    {
        RaycastHit2D[] enemies = Physics2D.CircleCastAll(_groundAttackPoint.transform.position, _radiusOfGroundAttack,
                transform.right, 0f, _enemies); 
        ApplyEffects(enemies);

    }
    
    public void AirAttackEnemies()
    {
        RaycastHit2D[] enemies = Physics2D.CircleCastAll(_airAttackPoint.transform.position, _radiusOfAirAttack,
            transform.right, 0f, _enemies); 
        ApplyEffects(enemies);
    }

    private void ApplyEffects(RaycastHit2D[] enemies)
    {
        foreach (RaycastHit2D obj in enemies)
        {
            IDamageable iDamageable = obj.collider.gameObject.GetComponent<IDamageable>();
            
            if (iDamageable != null) 
            { 
                iDamageable.Hit(_dammageAmount);
                Rigidbody2D enemyRb = obj.rigidbody;
                Vector2 knockbackDirection = (obj.transform.position - transform.position).normalized;
                knockbackDirection.y += _upwardKnockbackStrength; 
                knockbackDirection.Normalize();
                enemyRb.AddForce(knockbackDirection * _knockbackStrength, ForceMode2D.Impulse);
                
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(_groundAttackPoint.transform.position, _radiusOfGroundAttack);
        Gizmos.DrawWireSphere(_airAttackPoint.transform.position, _radiusOfAirAttack);
    }
    
}
