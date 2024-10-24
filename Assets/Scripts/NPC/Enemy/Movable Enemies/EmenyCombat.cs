using System.Collections;
using System.Collections.Generic;
using NPC.Enemy.Movable_Enemies.Interfaces;
using UnityEngine;

public class EmenyCombat : MonoBehaviour , IEnemyCombat
{
    private IEnemyController _enemyController;
    
    [Header("Attack HitBox Transforms")] 
    [SerializeField]
    private Transform center;
    
    [Header("Attack Configurations")] 
    [SerializeField]
    private float radious;
    
    [Header("Gizmos Options")]
    [SerializeField] private bool _showMovementGizmos = true;

    private void Awake()
    {
        _enemyController = GetComponent<IEnemyController>();
    }
}
