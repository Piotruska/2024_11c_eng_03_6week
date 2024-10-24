using System.Collections;
using System.Collections.Generic;
using NPC.Enemy.Movable_Enemies;
using NPC.Enemy.Movable_Enemies.Interfaces;
using UnityEngine;

public class EmemyController : MonoBehaviour, IEnemyController
{
    [Header("Movement Rays Transforms")]
    [SerializeField] private Transform _player;
    [SerializeField] private Transform _gapCheck; // x : 1 , y : 0.-76
    [SerializeField] private Transform _groundAfter1BlockGapCheck; // x : 2.8 , y : -0.83
    [SerializeField] private Transform _groundAfter2BlockGapCheck; // x : 4.55 , y : -0.81
    [SerializeField] private Transform _wallInFrontCheck; // x : 1.64 , y : 2.13
    [SerializeField] private Transform _groundAfter2blocksGap1BlockAboveCheck; // x : 3.48 , y : 1.54
    [SerializeField] private Transform _groundInFrontCheck; // x : 2.65  , y : 0
    [Header("Movement Rays Config")]
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private LayerMask _dangerLayer;
    
    [Header("Behaviour")]
    [SerializeField] private bool _canChase = false;
    [SerializeField] private bool _canPatroll = false;
    [SerializeField] private bool _canJump = false;

    [Header("Patroll Configurations")] 
    [SerializeField] private bool _groundDetectionBased =false;
    [SerializeField] private bool _timeBased =false;
    [Range(0f, 20f)]
    [SerializeField] private float _durationBeforeSwitch = 20;
    
    [Header("Starting Phase")]
    [SerializeField] private EnemyState _enemyState = EnemyState.Idle;
    
    [Header("Gizmos Options")]
    [SerializeField] private bool _showMovementGizmos = true;
    
    private float _chaseSpeed = 2.5f; //recomended for proper jump movements
    private float _jumpForce = 5f; //recomended for proper jump movements
    private Rigidbody2D _rb;
    private bool _isGrounded;
    private bool _shouldJump1Block;
    private bool _shouldJump2Blocks;
    private bool _isJumping = false;
    private float _direction = 1;
    private float _patrollDirection = 1;

    public bool CanChase()
    {
        return _canChase;
    }

    public float Direction()
    {
        return _direction;
    }

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        StartCoroutine(PatrolTimer(_durationBeforeSwitch));
    }

    void Update()
    {
        Move();
        CorrectLocalScale();
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
        while (true)
        {
            yield return new WaitForSeconds(durationBeforeSwitch);
            if (_enemyState == EnemyState.Patrol && _canPatroll && _timeBased && !_groundDetectionBased)
            {
                _direction *= -1;
            }
        }
    }

    private void Patrol(bool changeDirection)
    {
        if (_enemyState == EnemyState.Patrol && _canPatroll && _timeBased)
        {
            
        }
        
        if (_enemyState == EnemyState.Patrol && _canPatroll && _groundDetectionBased && !_timeBased && changeDirection)
        {
            _direction *= -1;
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
        
        RaycastHit2D noGapAhead = Physics2D.Raycast(_gapCheck.position, Vector2.down, 1.8f, _groundLayer);
        RaycastHit2D dangerAhead = Physics2D.Raycast(_gapCheck.position, Vector2.down, 1.8f, _dangerLayer);
        
        RaycastHit2D groundAfter1BlockGap =
            Physics2D.Raycast(_groundAfter1BlockGapCheck.position, Vector2.down, 1.8f, _groundLayer);
        RaycastHit2D dangerAfter1BlockGap =
            Physics2D.Raycast(_groundAfter1BlockGapCheck.position, Vector2.down, 1.8f, _dangerLayer);
        
        RaycastHit2D groundAfter2BlockGap =
            Physics2D.Raycast(_groundAfter2BlockGapCheck.position, Vector2.down, 1.8f, _groundLayer);
        RaycastHit2D dangerAfter2BlockGap =
            Physics2D.Raycast(_groundAfter2BlockGapCheck.position, Vector2.down, 1.8f, _dangerLayer);

        
        RaycastHit2D groundAfter2blocksGap1BlockAbove = 
            Physics2D.Raycast(_groundAfter2blocksGap1BlockAboveCheck.position, Vector2.down, 0.6f, _groundLayer);

        
        bool isPatrolling = _enemyState == EnemyState.Patrol;
        bool isChasing = _enemyState == EnemyState.Chase;
        bool isIdle = _enemyState == EnemyState.Idle;
        bool isStunned = _enemyState == EnemyState.Stunned;
        bool isDead = _enemyState == EnemyState.Die;
        
        bool noGroundAhead = (!groundAfter1BlockGap && !groundAfter2BlockGap && !groundAfter2blocksGap1BlockAbove && !noGapAhead && !wallInFront && !groundInFront) ||
                             (dangerAfter1BlockGap && dangerAfter2BlockGap && dangerAhead);
        bool gapAhead = !noGapAhead;
        
        bool shouldBeAbleToJump = (_canJump && _isGrounded && !wallInFront);
        bool shouldJumpIfGroundInFront = (shouldBeAbleToJump && groundInFront && !wallInFront && !noGapAhead); 
        bool shouldJump1BlockX  = shouldBeAbleToJump && (gapAhead || dangerAhead) && groundAfter1BlockGap && !dangerAfter1BlockGap ;
        bool shouldJump2BlocksX = shouldBeAbleToJump && (gapAhead || dangerAhead) && groundAfter2BlockGap && !dangerAfter2BlockGap ;
        bool shouldJump2BlocksX1BlockY = (shouldBeAbleToJump && (gapAhead&& groundAfter2blocksGap1BlockAbove));
        
        bool shouldStopIfCloseToWall = (wallInFront && (groundInFront.distance < 0.5f));
        bool shouldStopIfCloseToCliff = (_canJump && (_isGrounded && noGroundAhead) || 
                                         (_canPatroll && !_canJump && (gapAhead || dangerAhead)));
        bool shouldBeMovingFullSpeed  = (_canJump && _canPatroll && (_isJumping  || (noGapAhead && !shouldJumpIfGroundInFront ))) ||
                                        (_canPatroll && !_canJump && (noGapAhead));
        
        
        if (shouldBeMovingFullSpeed && (isPatrolling || isChasing))
        {
            _rb.velocity = new Vector2(_direction * _chaseSpeed, _rb.velocity.y);
        }
        if ((( shouldStopIfCloseToWall || shouldStopIfCloseToCliff) 
            && (isPatrolling || isChasing)) || isIdle || isDead)
        {
                _rb.velocity = new Vector2(0, _rb.velocity.y);
        }
        
        if (shouldJump1BlockX 
            && (isPatrolling || isChasing))
        {
            _shouldJump1Block = true;
        }
        if (((shouldJumpIfGroundInFront && !noGapAhead ) || shouldJump2BlocksX || shouldJump2BlocksX1BlockY)
            && (isPatrolling || isChasing))
        {
            _shouldJump2Blocks = true;
        }
        
        Chase();
        Patrol(shouldStopIfCloseToWall || shouldStopIfCloseToCliff);
    }

    private void CorrectLocalScale()
    {
        Vector3 localScale = transform.localScale;
        localScale.x = Mathf.Abs(localScale.x) * Mathf.Sign(_direction); 
        transform.localScale = localScale;
    }

    public void ChangeState(EnemyState state)
    {
        if (_enemyState != EnemyState.Die)
        {
            _enemyState = state;
        }
    }

    public void OnDrawGizmos()
    {
        if (_showMovementGizmos)
        {
            //_isGrounded
            Debug.DrawRay(transform.position, Vector2.down * 0.6f, Color.yellow);
        
            var position = transform.position;
        
            Gizmos.color = Color.red; // Draw the gap check ray
            Gizmos.DrawRay(position, (_gapCheck.position - position).normalized * Vector2.Distance(transform.position, _gapCheck.position));
            Gizmos.DrawRay(_gapCheck.position, Vector2.down * 1.8f);
        
            Gizmos.color = Color.blue; // Draw the ground after gap check ray
            Gizmos.DrawRay(position, (_groundAfter2BlockGapCheck.position - position).normalized * Vector2.Distance(transform.position, _groundAfter2BlockGapCheck.position));
            Gizmos.DrawRay(_groundAfter2BlockGapCheck.position, Vector2.down * 1.8f); 
        
            Gizmos.color = Color.cyan;
            Gizmos.DrawRay(position, (_groundAfter1BlockGapCheck.position - position).normalized * Vector2.Distance(transform.position, _groundAfter1BlockGapCheck.position));
            Gizmos.DrawRay(_groundAfter1BlockGapCheck.position, Vector2.down * 1.8f);
        
            Gizmos.color = Color.magenta;
            Gizmos.DrawRay(position,
                (_groundAfter2blocksGap1BlockAboveCheck.position - position).normalized * Vector2.Distance(transform.position, _groundAfter2blocksGap1BlockAboveCheck.position));
            Gizmos.DrawRay(_groundAfter2blocksGap1BlockAboveCheck.position, Vector2.down * 0.6f); 
        
            Gizmos.color = Color.green;
            Gizmos.DrawRay(position, (_groundInFrontCheck.position - position).normalized * Vector2.Distance(transform.position, _groundInFrontCheck.position));
            // Draw the ground in front ray
        
            Gizmos.DrawRay(position + new Vector3(0, 0.5f, 0), 
                (_wallInFrontCheck.position - (position + new Vector3(0, 0.5f, 0))).normalized * 
                Vector2.Distance(position + new Vector3(0, 0.5f, 0), _wallInFrontCheck.position));
        }
        
        
        
    }
}
