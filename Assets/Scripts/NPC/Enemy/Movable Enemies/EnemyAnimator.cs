using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour , IEnemyAnimator
{
    [SerializeField] private GameObject _dustParticleEffect;
    [SerializeField] private Transform _dustparticleSpawn;
    private Rigidbody2D _rb;

    private string _isRunningBool = "isRunning";
    private string _isGroundedBool = "isGrounded";
    private string _isJumpingBool = "isJumping";
    
    private string _jumpTrigger = "Jump";
    private string _attackTrigger= "Attack";
    private string _hitTrigger = "Hit";
    private string _deadHitTrigger = "DeadHit";
    private string _anticipationTrigger = "Anticipation";
    
    private string _runDustEffectTrigger = "RunDustEffect";
    private string _jumpDustEffectTrigger = "JumpDustEffect";
    private string _fallDustEffectTrigger = "FallDustEffect";
    private string _dashDustEffectTrigger = "DashDustEffect";

    private bool _isGrounded;
    private Animator _animator;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }
    
    public void IsGrounded(bool isGrounded)
    {
        _animator.SetBool(_isGroundedBool,isGrounded);
    }
    
    public void IsJumping(bool value)
    {
        _animator.SetBool(_isJumpingBool,false);
    }

    public void IdleAnimation()
    {
        _animator.SetBool(_isRunningBool,false);
    }

    public void RunAnimation()
    {
        _animator.SetBool(_isRunningBool,true);
    }

    public void JumpAnimation()
    {
        _animator.SetTrigger(_jumpTrigger);
        
    }

    public void ResetJumpTrigger()
    {
        _animator.ResetTrigger(_jumpTrigger);
    }

    

    public void AnticipateAnimation()
    {
        _animator.SetTrigger(_anticipationTrigger);
    }
    
    public void AttackAnimation()
    {
        _animator.SetTrigger(_attackTrigger);
    }

    public void HitAnimation()
    {
        _animator.SetTrigger(_hitTrigger);
    }

    public void DeadHitAnimation()
    {
        _animator.SetTrigger(_deadHitTrigger);
    }

    public void SpawnDustParticleEffect(int trigger)
    {
        var obj = Instantiate(_dustParticleEffect, _dustparticleSpawn.position, _dustparticleSpawn.rotation);
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
        }
    }
}
