using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager audioManager; 

    void Awake()
    {
        if (audioManager == null)
        {
            audioManager = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound sound in sounds)
        {
           sound.audioSource = gameObject.AddComponent<AudioSource>();

            sound.audioSource.clip = sound.audioClip;
            sound.audioSource.volume = sound.volume;
            sound.audioSource.pitch = sound.pitch;
            sound.audioSource.loop = sound.loop;
        }        
    }
    private void Start()
    {
        PlaySound("Theme");
    }

    public void StopAllSounds()
    {
        foreach (Sound sound in sounds)
        {
            sound.audioSource.Stop();
        }
    }

    public void PlaySound(string name)
    {
        Sound _sound = Array.Find(sounds, sound => sound.name == name);

        if (_sound == null)
        {
            Debug.LogWarning("Sound: " + name + " not found");
            return;
        }

        _sound.audioSource.Play();
    }
}
