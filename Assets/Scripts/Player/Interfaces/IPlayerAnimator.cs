namespace Player
{
    public interface IPlayerAnimator
    {
        public void FacingCheck();
        public void Idle();
        public void Walk();
        public void Jump();
        public void DashOn();
        public void Hit();
        public void Death();
        public void HasSword(bool hasSword);
        public void Attack();
        public void ThrowSword();
        public void Respawn();
    }
}