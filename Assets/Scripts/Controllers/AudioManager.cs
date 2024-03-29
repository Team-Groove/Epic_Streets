using UnityEngine;

using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AudioManager: MonoBehaviour
{
    public static AudioManager instance;
    
    public Sound[] sounds;
    public Sound[] musics;

    public float musicVolume;
    public float effectVolume;

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

            s.audioSource.playOnAwake = false;

            s.audioSource.volume = s.volume;
            s.audioSource.pitch = s.pitch;
        }

        foreach (Sound s in musics)
        {
            s.audioSource = gameObject.AddComponent<AudioSource>();
            s.audioSource.clip = s.audioClip;

            s.audioSource.playOnAwake = false;

            s.audioSource.loop = s.loop;

            s.audioSource.volume = s.volume;
            s.audioSource.pitch = s.pitch;
        }
    
        
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += ChangeMusicDependingOfScene;
    }

    private void Update()
    {
        
        musicVolume = AudioSettings.music;
        effectVolume = AudioSettings.effect;

        for (int i = 0; i < musics.Length; i++)
        {
            sounds[i].audioSource.volume = effectVolume;
        }

        for (int i = 0; i < musics.Length; i++)
        {
            musics[i].audioSource.volume = musicVolume;
        }
    }

    public void Play(string name, float pitch = (1), float volume = (0f))
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
      
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        s.audioSource.volume = effectVolume - volume;

        s.audioSource.pitch = pitch;
        s.audioSource.Play();
    }
    public void Stop(string name, float pitch = (1))
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        s.audioSource.pitch = pitch;
        s.audioSource.Stop();
    }
    public void PlayMusic(string name)
    {
       
        Sound m = Array.Find(musics, sound => sound.name == name);
        
        if (m == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        m.audioSource.volume = musicVolume;

        m.audioSource.Play();
     
    }
    public void StopMusic(string name)
    {

        Sound m = Array.Find(musics, sound => sound.name == name);

        if (m == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        m.audioSource.Stop();

    }
    public void StopAllMusic()
    {
        StopMusic("FightMusic");
        StopMusic("MainHubMusic");
        StopMusic("FinalBossMusic");
        StopMusic("MainMenuMusic");
    }

    private void ChangeMusicDependingOfScene(Scene scene, LoadSceneMode mode)
    {
        if (SceneManager.GetSceneByName("MainMenu").isLoaded)
        {
            StopMusic("FightMusic");
            StopMusic("MainHubMusic");
            StopMusic("FinalBossMusic");
            PlayMusic("MainMenuMusic");
        }
        else if (SceneManager.GetSceneByName("MainHub").isLoaded)
        {
            StopMusic("FightMusic");
            StopMusic("MainMenuMusic");
            StopMusic("FinalBossMusic");
            PlayMusic("MainHubMusic");
        }
        else if (SceneManager.GetSceneByName("Level_1").isLoaded)
        {
            StopMusic("MainMenuMusic");
            StopMusic("MainHubMusic");
            StopMusic("FinalBossMusic");
            PlayMusic("FightMusic");
        }
        else if (SceneManager.GetSceneByName("Level_6").isLoaded)
        {
            StopMusic("MainMenuMusic");
            StopMusic("MainHubMusic");
            StopMusic("FightMusic");
            PlayMusic("FinalBossMusic");
        }
    }

}

