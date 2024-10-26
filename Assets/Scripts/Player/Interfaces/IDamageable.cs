namespace Player
{
    public interface IDamageable
    {
        public void Hit(float damageAmount);
        public bool isDead();
    }
}