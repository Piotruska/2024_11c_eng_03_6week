using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rb;
    private float _xInput;
    [SerializeField] private PlayerConfig _config;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        _xInput = Input.GetAxis("Horizontal");
        
    }
    
    void FixedUpdate()
    {
        // Fix config
        _rb.velocity = new Vector2(_xInput * _config.movementSpeed, _rb.velocity.y);
    }
}
