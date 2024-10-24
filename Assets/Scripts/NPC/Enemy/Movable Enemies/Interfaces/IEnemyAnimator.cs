using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public interface IEnemyAnimator
{
    public void Idle();
    public void Run();
    public void Jump();
    public void Anticipate();
    public void Hit();
    public void DeadHit();
    public void SpawnDustParticles();
}
