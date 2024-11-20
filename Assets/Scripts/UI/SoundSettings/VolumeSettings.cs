using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private Slider _masterSlider;
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _sfxSlider;
    [SerializeField] private Toggle _tickToggle;
    private AudioManeger _audioManeger;

    private void Awake()
    {
        _audioManeger = GameObject.FindWithTag("AudioManager")?.GetComponent<AudioManeger>();
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey("masterVolume") && PlayerPrefs.HasKey("musicVolume") && PlayerPrefs.HasKey("sfxVolume"))
        {
            LoadVolume();
        }
        else
        {
            SetMasterVolume();
            SetMusicVolume();
            SetSfxVolume();
        }
    }

    public void SetMasterVolume()
    {
        float volume = _masterSlider.value;
        _audioMixer.SetFloat("Master", Mathf.Log10(volume)*20);
        PlayerPrefs.SetFloat("masterVolume",volume);
    }
    
    public void SetMusicVolume()
    {
        float volume = _musicSlider.value;
        _audioMixer.SetFloat("Music", Mathf.Log10(volume)*20);
        PlayerPrefs.SetFloat("musicVolume",volume);
    }
    
    public void SetSfxVolume()
    {
        float volume = +_sfxSlider.value;
        _audioMixer.SetFloat("SFX", Mathf.Log10(volume)*20);
        PlayerPrefs.SetFloat("sfxVolume",volume);
    }

    private void LoadVolume()
    {
        _masterSlider.value = PlayerPrefs.GetFloat("masterVolume");
        _musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        _sfxSlider.value = PlayerPrefs.GetFloat("sfxVolume");
        var menuVolume = PlayerPrefs.GetFloat("menuVolume");
        if (menuVolume < 0) _tickToggle.isOn = false; else _tickToggle.isOn = true;
        _audioMixer.SetFloat("Menu", menuVolume);
        
    }
    
    public void PressToggle()
    {
        _audioManeger.PlayMenuSFX(_audioManeger.menuClick);
        float volume = -80f;
        if (_tickToggle.isOn)
        {
            volume = 0;
        }
        _audioMixer.SetFloat("Menu", volume);
        PlayerPrefs.SetFloat("menuVolume",volume);
    }
}
