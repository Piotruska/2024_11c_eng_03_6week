using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using Trainng_Dummy;
using Unity.VisualScripting;
using UnityEngine;

public class AttackMechanic : MonoBehaviour, ICanAttack
{
    [SerializeField] private GameObject _attackPoint;
    [SerializeField] private float _radiusOfAttack;
    [SerializeField] public LayerMask _enemies;
    [SerializeField] public float _dammageAmount = 1 ;
    [SerializeField] public float _knockbackForce = 2 ;

    public bool shouldBeDamaging { get; private set; } = false;


    public void AttackEnemies()
    {
        shouldBeDamaging = true; 
        
        RaycastHit2D[] enemies = Physics2D.CircleCastAll(_attackPoint.transform.position, _radiusOfAttack,
                transform.right, 0f, _enemies); 
        foreach (RaycastHit2D obj in enemies)
        {
            IDamageable iDamageable = obj.collider.gameObject.GetComponent<IDamageable>();
            
            if (iDamageable != null) 
            { 
                iDamageable.Hit(_dammageAmount);
                Rigidbody2D enemyRb = obj.rigidbody;
                Vector2 knockbackDireciton = obj.transform.position - transform.position;
                knockbackDireciton.Normalize();
                enemyRb.AddForce(knockbackDireciton * _knockbackForce, ForceMode2D.Impulse );
                
            }
        }
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(_attackPoint.transform.position, _radiusOfAttack);
    }
}
