using System.Collections;
using System.Collections.Generic;
using NPC.Enemy.Movable_Enemies.Interfaces;
using UnityEngine;

public class EmenyCombat : MonoBehaviour , IEnemyCombat
{
    private IEnemyController _enemyController;

    private void Awake()
    {
        _enemyController = GetComponent<IEnemyController>();
    }
}
