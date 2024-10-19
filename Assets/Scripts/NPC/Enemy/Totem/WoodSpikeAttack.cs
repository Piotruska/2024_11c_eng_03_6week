using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

public class WoodSpikeAttack : MonoBehaviour
{
    private Collider2D _attackCollider;
    [Header("Player Identification")] 
    [SerializeField]private bool _attacksPlayer = true;
    [SerializeField] private LayerMask _player;

    [Header("Enemy Identification")] 
    [SerializeField]private bool _attacksEnemie = false;
    [SerializeField] private LayerMask _enemies;
    
    [Header("Values")] 
    [SerializeField] private float _dammageAmount = 1;
    [SerializeField] private float _knockbackStrength = 2;
    [SerializeField] private float _upwardKnockbackStrength = 2;

    public bool _isAlive = true;
    
    private void Awake()
    {
        _attacksPlayer = true;
        _attacksEnemie = false;
        _attackCollider = GetComponent<Collider2D>();
    }
    
    void Update()
    {
        if(!_isAlive) return;
        
        List<Collider2D> colliders = new List<Collider2D>();
        ContactFilter2D filter = new ContactFilter2D();
        if (_attacksEnemie && _attacksPlayer) filter.SetLayerMask(_enemies | _player); 
        else if (_attacksEnemie) filter.SetLayerMask(_enemies);
        else if (_attacksPlayer) filter.SetLayerMask(_player);
        
        filter.useTriggers = true; 

        _attackCollider.OverlapCollider(filter, colliders);

        if (colliders.Count > 0)
        {
            ApplyEffects(colliders.ToArray());
        }
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
        Destroy(gameObject);
    }
}
