using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManeger : MonoBehaviour
{
    public static AudioManeger Instance;

    [Header("Audio Source  Music")]
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _backgroundSource;
    
    
    [Header("Audio Source  SFX")]
    [SerializeField] private AudioSource _sfxCollectiblesSource;
    [SerializeField] private AudioSource _sfxInteractableSource;
    [SerializeField] private AudioSource _sfxMovementSource;
    [SerializeField] private AudioSource _sfxPlayerCombatSource;
    [SerializeField] private AudioSource _sfxEnemyCombatSource;
    [SerializeField] private AudioSource _sfxMenuSource;
    
    [Header("Menu")]
    public AudioClip menuClick;

    [Header("Music")]
    public AudioClip adventureMusic;
    // public AudioClip adventureMusic1;
    // public AudioClip adventureMusic2;
    // public AudioClip adventureMusic4;
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
    
    [Header("SFX interactible")]
    public AudioClip chestOpen;
    
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
    
    private void Awake()
    {
        // Implement singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Prevent destruction on scene load
        }
        else
        {
            Destroy(gameObject); // Destroy duplicates if they exist
        }
    }
    
    private void Start()
    {
        _musicSource.loop = true;
        _backgroundSource.loop = true;
        _musicSource.clip = adventureMusic;
        _backgroundSource.clip = background;
        
        if (!_musicSource.isPlaying)
        {
            _musicSource.Play();
        }
        
        _backgroundSource.Play();
    }

    public void PlaySoundtrackMusic()
    {
        _musicSource.clip = adventureMusic;
        _musicSource.Play();
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
    
    public void PlayMovementSFX(AudioClip clip)
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
    
    public void PlayInteractableSFX(AudioClip clip)
    {
        _sfxInteractableSource.PlayOneShot(clip);
    }

    
    public void PlayMenuSFX(AudioClip clip)
    {
        _sfxMenuSource.PlayOneShot(clip);
    }
}
