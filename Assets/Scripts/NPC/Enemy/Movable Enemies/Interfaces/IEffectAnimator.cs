namespace NPC.Enemy.Movable_Enemies.Interfaces
{
    public interface IEffectAnimator
    {
        public void SpawnDustParticleEffect(int trigger);
        public void PlayAttackEffect(int type);

    }
}