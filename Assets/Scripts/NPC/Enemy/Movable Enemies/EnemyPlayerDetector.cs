using System.Collections;
using NPC.Enemy.Movable_Enemies;
using NPC.Enemy.Movable_Enemies.Interfaces;
using UnityEngine;

public class EnemyPlayerDetector : MonoBehaviour, IEnemyPlayerDetector
{
    
    [Header("RayCast / OverlapBox Configuration - Front")] 
    [SerializeField]
    [Range(0f, 15f)]
    private float _frontXAxisMaxDistance = 6; 
    [SerializeField]
    [Range(0f, 15f)]
    private float _frontYAxisMaxDistance = 4; 
    [SerializeField]
    [Range(0f, 15f)]
    private int _frontXAxisBoxAmount = 12; 
    [SerializeField]
    [Range(0f, 15f)]
    private int _frontYAxisBoxAmount = 8; 

    [Header("RayCast / OverlapBox Configuration - Back")] 
    [SerializeField]
    [Range(0f, 15f)]
    private float _backXAxisMaxDistance = 6; 
    [SerializeField]
    [Range(0f, 15f)]
    private float _backYAxisMaxDistance = 4; 
    [SerializeField]
    [Range(0f, 15f)]
    private int _backXAxisBoxAmount = 12; 
    [SerializeField]
    [Range(0f, 15f)]
    private int _backYAxisBoxAmount = 8; 

    [Header("Detection Configuration")] 
    [SerializeField]
    [Range(-5f, 5f)]
    private float _frontYOffset = -0.35f; // Y offset for front detection
    [SerializeField]
    [Range(-5f, 5f)]
    private float _frontXOffset = -0.79f; // X offset for front detection
    [SerializeField]
    [Range(-5f, 5f)]
    private float _backYOffset = -0.35f; // Y offset for back detection
    [SerializeField]
    [Range(-5f, 5f)]
    private float _backXOffset = -0.79f; // X offset for back detection
    [SerializeField]
    private float _backDetectionDuration = 2f;
    [SerializeField]
    private float _detectionDelay = 0.1f; 
    [SerializeField]
    private LayerMask _detectorLayerMask; 
    [SerializeField]
    private LayerMask _groundLayerMask; 

    [Header("Gizmos Config")] 
    [SerializeField]
    private bool _showGizmos;
    [SerializeField]
    private bool _showGrid; 
    [SerializeField]
    private Color _gizmoGridColour = Color.gray; 
    [SerializeField]
    private bool _showXYLimits; 
    [SerializeField]
    private Color _gizmoXYLimitsColour = Color.blue; 
    [SerializeField]
    private bool _showPlayerDetection;
    [SerializeField]
    private Color _gizmoIdleColour = Color.green; 
    [SerializeField]
    private Color _gizmoDetectedColour = Color.red;
    [SerializeField]
    private bool _showGroundDetection;
    [SerializeField]
    private Color _gizmoRayColour = Color.yellow; 

    
    
    private IEnemyController _enemyController;
    private bool _playerDetected;
    private bool _playerDetectedFront;
    private bool _playerDetectedBack;
    private float _innerxAxisMaxDistance;
    private float _innerxOffset;
    private bool _canLookBack;
    private float _timeSincePlayerLost;

    private void Awake()
    {
        _enemyController = GetComponent<IEnemyController>();
        _timeSincePlayerLost = 0f;
    }

     private void Update()
    {
        // Update logic to manage detection and state transitions
        _innerxAxisMaxDistance = Mathf.Abs(_enemyController.Direction() > 0 ? _frontXAxisMaxDistance : _backXAxisMaxDistance);
        float direction = _enemyController.Direction();
        _innerxOffset = direction > 0 ? _frontXOffset : _backXOffset;

        if (_enemyController.CanChase())
        {
            StartCoroutine(DetectionCoroutine());
        }

        // State management based on player detection
        var state = _enemyController.GetState();
        bool changeStateToChase = _playerDetected && state != EnemyState.Die && _enemyController.CanChase();
        bool changeStateToPatrol = !_playerDetected && state != EnemyState.Die && _enemyController.CanPatroll();
        bool changeStateToIdle = !_playerDetected && state != EnemyState.Die && !_enemyController.CanPatroll();

        if (changeStateToChase)
        {
            _enemyController.ChangeState(EnemyState.Chase);
        }
        else if (changeStateToIdle)
        {
            _enemyController.ChangeState(EnemyState.Idle);
        }
        else if (changeStateToPatrol)
        {
            _enemyController.ChangeState(EnemyState.Patrol);
        }
    }

    private IEnumerator DetectionCoroutine()
    {
        yield return new WaitForSeconds(_detectionDelay);

        DetectPlayerFront();

        if (_playerDetectedFront)
        {
            
            _timeSincePlayerLost = 0f;
            StopBackDetection();
        }
        else
        {
            
            _timeSincePlayerLost += Time.deltaTime;
            
            if (_timeSincePlayerLost < _backDetectionDuration)
            {
                _canLookBack = true;
                DetectPlayerBack(); 
            }
            else
            {
                _canLookBack = false;
            }
        }

        _playerDetected = _playerDetectedFront || _playerDetectedBack;
    }

    private void StopBackDetection()
    {
        _canLookBack = false;
    }

    private void DetectPlayerFront()
    {
        _playerDetectedFront = false;
        var axisMaxDistance = _frontXAxisMaxDistance * _enemyController.Direction();
        float boxWidth = axisMaxDistance / _frontXAxisBoxAmount;
        float boxHeight = _frontYAxisMaxDistance / _frontYAxisBoxAmount;
        Vector3 position = new Vector3(transform.position.x + _frontXOffset, transform.position.y + _frontYOffset);

        for (int y = 0; y < _frontYAxisBoxAmount; y++)
        {
            for (int x = 0; x < _frontXAxisBoxAmount; x++)
            {
                Vector3 boxCenter = position + new Vector3(x * boxWidth + (boxWidth / 2), 
                    y * boxHeight + (boxHeight / 2));

                Collider2D hitCollider = Physics2D.OverlapBox(boxCenter, new Vector3(boxWidth, boxHeight) / 2, 0, _detectorLayerMask);
                
                if (hitCollider != null) 
                {
                    Vector3 rowCenter = new Vector3(position.x, y * boxHeight + (boxHeight / 2));
                    RaycastHit2D hit = Physics2D.Raycast(rowCenter, boxCenter - rowCenter,
                        Vector2.Distance(rowCenter, boxCenter), _groundLayerMask);
                    
                    if (hit.collider == null && !_playerDetectedFront)
                    {
                        _playerDetectedFront = true;
                        break; 
                    }
                }
            }

            if (_playerDetectedFront)
            {
                break; 
            }
        }
    }

    private void DetectPlayerBack()
    {
        _playerDetectedBack = false;
        var axisMaxDistance = _backXAxisMaxDistance * _enemyController.Direction();
        float boxWidth = -axisMaxDistance / _backXAxisBoxAmount;
        float boxHeight = _backYAxisMaxDistance / _backYAxisBoxAmount;
        Vector3 position = new Vector3((transform.position.x + _backXOffset), transform.position.y + _backYOffset);

        for (int y = 0; y < _backYAxisBoxAmount; y++)
        {
            for (int x = 0; x < _backXAxisBoxAmount; x++)
            {
                Vector3 boxCenter = position + new Vector3(x * boxWidth + (boxWidth / 2), 
                    y * boxHeight + (boxHeight / 2));

                Collider2D hitCollider = Physics2D.OverlapBox(boxCenter, new Vector3(boxWidth, boxHeight) / 2, 0, _detectorLayerMask);
                
                if (hitCollider != null) 
                {
                    Vector3 rowCenter = new Vector3(position.x, y * boxHeight + (boxHeight / 2));
                    RaycastHit2D hit = Physics2D.Raycast(rowCenter, boxCenter - rowCenter,
                        Vector2.Distance(rowCenter, boxCenter), _groundLayerMask);
                    
                    if (hit.collider == null && !_playerDetectedBack)
                    {
                        _playerDetectedBack = true;
                        break; 
                    }
                }
            }

            if (_playerDetectedBack)
            {
                break; 
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (!_showGizmos) return;
        DrawDetectionGizmos(_frontXAxisMaxDistance, _frontYAxisMaxDistance, _frontXAxisBoxAmount, _frontYAxisBoxAmount, _frontXOffset, _frontYOffset,_playerDetectedFront);
        DrawDetectionGizmos(-_backXAxisMaxDistance, _backYAxisMaxDistance, _backXAxisBoxAmount, _backYAxisBoxAmount, -_backXOffset, _backYOffset,_playerDetectedBack);
        
    }

    private void DrawDetectionGizmos(float xAxisMaxDistance, float yAxisMaxDistance, int xAxisBoxAmount, int yAxisBoxAmount, float xOffset, float yOffset,bool playerdetection)
    {
        xAxisMaxDistance *= _enemyController.Direction();
        float boxWidth = xAxisMaxDistance / xAxisBoxAmount;
        float boxHeight = yAxisMaxDistance / yAxisBoxAmount;
        Vector3 position = new Vector3(transform.position.x + xOffset, transform.position.y + yOffset);
        
        if (_showPlayerDetection)
        {
            Gizmos.color = _playerDetected && playerdetection ? _gizmoDetectedColour : _gizmoIdleColour;
            Gizmos.color = new Color(Gizmos.color.r, Gizmos.color.g, Gizmos.color.b, 0.2f);
            
            for (int x = 0; x < xAxisBoxAmount; x++)
            {
                for (int y = 0; y < yAxisBoxAmount; y++)
                {
                    Vector3 boxCenter = position + new Vector3(x * boxWidth + (boxWidth / 2), 
                        y * boxHeight + (boxHeight / 2));
                    Gizmos.DrawCube(boxCenter, new Vector3(boxWidth, boxHeight, 0));
                }
            }
        }
        
        if (_showGrid)
        {
            Gizmos.color = _gizmoGridColour;
            for (int x = 0; x < xAxisBoxAmount; x++)
            {
                for (int y = 0; y < yAxisBoxAmount; y++)
                {
                    Vector3 boxCenter = position + new Vector3(x * boxWidth + (boxWidth / 2), 
                        y * boxHeight + (boxHeight / 2));
                    Gizmos.DrawWireCube(boxCenter, new Vector3(boxWidth, boxHeight, 0));
                }
            }
        }

        if (_showXYLimits)
        {
            Gizmos.color = _gizmoXYLimitsColour;
            Vector3 endPointX = transform.position + new Vector3(xAxisMaxDistance + xOffset, 0 + yOffset, 0);
            Vector3 endPointY = transform.position + new Vector3(0 + xOffset, yAxisMaxDistance + yOffset, 0);
            Gizmos.DrawLine(position, endPointX);
            Gizmos.DrawLine(position, endPointY);
        }

        if (_showGroundDetection)
        {
            Gizmos.color = _gizmoRayColour;
            for (int y = 0; y < yAxisBoxAmount; y++)
            {
                Vector3 rowCenterStart = new Vector3(position.x, position.y + y * boxHeight + (boxHeight / 2));
                Vector3 rowCenterEnd = new Vector3(position.x + xAxisMaxDistance - (boxWidth / 2), rowCenterStart.y, rowCenterStart.z);
                Gizmos.DrawLine(rowCenterStart, rowCenterEnd);
            }
        }
    }
}
