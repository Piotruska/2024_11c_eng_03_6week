using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

public class WoodSpikeController : MonoBehaviour , IDamageable
{
    private Rigidbody2D _rb;
    private Collider2D _collider;
    private WoodSpikeAttack _woodSpikeAttack;
    [SerializeField] private float _speed = 5;
    [SerializeField] private float _despawnDuration = 5;
    [SerializeField] private LayerMask _groundLayer;
    private int _direction = -1;
    
    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
        _woodSpikeAttack = GetComponent<WoodSpikeAttack>();
    }

    void Start()
    {
        _rb.velocity = (transform.right * -1 * _speed);
    }
    
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    public void Hit(float damageAmount)
    {
        _woodSpikeAttack._isAlive = false;
        StartCoroutine(Despawn(_despawnDuration));
        int allLayers = ~0; 
        _collider.excludeLayers = allLayers & ~_groundLayer; // excludes all layers accept ground
    }

    private IEnumerator Despawn(float duration)
    {
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }
}
