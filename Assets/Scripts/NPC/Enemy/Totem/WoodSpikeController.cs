using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

public class WoodSpikeController : MonoBehaviour , IDamageable
{
    private Rigidbody2D _rb;
    private Collider2D _collider;
    private WoodSpikeAttack _woodSpikeAttack;
    private Animator _animator;
    [SerializeField] private float _speed = 5;
    [SerializeField] private float _despawnDuration = 5;
    [SerializeField] private LayerMask _groundLayer;
    private int _direction = -1;
    
    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
        _woodSpikeAttack = GetComponent<WoodSpikeAttack>();
        _animator = GetComponent<Animator>();
    }

    void Start()
    {
        _rb.velocity = (transform.right * -1 * _speed);
    }
    
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            _rb.velocity = Vector2.zero;
            _animator.SetTrigger("Destroyed");
        }
    }

    public void Hit(float damageAmount)
    {
        _woodSpikeAttack._isAlive = false;
        StartCoroutine(Despawn(_despawnDuration));
        int allLayers = ~0;
        _collider.isTrigger = false;
        _collider.excludeLayers = allLayers & ~_groundLayer; 
    }
    
    public bool isDead()
    {
        return _woodSpikeAttack._isAlive;
    }

    private IEnumerator Despawn(float duration)
    {
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }

    private void Despawn()
    {
        Destroy(gameObject);
    }
}
