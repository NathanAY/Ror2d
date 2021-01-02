using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Game;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public Sound[] sounds;
    
    void Start()
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
        foreach (Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.sound;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
        }
        // AudioClip clip = Resources.Load<AudioClip>("Sounds/shoot");
        Play("Theme");
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Play();
    }
}
