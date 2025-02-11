using System;
using UnityEngine;

public class AudioSystem : MonoBehaviour
{
    public static AudioSystem current;

    [Serializable]
    private struct Sound
    {
        public string name;
        public AudioClip clip;

        public Sound(string name, AudioClip clip)
        {
            this.name = name;
            this.clip = clip;
        }
    }

    [SerializeField] private Sound[] musicSounds, sfxSounds;
    [SerializeField] private AudioSource musicSource, sfxSource;

    void Awake()
    {
        if(current == null)
        {
            DontDestroyOnLoad(gameObject);
            current = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        PlayMusic("Theme");
    }

    public bool isMuteMusic() => musicSource.mute;
    public bool isMuteSFX() => sfxSource.mute;

    public float GetVolumeMusic() => musicSource.volume;
    public float GetVolumeSFX() => sfxSource.volume;

    public void PlayMusic(string name)
    {
        Sound sound = Array.Find(musicSounds, x => x.name == name);

        if(sound.Equals(default(Sound))) return;

        musicSource.clip = sound.clip;
        musicSource.Play();
    }

    public void PlaySFX(string name)
    {
        Sound sound = Array.Find(sfxSounds, x => x.name == name);

        if(sound.Equals(default(Sound))) return;

        sfxSource.PlayOneShot(sound.clip);
    }

    public void ToggleMusic()
    {
        musicSource.mute = !musicSource.mute;
    }

    public void ToggleSFX()
    {
        sfxSource.mute = !sfxSource.mute;
    }

    public void MusicVolume(float volume)
    {
        musicSource.volume = volume;
    }

    public void SFXVolume(float volume)
    {
        sfxSource.volume = volume;
    }
}
