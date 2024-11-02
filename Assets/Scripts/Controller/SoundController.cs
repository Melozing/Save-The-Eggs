using System;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public static SoundController Instance { get; private set; }

    [SerializeField]
    private Sound[] musicSounds;
    [SerializeField]
    private Sound[] sfxSounds;

    [SerializeField]
    private AudioSource musicSource;
    [SerializeField]
    private AudioSource sfxSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PlayMusic(Constants.ThemeSound);
    }

    public void PlayMusic(string clipName)
    {
        Sound s = Array.Find(musicSounds, sound => sound.name == clipName);
        if (s == null)
        {
            Debug.LogError("Sound not Found");
        }
        else
        {
            musicSource.clip = s.clip;
            musicSource.Play();
        }
    }

    public void PlaySFX(string clipName)
    {
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

    public void StopBackgroundMusic()
    {

    }

    public void SetMusicVolume(float volume)
    {

    }

    public void SetSFXVolume(float volume)
    {
        if (sfxSource != null)
        {
            sfxSource.volume = Mathf.Clamp(volume, 0f, 1f);
        }
    }

    private AudioClip GetSFXClipByName(string name)
    {
        return null;
    }
}
