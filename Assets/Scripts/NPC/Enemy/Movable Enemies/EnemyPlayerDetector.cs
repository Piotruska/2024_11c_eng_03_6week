using System.Collections;
using NPC.Enemy.Movable_Enemies;
using NPC.Enemy.Movable_Enemies.Interfaces;
using UnityEngine;

public class EnemyPlayerDetector : MonoBehaviour, IEnemyPlayerDetector
{
    [Header("Detection Configuration")] 
    [SerializeField]
    private float _backDetectionDuration = 2f;
    [SerializeField]
    private float _detectionDelay = 0.1f;
    [SerializeField]
    private float _lingerTime = 2f; // Time to keep detection after losing sight
    [SerializeField]
    private float _detectionDistance = 10f; // Maximum distance the enemy can see
    [SerializeField]
    private float _minDetectionAngle = 10f; // Minimum angle for detection (from the vertical)
    [SerializeField]
    private LayerMask _detectorLayerMask; 
    [SerializeField]
    private LayerMask _groundLayerMask;

    [Header("Gizmos Config")]
    [SerializeField]
    private bool _showDetectionCircle = true; // Show detection circle

    private IEnemyController _enemyController;
    private bool _playerDetected;
    private float _timeSincePlayerLost;

    private Transform _playerTransform;
    private Coroutine _lingerCoroutine;

    private void Awake()
    {
        _enemyController = GetComponent<IEnemyController>();
        _timeSincePlayerLost = 0f;
        
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            _playerTransform = playerObject.transform;
        }
    }

    private void FixedUpdate()
    {
        if (_enemyController.GetState() == EnemyState.Die || _playerTransform == null) 
            return;

        if (!_enemyController.isPlayerAlive()) _playerDetected = false;

        if (_enemyController.CanChase())
        {
            StartCoroutine(DetectionCoroutine());
        }

        
        var state = _enemyController.GetState();
        bool changeStateToChase = _playerDetected && state != EnemyState.Die && _enemyController.CanChase() && _enemyController.isPlayerAlive();
        bool changeStateToPatrol = (!_playerDetected || !_enemyController.isPlayerAlive()) && state != EnemyState.Die && _enemyController.CanPatroll()  ;
        bool changeStateToIdle = (!_playerDetected || !_enemyController.isPlayerAlive()) && state != EnemyState.Die && !_enemyController.CanPatroll();

        if (changeStateToChase)
        {
            if(state != EnemyState.Chase)Debug.Log(state);
            _enemyController.ChangeState(EnemyState.Chase);
        }
        else if (changeStateToPatrol)
        {
            _enemyController.ChangeState(EnemyState.Patrol);
        }
        else if (changeStateToIdle)
        {
            _enemyController.ChangeState(EnemyState.Idle);
        }
    }

    private IEnumerator DetectionCoroutine()
    {
        yield return new WaitForSeconds(_detectionDelay);

        DetectPlayerDirectly();

        if (_playerDetected)
        {
            _timeSincePlayerLost = 0f;
        }
        else
        {
            _timeSincePlayerLost += Time.deltaTime;
            if (_timeSincePlayerLost >= _backDetectionDuration && _enemyController.CanPatroll())
            {
                _enemyController.ChangeState(EnemyState.Patrol);
            }
        }
    }

    private void DetectPlayerDirectly()
    {
        if (!_enemyController.isPlayerAlive())
        {
            _playerDetected = false;
            return;
        }
        
        Vector2 directionToPlayer = (_playerTransform.position - transform.position).normalized;
        float distanceToPlayer = Vector2.Distance(transform.position, _playerTransform.position);
        
        if (distanceToPlayer > _detectionDistance)
        {
            if (_lingerCoroutine == null)
            {
                _lingerCoroutine = StartCoroutine(LingerDetection());
            }
            return;
        }
        float angle = Vector2.Angle(Vector2.up, directionToPlayer);
        if (angle <= _minDetectionAngle || angle >= 180 - _minDetectionAngle)
        {
            _playerDetected = false;
            return;
        }
        
        RaycastHit2D hit = Physics2D.Raycast(transform.position, directionToPlayer, distanceToPlayer, _detectorLayerMask | _groundLayerMask);
    
        if (hit.collider != null && hit.collider.CompareTag("Player"))
        {
            _playerDetected = true;
            
            if (_lingerCoroutine != null)
            {
                StopCoroutine(_lingerCoroutine);
                _lingerCoroutine = null;
            }
        }
        else if (_lingerCoroutine == null)
        {
            _lingerCoroutine = StartCoroutine(LingerDetection());
        }
    }


    private IEnumerator LingerDetection()
    {
        yield return new WaitForSeconds(_lingerTime);
        
        _playerDetected = false;
        _lingerCoroutine = null;
    }

    private void OnDrawGizmos()
    {
        if (_playerTransform == null) return;
        
        Vector3 angleOffsetAbove = Quaternion.Euler(0, 0, _minDetectionAngle) * Vector2.up * _detectionDistance;
        Vector3 angleOffsetBelow = Quaternion.Euler(0, 0, -_minDetectionAngle) * Vector2.up * _detectionDistance;

        Gizmos.color = Color.gray; 
        Gizmos.DrawLine(transform.position, transform.position + angleOffsetAbove);
        Gizmos.DrawLine(transform.position, transform.position + angleOffsetBelow);
        Gizmos.DrawLine(transform.position, transform.position - angleOffsetAbove);
        Gizmos.DrawLine(transform.position, transform.position - angleOffsetBelow);

        
        Vector2 directionToPlayer = (_playerTransform.position - transform.position).normalized;
        float distanceToPlayer = Vector2.Distance(transform.position, _playerTransform.position);

        
        bool isPlayerInRange = distanceToPlayer <= _detectionDistance;

        
        RaycastHit2D hit = Physics2D.Raycast(transform.position, directionToPlayer, Mathf.Min(distanceToPlayer, _detectionDistance), _detectorLayerMask | _groundLayerMask);

        
        Vector3 endPoint;
        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Player"))
            {
                Gizmos.color = Color.green; 
                endPoint = _playerTransform.position;
            }
            else if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                Gizmos.color = Color.yellow; 
                endPoint = hit.point;
            }
            else
            {
                Gizmos.color = Color.red;
                endPoint = _playerTransform.position;
            }
        }
        else
        {
            Gizmos.color = isPlayerInRange ? Color.green : Color.red;
            endPoint = _playerTransform.position;
        }
        
        Gizmos.DrawLine(transform.position, endPoint);
        
        Gizmos.color = isPlayerInRange ? new Color(1, 0, 0, 0.3f) : new Color(0, 1, 0, 0.3f);
        Gizmos.DrawWireSphere(transform.position, _detectionDistance);
    }
}
