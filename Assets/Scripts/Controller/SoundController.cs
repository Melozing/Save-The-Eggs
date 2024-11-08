using Melozing;
using System;
using UnityEngine;

public class SoundController : ManualSingletonMono<SoundController>
{
    [SerializeField]
    private Sound[] musicSounds;
    [SerializeField]
    private Sound[] sfxSounds;

    [SerializeField]
    private AudioSource musicSource;
    [SerializeField]
    private AudioSource sfxSource;

    private bool isSFXEnabled = true;
    private bool isMusicEnabled = true;

    private void Start()
    {
        isSFXEnabled = PlayerPrefs.GetInt(ConstantsPlayerPrefs.isSFXEnabled, 1) == 1;
        isMusicEnabled = PlayerPrefs.GetInt(ConstantsPlayerPrefs.isMusicEnabled, 1) == 1;

        PlayMusic(Constants.ThemeSound);
        if (!PlayerPrefs.HasKey(ConstantsPlayerPrefs.isSFXEnabled))
        {
            PlayerPrefs.SetInt(ConstantsPlayerPrefs.isSFXEnabled, 1);
        }
        if (!PlayerPrefs.HasKey(ConstantsPlayerPrefs.isMusicEnabled))
        {
            PlayerPrefs.SetInt(ConstantsPlayerPrefs.isMusicEnabled, 1);
        }

        isSFXEnabled = PlayerPrefs.GetInt(ConstantsPlayerPrefs.isSFXEnabled) == 1;
        isMusicEnabled = PlayerPrefs.GetInt(ConstantsPlayerPrefs.isMusicEnabled) == 1;
    }

    public void ToggleMusic(bool isOn)
    {
        if (isOn)
        {
            OpenAllMusic();
            PlayMusic(Constants.ThemeSound);
        }
        else
        {
            StopAllMusic();
        }
    }

    public void ToggleSFX(bool isOn)
    {
        if (isOn)
        {
            OpenAllSFX();
        }
        else
        {
            StopAllSFX();
        }
    }

    public void PlayMusic(string clipName)
    {
        if (!isMusicEnabled) return;

        Sound s = Array.Find(musicSounds, sound => sound.name == clipName);
        if (s == null)
        {
            Debug.LogError("Sound not Found");
        }
        else
        {
            musicSource.clip = s.clip;
            if (!musicSource.isPlaying && musicSource.clip != null)
            {
                musicSource.Play();
            }
            else
            {
                musicSource.UnPause();

            }
        }
    }

    public void PlaySFX(string clipName)
    {
        if (!isSFXEnabled) return; // Do not play SFX if it is disabled

        Sound s = Array.Find(sfxSounds, sound => sound.name == clipName);
        if (s == null)
        {
            Debug.LogError("Sound not Found");
        }
        else
        {
            sfxSource.PlayOneShot(s.clip);
        }
    }

    public void StopAllMusic()
    {
        isMusicEnabled = false;
        PlayerPrefs.SetInt(ConstantsPlayerPrefs.isMusicEnabled, 0);

        if (musicSource.isPlaying)
        {
            musicSource.Pause();
        }

        foreach (var sound in musicSounds)
        {
            if (sound.clip != null && musicSource.clip == sound.clip)
            {
                musicSource.Pause();
            }
        }
    }

    public void StopAllSFX()
    {
        isSFXEnabled = false;
        PlayerPrefs.SetInt(ConstantsPlayerPrefs.isSFXEnabled, 0);
        if (sfxSource.isPlaying)
        {
            sfxSource.Stop();
        }

        foreach (var sound in sfxSounds)
        {
            if (sound.clip != null && sfxSource.clip == sound.clip)
            {
                sfxSource.Stop();
            }
        }
    }

    public void OpenAllSFX()
    {
        isSFXEnabled = true;
        PlayerPrefs.SetInt(ConstantsPlayerPrefs.isSFXEnabled, 1);
    }

    public void OpenAllMusic()
    {
        isMusicEnabled = true;
        PlayerPrefs.SetInt(ConstantsPlayerPrefs.isMusicEnabled, 1);
    }
    public void SetSFXVolume(float volume)
    {
        if (sfxSource != null)
        {
            sfxSource.volume = Mathf.Clamp(volume, 0f, 1f);
        }
    }
}
