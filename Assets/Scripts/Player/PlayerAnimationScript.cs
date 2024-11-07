using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

public class PlayerAnimationScript : MonoBehaviour , IPlayerAnimator
{
    [SerializeField] private GameObject _dustparticleEffects;
    
    private Rigidbody2D _rb;
    private PlayerController _playerController;
    private float _xInput;

    private Animator _animator;

    private string _jumpTrigger = "Jump";
    private string _hitTrigger = "Hit";
    private string _deathTrigger = "Dies";
    private string _attack1Trigger = "Attack1";
    private string _attack2Trigger = "Attack2";
    private string _attack3Trigger = "Attack3";
    private string _airAttackTrigger = "AirAttack";
    private string _throwTrigger = "Throw";
    private string _respawnTrigger = "Respawn";
    private string _runningBool = "isRunning";
    private string _dashBool = "Dash";
    private string _isGroundedBool = "IsGrounded";
    
    private string _withSwordLayerName = "With Sword";
    private int _withSwordLayerIndex;

    private string _runDustEffectTrigger = "RunDustEffect";
    private string _jumpDustEffectTrigger = "JumpDustEffect";
    private string _fallDustEffectTrigger = "FallDustEffect";
    private string _dashDustEffectTrigger = "DashDustEffect";
    
    public bool _facingRight = true;
    private bool _sword = false;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _playerController = GetComponent<PlayerController>();
        _withSwordLayerIndex = _animator.GetLayerIndex(_withSwordLayerName);
    }

    void Update()
    {
        _xInput = Input.GetAxis(InputManager.XPlayerMovement);
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
    
    public void Flip()
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

    public void GroundAttackAnimation(int attack)
    {
        switch (attack)
        {
            case 1 :
                _animator.SetTrigger(_attack1Trigger);
                break;
            case 2:
                _animator.SetTrigger(_attack2Trigger);
                break;
            case 3:
                _animator.SetTrigger(_attack3Trigger);
                break;
        }
        
    }

    public void AirAttackAnimation()
    {
        _animator.SetTrigger(_airAttackTrigger);
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

    public void SpawnDustParticleEffect(int trigger)
    {
        var obj = Instantiate(_dustparticleEffects, _rb.transform.position, _rb.transform.rotation);
        obj.transform.localScale = _rb.transform.localScale;
        var particleAnimator = obj.GetComponent<Animator>();
        switch (trigger)
        {
            case 1: //run
                particleAnimator.SetTrigger(_runDustEffectTrigger);
                break;
            case 2: //jump
                particleAnimator.SetTrigger(_jumpDustEffectTrigger);
                break;
            case 3 : //fall
                particleAnimator.SetTrigger(_fallDustEffectTrigger);
                break;
            case 4 : //dash
                particleAnimator.SetTrigger(_dashDustEffectTrigger);
                break;
        }
    }
}