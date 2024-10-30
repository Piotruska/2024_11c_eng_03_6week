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
            _audioManeger.PlayMovmementSFX(_audioManeger.walk1);
        }
         
        public void Walk2SFX()
        {
            _audioManeger.PlayMovmementSFX(_audioManeger.walk2);
        }
        
        public void DashSFX()
        {
            _audioManeger.PlayMovmementSFX(_audioManeger.dash);
        }
        
        public void JumpSFX()
        {
            _audioManeger.PlayMovmementSFX(_audioManeger.jump);
        }
        
        public void swing1SFX()
        {
            _audioManeger.PlayEnemyCombatSFX(_audioManeger.swing1);
        }
        
        public void swing2SFX()
        {
            _audioManeger.PlayMovmementSFX(_audioManeger.swing2);
        }
        
        public void swing3SFX()
        {
            _audioManeger.PlayMovmementSFX(_audioManeger.swing3);
        }
        
        public void hit1SFX()
        {
            _audioManeger.PlayEnemyCombatSFX(_audioManeger.hit1);
        }
        
        public void hit2SFX()
        {
            _audioManeger.PlayMovmementSFX(_audioManeger.hit2);
        }
        
        public void hit3SFX()
        {
            _audioManeger.PlayMovmementSFX(_audioManeger.hit3);
        }
        
        public void deathSFX()
        {
            _audioManeger.PlayMovmementSFX(_audioManeger.death);
        }
    }
}