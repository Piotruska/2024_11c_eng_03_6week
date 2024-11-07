using UnityEngine;

namespace NPC.Enemy.Movable_Enemies
{
    public class EffectAnimator : MonoBehaviour
    {
        private Rigidbody2D _rb;
        [Header("Dust Particles Effect")]
        [SerializeField] private GameObject _dustParticleEffect;
        [SerializeField] private Transform _dustparticleSpawn;
    
        [Header("Attack Effect")]
        [SerializeField] private GameObject _attackEffect;
    
        private string _runDustEffectTrigger = "RunDustEffect";
        private string _jumpDustEffectTrigger = "JumpDustEffect";
        private string _fallDustEffectTrigger = "FallDustEffect";
        private string _dashDustEffectTrigger = "DashDustEffect";
    
        private string _attackEffectTrigger = "AttackEffect";
    
        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }
    
        public void SpawnDustParticleEffect(int trigger)
        {
            var obj = Instantiate(_dustParticleEffect, _dustparticleSpawn.position, _dustparticleSpawn.rotation);
            obj.transform.localScale = _rb.transform.localScale;
            var particleAnimator = obj.GetComponent<Animator>();
            switch (trigger)
            {
                case 1: //run
                    particleAnimator.SetTrigger(_runDustEffectTrigger);
                    break;
                case 2: //jump
                    particleAnimator.SetTrigger(_jumpDustEffectTrigger);
                    break;
            }
        }

        public void PlayAttackEffect(int type)
        {
            var attackEffectAnimator = _attackEffect.GetComponent<Animator>();
            attackEffectAnimator.SetTrigger(_attackEffectTrigger);
        
        }
    }
}
