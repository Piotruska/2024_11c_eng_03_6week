using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using Unity.VisualScripting;
using UnityEngine;

public class GameController : MonoBehaviour , IDamageable
{
    private Vector2 _checkPointposition;
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody2D;
    private IPlayerAnimator _animator;
    [Header("Configurations")]
    [SerializeField] private CheckPointConfig _checkPointConfig;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<AnimationScript>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        _checkPointposition = transform.position;
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
        throw new NotImplementedException();
    }

    private void Die()
    {
        StartCoroutine(Respawn(_checkPointConfig._respawnDuration));
    }

    private IEnumerator Respawn(float duration)
    {
        
        _animator.DeathAnimation();
        _rigidbody2D.bodyType = RigidbodyType2D.Static;
        yield return new WaitForSeconds(duration);
        _spriteRenderer.enabled = false;
        _animator.RespawnAnimation();
        _spriteRenderer.enabled = true;
        transform.position = _checkPointposition;
        _rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
        _rigidbody2D.velocity = new Vector2(0, 0);
        
        
    }
}