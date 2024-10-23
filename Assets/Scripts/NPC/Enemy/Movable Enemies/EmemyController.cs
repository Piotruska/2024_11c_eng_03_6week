using System.Collections;
using System.Collections.Generic;
using NPC.Enemy.Movable_Enemies;
using UnityEngine;

public class EmemyController : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private Transform _gapCheck;
    [SerializeField] private Transform _groundAfter1BlockGapCheck;
    [SerializeField] private Transform _groundAfter2BlockGapCheck;
    [SerializeField] private Transform _wallInFrontCheck;
    [SerializeField] private Transform _groundAfter2blocksGap1BlockAboveCheck;
    [SerializeField] private Transform _groundInFrontCheck;
    [SerializeField] private float _chaseSpeed = 2.5f; //recomended for proper jump movements
    [SerializeField] private float _jumpForce = 5f;
    [SerializeField] private float _durationBeforeSwitch = 20;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private bool _canChase = false;
    [SerializeField] private bool _canPatroll = false;
    [SerializeField] private bool _canJump = false;
    [SerializeField] private EnemyState _enemyState = EnemyState.Idle;
    
    private Rigidbody2D _rb;
    private bool _isGrounded;
    private bool _shouldJump1Block;
    private bool _shouldJump2Blocks;
    private bool _isJumping = false;
    private float _direction = 1;
    private float _patrollDirection = 1;
    
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        StartCoroutine(PatrolTimer(_durationBeforeSwitch));
    }

    void Update()
    {
        Chase();
        Patrol();
        Move();
        Flip();
    }

    private void FixedUpdate()
    {
        if (_isGrounded)
        {
            _isJumping = false;
            
        }
        
        if (_shouldJump1Block)
        {
            _isJumping = true;
            _shouldJump1Block = false;
            _rb.velocity = Vector2.up * _jumpForce/2;
        }
        
        if (_shouldJump2Blocks)
        {
            _isJumping = true;
            _shouldJump2Blocks = false;
            _rb.velocity = Vector2.up * _jumpForce;
        }
    }

    private IEnumerator PatrolTimer(float durationBeforeSwitch)
    {
        while (_canPatroll)
        {
            yield return new WaitForSeconds(durationBeforeSwitch);
            _patrollDirection *= -1; 
        }
        if (!_canPatroll) _direction = 0;
    }

    private void Patrol()
    {
        if (_enemyState == EnemyState.Patrol && _canPatroll)
        {
            _direction = _patrollDirection;
        }
    }
    
    private void Chase()
    {
        if (_enemyState == EnemyState.Chase && _canChase)
        {
            _direction = Mathf.Sign(_player.position.x - transform.position.x);
        }
    }

    private void Move()
    {
        _isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 0.6f, _groundLayer);
        
        RaycastHit2D groundInFront =Physics2D.Raycast(transform.position,
            (_groundInFrontCheck.position - transform.position).normalized,
            Vector2.Distance(transform.position, _groundInFrontCheck.position), _groundLayer);
        
        RaycastHit2D wallInFront = Physics2D.Raycast(transform.position+new Vector3(0,0.5f,0),
            (_wallInFrontCheck.position - transform.position).normalized,
            Vector2.Distance(transform.position, _wallInFrontCheck.position), _groundLayer);
        
        RaycastHit2D groundAfter2blocksGap1BlockAbove = 
            Physics2D.Raycast(_groundAfter2blocksGap1BlockAboveCheck.position, Vector2.down, 0.5f, _groundLayer);
        
        RaycastHit2D noGapAhead = Physics2D.Raycast(_gapCheck.position, Vector2.down, 1.5f, _groundLayer);
        
        RaycastHit2D groundAfter1BlockGap =
            Physics2D.Raycast(_groundAfter1BlockGapCheck.position, Vector2.down, 1.5f, _groundLayer);
        
        RaycastHit2D groundAfter2BlockGap =
            Physics2D.Raycast(_groundAfter2BlockGapCheck.position, Vector2.down, 1.5f, _groundLayer);
        


        // bool shouldBeAbleToJump = _isGrounded && _canJump && !wallInFront.collider;
        // bool shouldJumpIfGroundInFront = groundInFront.collider;
        // bool shouldBeMoving = noGapAhead.collider || ((groundAfter2BlockGap.collider || groundAfter1BlockGap.collider) && _canJump);
        // bool shouldJump1Block = !groundInFront.collider && !noGapAhead.collider && groundAfter1BlockGap.collider ;
        // bool shouldJump2Blocks = !groundInFront.collider && !noGapAhead.collider && groundAfter2BlockGap.collider;
        //
        // if (shouldBeMoving)
        // {
        //     _rb.velocity = new Vector2(_direction * _chaseSpeed, _rb.velocity.y);
        // }
        // else if (_isGrounded)
        // {
        //     _rb.velocity = new Vector2(0, 0);
        // }
        //
        // if (shouldBeAbleToJump)
        // {
        //     if (shouldJump1Block) // Gap detected
        //     {
        //         _shouldJump1Block = true;
        //     }
        //     else if (shouldJump2Blocks || shouldJumpIfGroundInFront)
        //     {
        //         _shouldJump2Blocks = true;
        //     }
        // }
        
        
        bool shouldBeAbleToJump = _canJump && _isGrounded && !wallInFront; //true
        bool shouldJumpIfGroundInFront = shouldBeAbleToJump && groundInFront && !wallInFront && !noGapAhead; 
        bool shouldJump1BlockX  = shouldBeAbleToJump && (!noGapAhead && groundAfter1BlockGap);
        bool shouldJump2BlocksX = shouldBeAbleToJump && (!noGapAhead && groundAfter2BlockGap);
        bool shouldJump2BlocksX1BlockY = shouldBeAbleToJump && (!noGapAhead && groundAfter2blocksGap1BlockAbove);
        bool groundInFrontOfPlayer = noGapAhead;
        bool shouldBeMovingFullSpeed  = _isJumping || (groundInFrontOfPlayer && !shouldJumpIfGroundInFront ) ;
        bool shouldBeMovingHalfSpeed = (groundInFrontOfPlayer && shouldJumpIfGroundInFront ) ;
        
        //TODO : Add so that if close to wall then dont move
        
        //TODO : Steps with drop suicide problem fix
        
        if (shouldBeMovingHalfSpeed)
        {
            _rb.velocity = new Vector2(_direction * _chaseSpeed/3, _rb.velocity.y);
        } else if (shouldBeMovingFullSpeed)
        {
            _rb.velocity = new Vector2(_direction * _chaseSpeed, _rb.velocity.y);
        }else if (_isGrounded)
        {
                _rb.velocity = new Vector2(0, 0);
        }
        
        if (shouldJump1BlockX)
        {
            _shouldJump1Block = true;
        }else if ((shouldJumpIfGroundInFront && !noGapAhead ) || shouldJump2BlocksX || shouldJump2BlocksX1BlockY)
        {
            _shouldJump2Blocks = true;
        }
        
        
        
        
        
    }

    private void Flip()
    {
        if (_direction != 0)
        {
            Vector3 localScale = transform.localScale;
            localScale.x = Mathf.Abs(localScale.x) * Mathf.Sign(_direction); 
            transform.localScale = localScale; 
        }
    }

    public void OnDrawGizmos()
    {
        
        //_isGrounded
        Debug.DrawRay(transform.position, Vector2.down * 0.6f, Color.yellow);
        
        var position = transform.position;
        
        Gizmos.color = Color.red; // Draw the gap check ray
        Gizmos.DrawRay(position, (_gapCheck.position - position).normalized * Vector2.Distance(transform.position, _gapCheck.position));
        Gizmos.DrawRay(_gapCheck.position, Vector2.down * 1.5f);
        
        Gizmos.color = Color.blue; // Draw the ground after gap check ray
        Gizmos.DrawRay(position, (_groundAfter2BlockGapCheck.position - position).normalized * Vector2.Distance(transform.position, _groundAfter2BlockGapCheck.position));
        Gizmos.DrawRay(_groundAfter2BlockGapCheck.position, Vector2.down * 1.5f); 
        Gizmos.color = Color.cyan;
        Gizmos.DrawRay(position, (_groundAfter1BlockGapCheck.position - position).normalized * Vector2.Distance(transform.position, _groundAfter1BlockGapCheck.position));
        Gizmos.DrawRay(_groundAfter1BlockGapCheck.position, Vector2.down * 1.5f);
        
        Gizmos.color = Color.green;
        Gizmos.DrawRay(position, (_groundInFrontCheck.position - position).normalized * Vector2.Distance(transform.position, _groundInFrontCheck.position));
        // Draw the ground in front ray
        
        Gizmos.DrawRay(position + new Vector3(0, 0.5f, 0), 
            (_wallInFrontCheck.position - (position + new Vector3(0, 0.5f, 0))).normalized * 
            Vector2.Distance(position + new Vector3(0, 0.5f, 0), _wallInFrontCheck.position));
        
        Gizmos.color = Color.magenta;
        Gizmos.DrawRay(position,
            (_groundAfter2blocksGap1BlockAboveCheck.position - position).normalized * Vector2.Distance(transform.position, _groundAfter2blocksGap1BlockAboveCheck.position));
        Gizmos.DrawRay(_groundAfter2blocksGap1BlockAboveCheck.position, Vector2.down * 0.5f); 
    }
}
