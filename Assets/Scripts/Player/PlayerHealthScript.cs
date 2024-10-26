using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerHealthScript : MonoBehaviour , IDamageable
{
    private Vector2 _checkPointposition;
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody2D;
    private IPlayerAnimator _animator;
    private PlayerController _controller;
    private Collider2D _collider;
    [Header("Configurations")]
    [SerializeField] private CheckPointConfig _checkPointConfig;
    [SerializeField] private PlayerConfig _playerConfig;
    [SerializeField] private LayerMask _includedLayersWhenDead;
    private float _currentHealth;
    private bool _isAlive;
    

    private void Awake()
    {
        _controller = GetComponent<PlayerController>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<PlayerAnimationScript>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
        _currentHealth = _playerConfig.maxHealth;
    }

    void Start()
    {
        _checkPointposition = transform.position;
    }

    private void Update()
    {
        if (_currentHealth <= 0)
        {
            Die();
        }

        

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("InstantDeath"))
        {
            _rigidbody2D.bodyType = RigidbodyType2D.Static;
            Die();
        }
    }

    public bool isDead()
    {
        return !_controller._isAlive;
    }

    public void UpdateCheckpointPosition(Vector2 position)
    {
        _checkPointposition = position;
    }

    public void Hit(float damageAmount)
    {
        if(!_controller._isAlive) return;
        _animator.HitAnimation();
        _currentHealth -= damageAmount;
        StartCoroutine(Stun(_playerConfig.stunTime));
    }

    private void Die()
    {
        _controller._isAlive = false;
        StartCoroutine(Respawn(_checkPointConfig._respawnDuration));
    }

    private IEnumerator Stun(float duration)
    {
        _controller._isStunned = true;
        yield return new WaitForSeconds(duration);
        _controller._isStunned = false;
    }

    private int _originalLayers;

    private IEnumerator Respawn(float duration)
    {
        int allLayers = ~0;
        _originalLayers = _collider.excludeLayers;
        
        _collider.isTrigger = false;
        _collider.excludeLayers = allLayers & ~_includedLayersWhenDead;
        
        _animator.DeathAnimation();
        _currentHealth = _playerConfig.maxHealth;
        _controller._isAlive = false;
        
        yield return new WaitForSeconds(duration);
        
        _spriteRenderer.enabled = false;
        _animator.RespawnAnimation();
        _spriteRenderer.enabled = true;
        
        _controller._isAlive = true;
        transform.position = _checkPointposition;
        _rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
        _rigidbody2D.velocity = Vector2.zero;
        
        _collider.excludeLayers = _originalLayers;
    }

}