using System.Collections;
using NPC.Enemy.Movable_Enemies.Interfaces;
using UnityEngine;

public class EnemyPlayerDetector : MonoBehaviour, IEnemyPlayerDetector
{
    private IEnemyController _enemyController;
    private bool _playerDetected;
    private float _innerxAxisMaxDistance;
    private float _innerxOffset;

    [Header("RayCast / OverlapBox Configuration")] 
    
    [SerializeField]
    private float _xAxisMaxDistance; 
    [SerializeField]
    private float _yAxisMaxDistance; 
    [SerializeField]
    private int _xAxisBoxAmount; 
    [SerializeField]
    private int _yAxisBoxAmount; 
    
    [Header("Detection Configuration")] 
    
    [SerializeField]
    private float _yOffset = 0.3f;
    [SerializeField]
    private float _xOffset = 0.3f;
    [SerializeField]
    private float _detectionDelay = 0.3f; 
    [SerializeField]
    private LayerMask _detectorLayerMask; 

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
    private bool _showDetection;
    [SerializeField]
    private Color _gizmoIdleColour = Color.green; 
    [SerializeField]
    private Color _gizmoDetectedColour = Color.red;

    private void Awake()
    {
        _enemyController = GetComponent<IEnemyController>();
    }

    private void Update()
    {
        _innerxAxisMaxDistance = Mathf.Abs(_xAxisMaxDistance) * Mathf.Sign(_enemyController.Direction());
        _innerxOffset = Mathf.Abs(_xOffset) * Mathf.Sign(_enemyController.Direction());
        
        if (_enemyController.CanChase())
        {
            StartCoroutine(DetectionCoroutine());
        }
    }

    private IEnumerator DetectionCoroutine()
    {
        yield return new WaitForSeconds(_detectionDelay);
        DetectPlayer();
    }

    private void DetectPlayer()
    {
        _playerDetected = false; 
        
        float boxWidth = _innerxAxisMaxDistance / _xAxisBoxAmount;
        float boxHeight = _yAxisMaxDistance / _yAxisBoxAmount;

        Vector3 position = new Vector3(transform.position.x + _innerxOffset, transform.position.y+ _yOffset);

        _playerDetected = false;
        for (int x = 0; x < _xAxisBoxAmount; x++)
        {
            for (int y = 0; y < _yAxisBoxAmount; y++)
            {
                
                Vector3 boxCenter = position + new Vector3(x * boxWidth + (boxWidth/2), 
                                                                     y * boxHeight + (boxHeight/2));
                Collider2D hitCollider = Physics2D.OverlapBox(boxCenter, new Vector3(boxWidth, boxHeight) / 2,0, _detectorLayerMask);

                if (!_playerDetected)
                {
                    _playerDetected = hitCollider == null ? false : true;
                }
                
                
            }
        }
        
    }

    private void OnDrawGizmos()
    {
        if(!_showGizmos) return;

        float boxWidth = _innerxAxisMaxDistance / _xAxisBoxAmount;
        float boxHeight = _yAxisMaxDistance / _yAxisBoxAmount;
        Vector3 position = new Vector3(transform.position.x+ _innerxOffset, transform.position.y+ _yOffset);
        Vector3 startPoint = new Vector3(transform.position.x+ _innerxOffset,transform.position.y + _yOffset);
        
        if (_showDetection)
        {
            Gizmos.color = _playerDetected ? _gizmoDetectedColour : _gizmoIdleColour;
            Gizmos.color = new Color(Gizmos.color.r, Gizmos.color.g, Gizmos.color.b, 0.2f);
            
            for (int x = 0; x < _xAxisBoxAmount; x++)
            {
                for (int y = 0; y < _yAxisBoxAmount; y++)
                {
                    Vector3 boxCenter = position + new Vector3(x * boxWidth + (boxWidth/2), 
                        y * boxHeight + (boxHeight/2));
                    Gizmos.DrawCube(boxCenter, new Vector3(boxWidth, boxHeight,0 ));
                }
            }
        }
        
        
        
        if ( _showGrid)
        {
            Gizmos.color = _gizmoGridColour;
            
            for (int x = 0; x < _xAxisBoxAmount; x++)
            {
                for (int y = 0; y < _yAxisBoxAmount; y++)
                {
                    Vector3 boxCenter = position + new Vector3(x * boxWidth + (boxWidth/2), 
                        y * boxHeight + (boxHeight/2));
                    Gizmos.DrawWireCube(boxCenter, new Vector3(boxWidth, boxHeight,0 ));
                }
            }
        }

        if (_showXYLimits)
        {
            Gizmos.color = _gizmoXYLimitsColour; 
            Vector3 endPointX = transform.position + new Vector3(_innerxAxisMaxDistance+ _innerxOffset, 0 + _yOffset, 0);
            Vector3 endPointY = transform.position + new Vector3(0+ _innerxOffset, _yAxisMaxDistance + _yOffset,0 );
            Gizmos.DrawLine(startPoint, endPointX);
            Gizmos.DrawLine(startPoint, endPointY);
        }
        
        
    }
}
