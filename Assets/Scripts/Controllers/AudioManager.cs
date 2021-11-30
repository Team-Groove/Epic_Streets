using UnityEngine;
using UnityEngine.Audio;
using System;

public class AudioManager: MonoBehaviour
{

    public static AudioManager instance;
    
    public Sound[] sounds;
    public Sound[] musics;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)     
        {
            s.audioSource = gameObject.AddComponent<AudioSource>();
            s.audioSource.clip = s.audioClip;

            s.audioSource.volume = s.volume;
            s.audioSource.pitch = s.pitch;
        }

        foreach (Sound s in musics)
        {
            s.audioSource = gameObject.AddComponent<AudioSource>();
            s.audioSource.clip = s.audioClip;

            s.audioSource.loop = s.loop;

            s.audioSource.volume = s.volume;
            s.audioSource.pitch = s.pitch;
        }
    }

    private void Start()
    {
        PlayMusic("FightMusic");
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
      
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
       
        s.audioSource.Play();
    }

    public void PlayMusic(string name)
    {
       
        Sound m = Array.Find(musics, sound => sound.name == name);
        if (m == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        m.audioSource.Play();
     
    }

}

