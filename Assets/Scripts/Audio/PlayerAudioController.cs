using System;
using UnityEngine;

namespace Audio
{
    public class PlayerAudioController : MonoBehaviour
    {
        private AudioManeger _audioManeger;

        private void Awake()
        {
            _audioManeger = GameObject.FindWithTag("AudioManager")?.GetComponent<AudioManeger>();
        }

        public void Walk1SFX()
        {
            _audioManeger.PlayMovementSFX(_audioManeger.walk1);
        }
         
        public void Walk2SFX()
        {
            _audioManeger.PlayMovementSFX(_audioManeger.walk2);
        }
        
        public void DashSFX()
        {
            _audioManeger.PlayMovementSFX(_audioManeger.dash);
        }
        
        public void JumpSFX()
        {
            _audioManeger.PlayMovementSFX(_audioManeger.jump);
        }
        
        public void swing1SFX()
        {
            _audioManeger.PlayPlayerCombatSFX(_audioManeger.swing1);
        }
        
        public void swing2SFX()
        {
            _audioManeger.PlayPlayerCombatSFX(_audioManeger.swing2);
        }
        
        public void swing3SFX()
        {
            _audioManeger.PlayPlayerCombatSFX(_audioManeger.swing3);
        }
        
        public void hit1SFX()
        {
            _audioManeger.PlayPlayerCombatSFX(_audioManeger.hit1);
        }
        
        public void hit2SFX()
        {
            _audioManeger.PlayPlayerCombatSFX(_audioManeger.hit2);
        }
        
        public void hit3SFX()
        {
            _audioManeger.PlayPlayerCombatSFX(_audioManeger.hit3);
        }
        
        public void deathSFX()
        {
            _audioManeger.PlayPlayerCombatSFX(_audioManeger.death);
        }
    }
}