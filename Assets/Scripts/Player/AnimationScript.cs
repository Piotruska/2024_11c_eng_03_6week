using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

public class AnimationScript : MonoBehaviour , IPlayerAnimator
{
    private Rigidbody2D _rb;
    private PlayerController _playerController;
    private float _xInput;

    private Animator _animator;

    private string _jumpTrigger = "Jump";
    private string _hitTrigger = "Hit";
    private string _deathTrigger = "Dies";
    private string _attackTrigger = "Attack";
    private string _throwTrigger = "Throw";
    private string _respawnTrigger = "Respawn";
    private string _runningBool = "isRunning";
    private string _isGroundedBool = "IsGrounded";
    
    private string _withSwordLayerName = "With Sword";
    
    private int _withSwordLayerIndex;
    
    private bool _facingRight = true;
    private bool _sword;

    void Awake()
    {
        
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _playerController = GetComponent<PlayerController>();
        _withSwordLayerIndex = _animator.GetLayerIndex(_withSwordLayerName);
        HasSword(true);
    }

    void Update()
    {
        _xInput = Input.GetAxis("Horizontal");
        _animator.SetBool(_isGroundedBool, _playerController.IsGrounded);
    }

    private void FixedUpdate()
    {
        
    }
    
    
    public void HasSword(bool hasSword)
    {
        if (hasSword)
        {
            _animator.SetLayerWeight(_withSwordLayerIndex,1);
        }
        else
        {
            _animator.SetLayerWeight(_withSwordLayerIndex,0);
        }
        _sword = hasSword;
    }

    public void FacingCheck()
    {
        if (_facingRight == false && _xInput > 0)
        {
            Flip();
        }
        else if (_facingRight == true && _xInput < 0)
        {
            Flip();
        }
    }
    
    private void Flip()
    {
        _facingRight = !_facingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }
    
    public void Idle()
    {
        _animator.SetBool(_runningBool, false);
    }

    public void Walk()
    {
        _animator.SetBool(_runningBool, true);
    }

    public void Jump()
    {
        _animator.SetTrigger(_jumpTrigger);
    }
    
    public void Hit()
    {
        _animator.SetTrigger(_hitTrigger);
    }
    
    public void Death()
    {
        _animator.SetLayerWeight(_withSwordLayerIndex,0);
        _animator.SetTrigger(_deathTrigger);
    }

    public void Attack()
    {
        _animator.SetTrigger(_attackTrigger);
    }

    public void ThrowSword()
    {
        _animator.SetTrigger(_throwTrigger);
    }

    public void Respawn()
    {
        if (_sword)
        {
            _animator.SetLayerWeight(_withSwordLayerIndex,1);
        }else {
            _animator.SetLayerWeight(_withSwordLayerIndex,0);
        }
        _animator.SetTrigger(_respawnTrigger);
    }
}