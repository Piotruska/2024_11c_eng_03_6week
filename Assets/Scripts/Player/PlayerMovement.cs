using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rb;
    private float _xInput;
    private bool _perform_jump ;
    private bool _isGrounded;
    private int extraJumpsValue;
    private KeyCode _jumpKeyCode = KeyCode.UpArrow;
    [SerializeField] private PlayerConfig _config;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private float _checkRadious;
    [SerializeField] private LayerMask _whatIsGround; 
    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        extraJumpsValue = _config.extraJumpCount;
    }

    void Update()
    {
        _xInput = Input.GetAxis("Horizontal");
        
        if (_isGrounded)
        {
            extraJumpsValue = _config.extraJumpCount;
        }
        
        if (Input.GetKeyDown(KeyCode.UpArrow) && extraJumpsValue >0)
        {
            _rb.velocity = Vector2.up* _config.jumpForce;
            extraJumpsValue--;
        }else if(Input.GetKeyDown(KeyCode.UpArrow) && extraJumpsValue == 0 && _isGrounded){
            _rb.velocity = Vector2.up * _config.jumpForce;
        }
        
    }
    void FixedUpdate()
    {
        _isGrounded = Physics2D.OverlapCircle(_groundCheck.position, _checkRadious, _whatIsGround);
        _rb.velocity = new Vector2(_xInput * _config.movementSpeed, _rb.velocity.y);
    }
}
