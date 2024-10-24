using System.Collections;
using System.Collections.Generic;
using NPC.Enemy.Movable_Enemies;
using NPC.Enemy.Movable_Enemies.Interfaces;
using UnityEngine;

public class EnemyController : MonoBehaviour, IEnemyController
{
    [Header("Movement Rays Transforms")]
    [SerializeField] private Transform _player;
    [SerializeField] private Transform _gapCheck; 
    [SerializeField] private Transform _groundAfter1BlockGapCheck; 
    [SerializeField] private Transform _groundAfter2BlockGapCheck; 
    [SerializeField] private Transform _wallInFrontCheck; 
    [SerializeField] private Transform _groundAfter2blocksGap1BlockAboveCheck; 
    [SerializeField] private Transform _groundInFrontCheck; 
    [Header("Movement Rays Config")]
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private LayerMask _dangerLayer;
    
    [Header("Behaviour")]
    [SerializeField] public bool _canChase = false;
    [SerializeField] public bool _canPatroll = false;
    [SerializeField] public bool _canJump = false;

    [Header("Patroll Configurations")] 
    [SerializeField] public bool _groundDetectionBased =false;
    [SerializeField] public bool _timeBased =false;
    [Range(0f, 20f)]
    [SerializeField] public float _durationBeforeSwitch = 20;
    
    [SerializeField] private EnemyState _enemyState = EnemyState.Idle;
    
    [Header("Gizmos Options")]
    [SerializeField] private bool _showMovementGizmos = true;

    private IEnemyAnimator _enemyAnimator;
    
    private float _chaseSpeed = 2.5f; //recomended for proper jump movements
    private float _jumpForce = 5f; //recomended for proper jump movements
    private Rigidbody2D _rb;
    private bool _isGrounded;
    private bool _shouldJump1Block;
    private bool _shouldJump2Blocks;
    private bool _isJumping = false;
    private float _direction = 1;
    private float _patrollDirection = 1;
    private bool changeDirectionDuringChase = false;

    public bool CanChase()
    {
        return _canChase;
    }

    public bool CanPatroll()
    {
        return _canPatroll;
    }

    public EnemyState GetState()
    {
        return _enemyState;
    }

    public float Direction()
    {
        return _direction;
    }

    void Start()
    {
        _enemyAnimator = GetComponent<IEnemyAnimator>();
        _rb = GetComponent<Rigidbody2D>();
        StartCoroutine(PatrolTimer(_durationBeforeSwitch));
    }

    void Update()
    {
        if (_enemyState == EnemyState.Die) return;
        Move();
        CorrectLocalScale();
    }

    private void FixedUpdate()
    {
        _isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 0.6f, _groundLayer);
        _enemyAnimator.IsGrounded(_isGrounded);
        _enemyAnimator.IsJumping(_isJumping);

        if (_isGrounded) _isJumping = false;
        
        if (_shouldJump1Block )
        {
            _enemyAnimator.JumpAnimation();
            _isJumping = true;
            _shouldJump1Block = false;
            _rb.velocity = Vector2.up * _jumpForce/2;
        }
        
        if (_shouldJump2Blocks)
        {
            _enemyAnimator.JumpAnimation();
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
        if (_enemyState == EnemyState.Patrol && _canPatroll && _timeBased && !_groundDetectionBased)
        {
            StartCoroutine(PatrolTimer(_durationBeforeSwitch));
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
            var playerDirection= Mathf.Sign(_player.position.x - transform.position.x);
            if (playerDirection != _direction)
            {
                changeDirectionDuringChase = true;
            }
            else
            {
                changeDirectionDuringChase = false;
            }
            
            if (changeDirectionDuringChase && _isGrounded)
            {
                _direction = playerDirection;
            }
        }

        
    }

    private void Move()
    {
        
        
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
            _enemyAnimator.RunAnimation();
        }
        if ((( shouldStopIfCloseToWall || shouldStopIfCloseToCliff) 
            && (isPatrolling || isChasing)) || isIdle || isDead)
        {
                _rb.velocity = new Vector2(0, _rb.velocity.y);
                _enemyAnimator.IdleAnimation();
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
