namespace NPC.Enemy.Movable_Enemies.Interfaces
{
    public interface IEnemyController
    {
        public void ChangeState(EnemyState state);
        public bool CanChase();
        public float Direction();

    }
}