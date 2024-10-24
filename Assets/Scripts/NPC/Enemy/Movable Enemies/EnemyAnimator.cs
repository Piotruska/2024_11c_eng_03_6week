using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour , IEnemyAnimator
{

    private string _isRunningBool = "isRunning";
    private string _isGroundedBool = "isGrounded";
    
    private string _jumpTrigger = "Jump";
    private string _attackTrigger= "Attack";
    private string _hitTrigger = "Hit";
    private string _deadHitTrigger = "DeadHit";
    private string _anticipationTrigger = "Anticipation";

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
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

    public void SpawnDustParticles()
    {
        
    }
}
