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
    private string _dashBool = "Dash";
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
    }

    void Update()
    {
        _xInput = Input.GetAxis("Horizontal Movement");
        _animator.SetBool(_isGroundedBool, _playerController.IsGrounded);
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
    
    public void IdleAnimation()
    {
        _animator.SetBool(_runningBool, false);
        _animator.SetBool(_dashBool, false);
    }

    public void WalkAnimation()
    {
        _animator.SetBool(_runningBool, true);
    }

    public void JumpAnimation()
    {
        _animator.SetTrigger(_jumpTrigger);
    }

    public void DashOn()
    {
        _animator.SetBool(_dashBool, true);
    }

    public void HitAnimation()
    {
        _animator.SetTrigger(_hitTrigger);
    }
    
    public void DeathAnimation()
    {
        _animator.SetLayerWeight(_withSwordLayerIndex,0);
        _animator.SetTrigger(_deathTrigger);
    }

    public void AttackAnimation()
    {
        _animator.SetTrigger(_attackTrigger);
    }

    public void ThrowSwordAnimation()
    {
        _animator.SetTrigger(_throwTrigger);
    }

    public void RespawnAnimation()
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