using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PllayerJump : MonoBehaviour
{
    private Rigidbody2D rb;
   private bool perform_jump ;
   private bool isGrounded;
   private int jumpCount=0;
   [SerializeField] private PlayerConfig _config;
   

   private void Awake()
   {
       rb = GetComponent<Rigidbody2D>();
   }

   private void Update() {
       if (Input.GetButtonDown("Jump") && (isGrounded || jumpCount < _config.maxJumpCount))
        {
            perform_jump = true;
        }
    }

   private void FixedUpdate()
    {
        if (perform_jump)
        {
              jumpCount ++;
                perform_jump = false;
                rb.AddForce(new Vector2(0, _config.jumpForce), ForceMode2D.Impulse);
            if (jumpCount >=1) { isGrounded = false; }
        }
    }

     private void OnCollisionEnter2D(Collision2D collision)
    {
        isGrounded = true;
        jumpCount = 0;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        
    }

}
