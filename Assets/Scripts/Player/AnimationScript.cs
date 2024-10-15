using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

public class AnimationScript : MonoBehaviour , PlayerAnimator
{
    private Rigidbody2D _rb;
    private PlayerMovement _playerMovement;
    private float _xInput;

    private Animator _animator;

    private string _jumpTrigger = "Jump";
    private string _hitTrigger = "Hit";
    private string _DeathTrigger = "Dies";
    private string _runningBool = "isRunning";
    private string _isGroundedBool = "IsGrounded";

    private bool _facingRight = true;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _playerMovement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        _xInput = Input.GetAxis("Horizontal");

        if (_xInput > 0.1f || _xInput < -0.1f)
        {
            _animator.SetBool(_runningBool, true);
        }
        else
        {
            _animator.SetBool(_runningBool, false);
        }
        
        _animator.SetBool(_isGroundedBool, _playerMovement.IsGrounded);
    }

    private void FixedUpdate()
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
        _animator.SetTrigger(_DeathTrigger);
    }

}