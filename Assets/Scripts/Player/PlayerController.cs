using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rb;
    private IPlayerAnimator _animator;
    
    private float _xInput;
    private bool _jumpInput;
    private bool _dashInput;
    private bool _meleeAttackInput;
    
    private bool _perform_jump;
    private bool _isGrounded;
    private int _extraJumpsValue;
    private bool _jumpbool = false;
    private bool _isDashing = false;
    private bool _canDash = true;
    private bool _facingRight = false;
    

    [Header("Configurations")] 
    [SerializeField] private PlayerConfig _config;

    [Header("Ground Check")] 
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
        _extraJumpsValue = _config.extraJumpCount;
    }

    void Update()
    {
        if(_isDashing) return;
        
        _xInput = Input.GetAxis("Horizontal Movement");
        _jumpInput = Input.GetButtonDown("Jump");
        _dashInput = Input.GetButton("Dash");
        _meleeAttackInput = Input.GetButtonDown("Melee Attack");

        if (_isGrounded) _extraJumpsValue = _config.extraJumpCount;
        

        if (_jumpInput && _extraJumpsValue > 0)
        {
            _jumpbool = true;
            _extraJumpsValue--;
        }
        else if (_jumpInput && _extraJumpsValue == 0 && _isGrounded)
        {
            _jumpbool = true;
        }

        if (_meleeAttackInput) _animator.Attack();
        if (_dashInput && _canDash) StartCoroutine(Dash());

    }

    void FixedUpdate()
    {
        CheckIfGrounded();
        if(_isDashing) return;
        _animator.FacingCheck();
        _rb.velocity = new Vector2(_xInput * _config.movementSpeed, _rb.velocity.y);
        if (_xInput != 0) Walk(); else Idle();
        if (_jumpbool) Jump();
    }

    private void CheckIfGrounded()
    {
        _isGrounded = Physics2D.OverlapCircle(_groundCheck.position, _checkRadious, _whatIsGround);
 
    }

    private void Walk()
    {
        _animator.Walk();

    }

    private void Idle()
    {
        _animator.Idle();
    }

    private void Jump()
    {
        _animator.Jump();
        _rb.velocity = Vector2.up * _config.jumpForce;
        _jumpbool = false;
    }

    private IEnumerator Dash()
    {
        _animator.DashOn();
        _isDashing = true;
        _canDash = false;
        float originalGravity = _rb.gravityScale;
        Vector2 originalVelocity = _rb.velocity;
        _rb.gravityScale = 0f;
        _rb.velocity = new Vector2((transform.localScale.x * _config.dashSpeed)+originalVelocity.x, 0f);
        yield return new WaitForSeconds(_config.dashDuration);
        _animator.Idle();
        _rb.gravityScale = originalGravity;
        _rb.velocity = originalVelocity;
        _isDashing = false;
        yield return new WaitForSeconds(_config.dashCooldown);
        _canDash = true;
        
    }

}
    