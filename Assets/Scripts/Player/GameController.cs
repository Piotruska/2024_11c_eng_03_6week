using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class GameController : MonoBehaviour , IDamageable
{
    private Vector2 _checkPointposition;
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody2D;
    private IPlayerAnimator _animator;
    private PlayerController _controller;
    [Header("Configurations")]
    [SerializeField] private CheckPointConfig _checkPointConfig;
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _stunTime;
    private float _currentHealth;

    private void Awake()
    {
        _controller = GetComponent<PlayerController>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<AnimationScript>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _currentHealth = _maxHealth;
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
            Die();
        }
    }

    public void UpdateCheckpointPosition(Vector2 position)
    {
        _checkPointposition = position;
    }

    public void Hit(float damageAmount)
    {
        _animator.HitAnimation();
        _currentHealth -= damageAmount;
        StartCoroutine(Stun(_stunTime));
    }

    private void Die()
    {
        StartCoroutine(Respawn(_checkPointConfig._respawnDuration));
    }

    private IEnumerator Stun(float duration)
    {
        _controller._isStunned = true;
        yield return new WaitForSeconds(duration);
        _controller._isStunned = false;
    }

    private IEnumerator Respawn(float duration)
    {
        
        _animator.DeathAnimation();
        _currentHealth = _maxHealth;
        //_rigidbody2D.bodyType = RigidbodyType2D.Static;
        yield return new WaitForSeconds(duration);
        _spriteRenderer.enabled = false;
        _animator.RespawnAnimation();
        _spriteRenderer.enabled = true;
        transform.position = _checkPointposition;
        //_rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
        _rigidbody2D.velocity = new Vector2(0, 0);
        
        
    }
}