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
    [SerializeField] private float _chaseSpeed = 2.5f; //recomended for proper jump movements
    [SerializeField] private float _jumpForce = 0.1f;
    [SerializeField] private float _durationBeforeSwitch = 4;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private bool _canChase = false;
    [SerializeField] private bool _canPatroll = false;
    [SerializeField] private bool _canJump = false;
    [SerializeField] private EnemyState _enemyState = EnemyState.Idle;
    
    private Rigidbody2D _rb;
    private bool _isGrounded;
    private bool _shouldJump1Block;
    private bool _shouldJump2Blocks;
    private float _direction = 0;
    private float _patrollDirection = -1;
    
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
        if (_isGrounded && _shouldJump1Block)
        {
            _shouldJump1Block = false;
            _rb.velocity = Vector2.up * _jumpForce/2;
        }
        
        if (_isGrounded && _shouldJump2Blocks)
        {
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
        RaycastHit2D groundInFront =
            Physics2D.Raycast(transform.position, new Vector2(_direction, 0), 2f, _groundLayer);
        RaycastHit2D wallInFront = Physics2D.Raycast(transform.position+new Vector3(0,0.5f,0),
            (_wallInFrontCheck.position - transform.position).normalized,
            Vector2.Distance(transform.position, _wallInFrontCheck.position), _groundLayer);
        RaycastHit2D noGapAhead = Physics2D.Raycast(_gapCheck.position, Vector2.down, 1.5f, _groundLayer);
        RaycastHit2D groundAfter1BlockGap =
            Physics2D.Raycast(_groundAfter1BlockGapCheck.position, Vector2.down, 1.5f, _groundLayer);
        RaycastHit2D groundAfter2BlockGap =
            Physics2D.Raycast(_groundAfter2BlockGapCheck.position, Vector2.down, 1.5f, _groundLayer);
        DrawRays();

        if ((noGapAhead.collider || ((groundAfter2BlockGap.collider || groundAfter1BlockGap.collider) && _canJump)))
        {
            _rb.velocity = new Vector2(_direction * _chaseSpeed, _rb.velocity.y);
        }
        else if (_isGrounded)
        {
            _rb.velocity = new Vector2(0, 0);
        }

        if (_isGrounded && _canJump && !(wallInFront.collider))
        {
            if (!groundInFront.collider && !noGapAhead.collider && groundAfter1BlockGap.collider ) // Gap detected
            {
                _shouldJump1Block = true;
            }
            else if (!groundInFront.collider && !noGapAhead.collider && groundAfter2BlockGap.collider) // Gap detected
            {
                _shouldJump2Blocks = true;
            }
            else if (groundInFront.collider) 
            {
                _shouldJump2Blocks = true;
            }
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

    private void DrawRays()
    {
        //_isGrounded
        Debug.DrawRay(transform.position, Vector2.down * 0.6f, Color.yellow);
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red; // Draw the gap check ray
        Gizmos.DrawRay(transform.position, (_gapCheck.position - transform.position).normalized * Vector2.Distance(transform.position, _gapCheck.position));
        Gizmos.DrawRay(_gapCheck.position, Vector2.down * 1.5f);
        
        Gizmos.color = Color.blue; // Draw the ground after gap check ray
        Gizmos.DrawRay(transform.position, (_groundAfter2BlockGapCheck.position - transform.position).normalized * Vector2.Distance(transform.position, _groundAfter2BlockGapCheck.position));
        Gizmos.DrawRay(_groundAfter2BlockGapCheck.position, Vector2.down * 1.5f); 
        Gizmos.color = Color.cyan;
        Gizmos.DrawRay(transform.position, (_groundAfter1BlockGapCheck.position - transform.position).normalized * Vector2.Distance(transform.position, _groundAfter1BlockGapCheck.position));
        Gizmos.DrawRay(_groundAfter1BlockGapCheck.position, Vector2.down * 1.5f);
        
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, new Vector2(_direction, 0) * 2f); // Draw the ground in front ray
        Gizmos.DrawRay(transform.position + new Vector3(0, 0.5f, 0), 
            (_wallInFrontCheck.position - (transform.position + new Vector3(0, 0.5f, 0))).normalized * 
            Vector2.Distance(transform.position + new Vector3(0, 0.5f, 0), _wallInFrontCheck.position));
    }
}
