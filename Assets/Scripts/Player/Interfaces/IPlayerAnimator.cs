namespace Player
{
    public interface IPlayerAnimator
    {
        public void Jump();
        public void Hit();
        public void Death();
        public void HasSword(bool hasSword);
        public void Attack();
        public void ThrowSword();
        public void Respawn();
    }
}