using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private Vector2 _startPosition;
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody2D;
    [SerializeField]private float _respawnDuration = 0.5f;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        _startPosition = transform.position;
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("InstantDeath"))
        {
            Die();
        }
    }

    private void Die()
    {
        StartCoroutine(Respawn(0.5f));
    }

    private IEnumerator Respawn(float duration)
    {
        _spriteRenderer.enabled = false;
        yield return new WaitForSeconds(duration);
        transform.position = _startPosition;
        _rigidbody2D.velocity = new Vector2(0, 0);
        _spriteRenderer.enabled = true;
    }
    
}
