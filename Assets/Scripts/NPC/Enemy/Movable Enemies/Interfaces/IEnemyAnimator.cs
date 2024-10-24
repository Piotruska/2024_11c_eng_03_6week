using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public interface IEnemyAnimator
{
    public void IsGrounded(bool isGrounded);
    public void IsJumping(bool value);
    public void IdleAnimation();
    public void RunAnimation();
    public void JumpAnimation();
    public void AnticipateAnimation();
    public void AttackAnimation();
    public void HitAnimation();
    public void DeadHitAnimation();
    public void SpawnDustParticleEffect(int trigger);
}
