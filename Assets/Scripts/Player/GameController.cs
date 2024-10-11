using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private Vector2 _checkPointposition;
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

    private void Die()
    {
        StartCoroutine(Respawn(_respawnDuration));
    }

    private IEnumerator Respawn(float duration)
    {
        _spriteRenderer.enabled = false;
        _rigidbody2D.bodyType = RigidbodyType2D.Static;
        yield return new WaitForSeconds(duration);
        transform.position = _checkPointposition;
        _rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
        _rigidbody2D.velocity = new Vector2(0, 0);
        _spriteRenderer.enabled = true;
    }
    
}
