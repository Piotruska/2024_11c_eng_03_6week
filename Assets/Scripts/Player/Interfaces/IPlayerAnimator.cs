namespace Player
{
    public interface IPlayerAnimator
    {
        public void FacingCheck();
        public void IdleAnimation();
        public void WalkAnimation();
        public void JumpAnimation();
        public void DashOn();
        public void HitAnimation();
        public void DeathAnimation();
        public void HasSword(bool hasSword);
        public void AttackAnimation();
        public void ThrowSwordAnimation();
        public void RespawnAnimation();
        public void SpawnDustParticleEffect(int trigger);
    }
}