using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public Sound[] musicSounds;
    private AudioSource musicSource, sfxSource, loopSoundSource;

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
        AudioSource[] audioSourceList;
        audioSourceList = GetComponents<AudioSource>();

        musicSource = audioSourceList[0];
        sfxSource = audioSourceList[1];
        loopSoundSource = audioSourceList[2];

        PlayMusic("Theme");
    }

    public void PlayMusic(string name)
    {
        musicSource.clip = FindSource(name);
        musicSource.Play();
        
    }

    public void PlaySFX(string name)
    {
        sfxSource.PlayOneShot(FindSource(name));
        
    }

    public void PlayLoopSound(string name)
    {
        loopSoundSource.clip = FindSource(name);
        loopSoundSource.Play();
    }

    public void StopLoopSound()
    {
        loopSoundSource.Stop();
    }

    AudioClip FindSource(string name)
    {
        Sound s = Array.Find(musicSounds, x => x.name == name);
        if (s == null)
        {
            Debug.Log("Sound Not Found");
            return null;
        }
        else
        {
            return s.clip;
        }
    }
}