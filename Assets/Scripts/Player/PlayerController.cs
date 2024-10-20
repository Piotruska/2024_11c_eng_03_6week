using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using Player.Interfaces;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rb;
    private IPlayerAnimator _animator;
    private ICanAttack _canAttack;
    private ICanInteract _canInteract;
    
    private float _xInput;
    private bool _jumpInput;
    private bool _dashInput;
    private bool _meleeAttackInput;
    private bool _interactInput;
    
    private bool _perform_jump;
    private bool _isGrounded;
    private int _extraJumpsValue;
    private int _currentAirDashCount; 
    private bool _jumpbool = false;
    private bool _isDashing = false;
    private bool _canDash = true;
    public bool _isStunned { get; set; } = false;
    [SerializeField] private bool _hasSword = false;
    

    [Header("Configurations")] 
    [SerializeField] private PlayerConfig _config;
    [SerializeField] private int _amountOfAirDash = 1;
    

    [Header("Ground Check")] 
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private float _checkRadious;
    [SerializeField] private LayerMask _whatIsGround;
    [SerializeField] private LayerMask _whatIsEnemy;



    public bool IsGrounded
    {
        get => _isGrounded;
    }

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<AnimationScript>();
        _canAttack = GetComponent<AttackMechanic>();
        _canInteract = GetComponent<InteractionMechanic>();
        _extraJumpsValue = _config.extraJumpCount;
    }

    void Update()
    {
        if(_isDashing || _isStunned) return;
        
        _xInput = Input.GetAxis("Horizontal Movement");
        _jumpInput = Input.GetButtonDown("Jump");
        _dashInput = Input.GetButton("Dash");
        _meleeAttackInput = Input.GetButtonDown("Melee Attack");
        _interactInput = Input.GetButtonDown("Interact");

        if (_isGrounded)
        {
            _extraJumpsValue = _config.extraJumpCount;
            _currentAirDashCount = _amountOfAirDash;
        }
        

        if (_jumpInput && _extraJumpsValue > 0)
        {
            _jumpbool = true;
            _extraJumpsValue--;
        }
        else if (_jumpInput && _extraJumpsValue == 0 && _isGrounded)
        {
            _jumpbool = true;
        }

        if (_meleeAttackInput && _hasSword) Attack();
        if (_interactInput) Interact();
        if (_dashInput && _canDash && (_currentAirDashCount > 0)) StartCoroutine(Dash());

    }

    void FixedUpdate()
    {
        CheckIfGrounded();
        if(_isDashing || _isStunned) return;
        _animator.FacingCheck();
        _rb.velocity = new Vector2(_xInput * _config.movementSpeed, _rb.velocity.y);
        if (_xInput != 0) Walk(); else Idle();
        if (_jumpbool) Jump();
    }

    private void CheckIfGrounded()
    {
        Vector2 groundCheck = _groundCheck.position;
        _isGrounded = (Physics2D.OverlapCircle(groundCheck, _checkRadious, _whatIsGround)||
                       Physics2D.OverlapCircle(groundCheck, _checkRadious, _whatIsEnemy));
        
 
    }

    private void Walk()
    {
        _animator.WalkAnimation();

    }

    private void Idle()
    {
        _animator.IdleAnimation();
    }

    private void Jump()
    {
        _animator.JumpAnimation();
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
        _animator.IdleAnimation();
        _rb.gravityScale = originalGravity;
        _rb.velocity = originalVelocity;
        _isDashing = false;
        yield return new WaitForSeconds(_config.dashCooldown);
        if (!_isGrounded) _currentAirDashCount--;
        _canDash = true;
        
    }

    private void Attack()
    {
        _animator.AttackAnimation();
        if(_isGrounded) _canAttack.GroundAttackEnemies(); else _canAttack.AirAttackEnemies();
    }

    private void Interact()
    {
        _canInteract.InteractAction();
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_groundCheck.position, _checkRadious);
    }
}
    