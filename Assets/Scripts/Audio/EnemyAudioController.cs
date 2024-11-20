using UnityEngine;

namespace Audio
{
    public class EnemyAudioController : MonoBehaviour
    {
        private AudioManeger _audioManeger;

        private void Awake()
        {
            _audioManeger = GameObject.FindWithTag("AudioManager")?.GetComponent<AudioManeger>();
        }
        
        public void FierceToothAttackSFX()
        {
            _audioManeger.PlayEnemyCombatSFX(_audioManeger.FierceToothAttack);
        }
        
        public void PatricAttackSFX()
        {
            _audioManeger.PlayEnemyCombatSFX(_audioManeger.PatrickAttack);
        }
        
        public void TotemAttackSFX()
        {
            _audioManeger.PlayEnemyCombatSFX(_audioManeger.TotemAttack);
        }
        public void hit1SFX()
        {
            _audioManeger.PlayEnemyCombatSFX(_audioManeger.hit1);
        }
        
        public void hit2SFX()
        {
            _audioManeger.PlayEnemyCombatSFX(_audioManeger.hit2);
        }
        
        public void hit3SFX()
        {
            _audioManeger.PlayEnemyCombatSFX(_audioManeger.hit3);
        }
        
        public void deathSFX()
        {
            _audioManeger.PlayEnemyCombatSFX(_audioManeger.death);
        }
    }
}