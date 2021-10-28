using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;



public class AudioManager : MonoBehaviour
{
    [SerializeField]
    Sound[] _sounds;

    public static AudioManager instance { get; private set;}

    // Start is called before the first frame update
    void Awake()
    {

        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

      foreach(Sound s in _sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }  
    }

   
    public void PlaySound (string name)
    {
        Sound s = Array.Find(_sounds, sound => sound.name == name);
        if(s == null)
        {
            return;
        }
        s.source.Play();
    }

     void Start()
    {
        PlaySound("theme");
    }
}
