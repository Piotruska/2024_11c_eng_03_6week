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
    private ICanAttack _IcanAttack;
    private ICanInteract _canInteract;

    
    //Inputs
    private float _xInput;
    private bool _jumpInput;
    private bool _dashInput;
    private bool _meleeAttackInput;
    private bool _interactInput;
    
    //Surroundings Checks
    private bool _isGrounded;
    
    //Jump Mechanic Variables
    private bool _perform_jump;
    private int _extraJumpsValue;
    private int _currentAirDashCount; 
    private bool _jumpbool = false;
    
    //Dashing Mechanic Variables
    private bool _isDashing = false;
    private bool _canDash = true;
    
    //Attacking & Taking Damage
    public bool _isStunned { get; set; } = false;
    private bool _hasSword = false;
    private int _currentAttackIndex = 1; 
    private Coroutine _attackCoroutine;
    private bool _canAttack = true;
    public bool _isAlive = true;

    

    [Header("Configurations")] 
    [SerializeField] private PlayerConfig _config;
    

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
        _animator = GetComponent<PlayerAnimationScript>();
        _IcanAttack = GetComponent<AttackMechanic>();
        _canInteract = GetComponent<InteractionMechanic>();
        _extraJumpsValue = _config.extraJumpCount;
    }

    void Update()
    {
        if(_isDashing || _isStunned || !_isAlive) return;
        
        _xInput = Input.GetAxis("Horizontal Movement");
        _jumpInput = Input.GetButtonDown("Jump");
        _dashInput = Input.GetButton("Dash");
        _meleeAttackInput = Input.GetButtonDown("Melee Attack");
        _interactInput = Input.GetButtonDown("Interact");

        if (_isGrounded)
        {
            _extraJumpsValue = _config.extraJumpCount;
            _currentAirDashCount = _config.amountOfAirDash;
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
    
    public void HasSword(bool hasSword)
    {
        _animator.HasSword(hasSword);
        _hasSword = hasSword;
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
        _canAttack = true;
    }

    private IEnumerator Dash()
    {
        _animator.DashOn();
        _animator.SpawnDustParticleEffect(4);
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

    public void Attack()
    {
        if (_currentAttackIndex <= 3 && _canAttack)
        {
            if (_attackCoroutine != null)
            {
                StopCoroutine(_attackCoroutine);
            }
            
            if (_isGrounded)
            {
                _animator.GroundAttackAnimation(_currentAttackIndex);
                _IcanAttack.GroundAttackEnemies();
            }
            else
            {
                _animator.AirAttackAnimation();
                _IcanAttack.AirAttackEnemies();
            }
            _currentAttackIndex++;
            
            _attackCoroutine = StartCoroutine(AttackCooldownAndReset());
            
            if (_currentAttackIndex > 3) 
            {
                _currentAttackIndex = 1;
            }
        }
    }

    private IEnumerator AttackCooldownAndReset()
    {
        _canAttack = false;
        yield return new WaitForSeconds(_config.attackInterval);
        _canAttack = true;
        yield return new WaitForSeconds(_config.resetTime - _config.attackInterval);

        ResetAttack();
    }

    private void ResetAttack()
    {
        _currentAttackIndex = 1;
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
    