using System;
using UnityEngine.Audio;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public static AudioManager instance; //Helps to find is we have a second instance of audiomanager when loading scene
    [Header("Sound Categories")]
    public AudioMixerGroup masterSound;
    public AudioMixerGroup ambientMusic;
    public AudioMixerGroup soundEffects;
    public static List<string> musicNames = new List<string>();

    void Awake()
    {
        if (instance == null)
        {
            instance = this; //if instance is null, it means there's no audio manager yet so there should be one

            foreach (Sound s in sounds)
            {
                s.source = gameObject.AddComponent<AudioSource>();
                s.source.clip = s.clip;
                s.source.loop = s.loop;

                if (s.typeOfSound == "Music") //Apply what mixer every sound will get
                {
                    s.source.outputAudioMixerGroup = ambientMusic;
                }
                else if (s.typeOfSound == "SoundEffect")
                {

                    s.source.outputAudioMixerGroup = soundEffects;
                }
            }

            for (int i = 0; i <= musicNames.Count; i++) //Gather the sounds labed as songs into a list
            {
                if (sounds[i].typeOfSound == "Music")
                {
                    musicNames.Add(sounds[i].name);
                }
            }
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);


    }

    private void Start()
    {
        Play(musicNames[0]);
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, item => item.name == name);
        s.source.Stop();
    }

}
[Serializable]
public class Sound
{
    public string name;

    public string typeOfSound;

    public AudioClip clip;

    public bool loop;

    [HideInInspector]
    public AudioSource source;

}