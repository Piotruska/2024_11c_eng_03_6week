namespace Player.Interfaces
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
        public void GroundAttackAnimation(int attack);
        public void AirAttackAnimation();
        public void ThrowSwordAnimation();
        public void RespawnAnimation();
        public void SpawnDustParticleEffect(int trigger);
    }
}