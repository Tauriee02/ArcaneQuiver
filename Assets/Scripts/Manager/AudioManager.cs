using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Sources")]
    public AudioSource musicSource;     
    public AudioSource sfxSource;  

    [Header("Audio Clips")]
    public AudioClip buttonClickClip;
    public AudioClip gameplayMusicClip;
    public AudioClip playerDeathClip;
    public AudioClip gameStartClip;

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
            return;
        }

  
        if (musicSource == null)
            musicSource = gameObject.AddComponent<AudioSource>();

        if (sfxSource == null)
            sfxSource = gameObject.AddComponent<AudioSource>();

        musicSource.loop = true;
        musicSource.playOnAwake = false;

        sfxSource.loop = false;
        sfxSource.playOnAwake = false;
    }

    private void Start()
    {
        // Avvia musica di sottofondo (opzionale)
        // PlayMusic(gameplayMusicClip);
    }



    public void PlayButtonClick()
    {
        PlaySFX(buttonClickClip);
    }

    public void PlayPlayerDeath()
    {
        PlaySFX(playerDeathClip);
    }

    public void PlayGameStart()
    {
        PlaySFX(gameStartClip);
    }

    public void PlayMusic(AudioClip music)
    {
        if (music != null && musicSource != null)
        {
            musicSource.clip = music;
            musicSource.Play();
        }
    }

    public void StopMusic()
    {
        if (musicSource != null && musicSource.isPlaying)
        {
            musicSource.Stop();
        }
    }

    private void PlaySFX(AudioClip clip)
    {
        if (clip != null && sfxSource != null)
        {
            sfxSource.PlayOneShot(clip);
        }
    }
}
