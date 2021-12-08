using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

/// <summary>
/// Sound contains the attributes for each sound.
/// </summary>
[System.Serializable]
public class Sound {
    /// <summary>
    /// name is the name of the sound.
    /// </summary>
    public string name;

    /// <summary>
    /// clip is the sound clip.
    /// </summary>
    public AudioClip clip;

    /// <summary>
    /// volume holds how loud the sound should be playing.
    /// </summary>
    [Range(0f, 1f)]
    public float volume;

    /// <summary>
    /// pitch holds the pitch of the sound.
    /// </summary>
    [Range(.1f, 3f)]
    public float pitch;

    /// <summary>
    /// loop determines if the song should loop or not.
    /// </summary>
    public bool loop;

    /// <summary>
    /// source holds the sound source.
    /// </summary>
    [HideInInspector]
    public AudioSource source;

}
