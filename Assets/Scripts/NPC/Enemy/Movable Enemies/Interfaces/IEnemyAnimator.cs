using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public interface IEnemyAnimator
{
    public void IdleAnimation();
    public void RunAnimation();
    public void JumpAnimation();
    public void AnticipateAnimation();
    public void AttackAnimation();
    public void HitAnimation();
    public void DeadHitAnimation();
    public void SpawnDustParticles();
}
