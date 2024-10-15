using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    private Rigidbody2D _rb;
   private bool _perform_jump ;
   private bool _isGrounded;
   private int _jumpCount=0;
   [SerializeField] private PlayerConfig _config;
   [SerializeField] private Transform _groundCheck;
   [SerializeField] private float _checkRadious;
   [SerializeField] private LayerMask _whatIsGround; 
   

   private void Awake()
   {
       _rb = GetComponent<Rigidbody2D>();
   }

   private void Update() {
       if (Input.GetButtonDown("Jump") && (_isGrounded || _jumpCount < _config.extraJumpCount))
        {
            _perform_jump = true;
        }
       
       if (_isGrounded)
       {
           _jumpCount = 0;
       }
    }

   private void FixedUpdate()
    {
        _isGrounded = Physics2D.OverlapCircle(_groundCheck.position, _checkRadious, _whatIsGround);
        if (_perform_jump)
        {
            _jumpCount ++;
            _perform_jump = false;
            _rb.AddForce(new Vector2(0, _config.jumpForce), ForceMode2D.Impulse);
        }
    }
}
