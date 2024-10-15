using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rb;
    private IPlayerAnimator _animator;
    private float _xInput;
    private bool _perform_jump;
    private bool _isGrounded;
    private int extraJumpsValue;
    private KeyCode _jumpKeyCode = KeyCode.UpArrow;
    private bool _jumpbool = false;
    [SerializeField] private PlayerConfig _config;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private float _checkRadious;
    [SerializeField] private LayerMask _whatIsGround;



    public bool IsGrounded
    {
        get => _isGrounded;
    }

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<AnimationScript>();
        extraJumpsValue = _config.extraJumpCount;
    }

    void Update()
    {
        _xInput = Input.GetAxis("Horizontal");

        if (_isGrounded)
        {
            extraJumpsValue = _config.extraJumpCount;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && extraJumpsValue > 0)
        {
            _jumpbool = true;
            extraJumpsValue--;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) && extraJumpsValue == 0 && _isGrounded)
        {
            _jumpbool = true;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _animator.Attack();
        }

    }

    void FixedUpdate()
    {
        _isGrounded = Physics2D.OverlapCircle(_groundCheck.position, _checkRadious, _whatIsGround);
        _rb.velocity = new Vector2(_xInput * _config.movementSpeed, _rb.velocity.y);

        if (_jumpbool)
        {
            _animator.Jump();
            _rb.velocity = Vector2.up * _config.jumpForce;
            _jumpbool = false;
        }
    }
}
    
