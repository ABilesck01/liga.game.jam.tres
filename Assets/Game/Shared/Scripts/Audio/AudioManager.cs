using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using Random = UnityEngine.Random;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private SoundController controller;
    [SerializeField] private AudioMixerGroup mixerGroup;
    //public Sound[] sounds;

    public static AudioManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }


        foreach (Sound item in controller.sounds)
        {
            item.source = gameObject.AddComponent<AudioSource>();
            item.source.clip = item.clip;
            item.source.volume = item.volume;
            item.source.pitch = item.pitch;
            item.source.loop = item.loop;
            item.source.outputAudioMixerGroup = mixerGroup;
        }
    }

    public void Play(string name, bool stopSounds = false)
    {
        if(stopSounds)
            StopAllAudios();

        Sound s = Array.Find(controller.sounds, sound => sound.name == name);

        if(s == null)
        {
            return;
        }

        if(s.source.isPlaying)
            s.source.Stop();
        if(s.variablePitch)
        {
            s.pitch = Random.Range(0.8f, 1.2f);
        }
        if (s != null)
            s.source.Play();
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(controller.sounds, sound => sound.name == name);
        if (s != null)
            s.source.Stop();
    }

    public void StopAllAudios()
    {
        foreach (Sound s in controller.sounds)
            s.source?.Stop();
    }
}
