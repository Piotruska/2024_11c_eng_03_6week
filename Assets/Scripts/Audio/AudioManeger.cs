using UnityEngine;

namespace Audio
{
    public class AudioManeger : MonoBehaviour
    {
        [Header("Audio Source  Music")]
        [SerializeField] private AudioSource _musicSource;
        [SerializeField] private AudioSource _backgroundSource;
    
        [Header("Audio Source  SFX")]
        [SerializeField] private AudioSource _sfxCollectiblesSource;
        [SerializeField] private AudioSource _sfxMovementSource;
        [SerializeField] private AudioSource _sfxPlayerCombatSource;
        [SerializeField] private AudioSource _sfxEnemyCombatSource;

    
        [Header("Music")]
        public AudioClip adventureMusic;
        public AudioClip johnyEndPointMusic;
        public AudioClip endingMusic;
        [Header("Background")]
        public AudioClip background;
        [Header("SFX collectibles")]
        public AudioClip coidPickup;
        public AudioClip diamondPickup;
        public AudioClip swordPickup;
        public AudioClip potionPickup;
        public AudioClip potionUse;
        public AudioClip keyGet;
    
        [Header("SFX Combat")]
        public AudioClip FierceToothAttack;
        public AudioClip PatrickAttack;
        public AudioClip TotemAttack;
        public AudioClip hit1;
        public AudioClip hit2;
        public AudioClip hit3;
        public AudioClip death;
        public AudioClip swing1;
        public AudioClip swing2;
        public AudioClip swing3;
    
        [Header("SFX Movment")]
        public AudioClip walk1;
        public AudioClip walk2;
        public AudioClip jump;
        public AudioClip dash;
        void Start()
        {
            _musicSource.loop = true;
            _backgroundSource.loop = true;
            _musicSource.clip = adventureMusic;
            _backgroundSource.clip = background;
            _musicSource.Play();
            _backgroundSource.Play();
        }

        public void PlayEndingMusic()
        {
            _musicSource.clip = endingMusic;
            _musicSource.Play();
        }

        public void PlayCollectableSFX(AudioClip clip)
        {
            _sfxCollectiblesSource.PlayOneShot(clip);
        }
    
        public void PlayMovmementSFX(AudioClip clip)
        {
            _sfxMovementSource.PlayOneShot(clip);
        }
    
        public void PlayEnemyCombatSFX(AudioClip clip)
        {
            _sfxEnemyCombatSource.PlayOneShot(clip);
        }
    
        public void PlayPlayerCombatSFX(AudioClip clip)
        {
            _sfxPlayerCombatSource.PlayOneShot(clip);
        }
    }
}
