namespace NPC.Enemy.Movable_Enemies.Interfaces
{
    public interface IEnemyController
    {
        public void ChangeState(EnemyState state);
        public EnemyState GetState();
        public bool CanChase();
        public bool CanPatroll();
        public float Direction();

    }
}