namespace NPC.Enemy.Movable_Enemies.Interfaces
{
    public interface IEnemyController
    {
        public void ChangeState(EnemyState state);
        public EnemyState GetState();
        public bool CanChase();
        public bool CanPatroll();
        public void Stunn(float timeStunned);
        public void isAttackingTrue();
        public void isAttackingFalse();
        public bool isGrounded();
        public bool isPlayerAlive();
        public float Direction();

    }
}