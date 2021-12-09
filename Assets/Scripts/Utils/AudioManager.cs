using System;
using UnityEngine;


/// <summary>
/// AudioManager hold sounds and can either play or stop them at command.
/// </summary>
public class AudioManager : MonoBehaviour {

    private const string TUTORIAL_SCENE_NAME = "Level_tutorial";
    private const string MAIN_MENU_SCENE_NAME = "StartMenu";

    [SerializeField]
    Sound[] _sounds;

    String _scene;

    public static AudioManager instance { get; private set; }

    /// <summary>
    /// PlaySound plays a specific sound from the AudioManager sound array based on the sound name imput. 
    /// </summary>
    /// <param name="soundName">soundName is the name of the sound you want to play as a string.</param>
    public void PlaySound(string soundName) {
        Sound soundToPlay = Array.Find(_sounds, sound => sound.name == soundName);

        // If the sound is already playing, it should not be played again
        if (soundToPlay == null || soundToPlay.source.isPlaying) {
            return;
        }
        soundToPlay.source.Play();
    }

    /// <summary>
    /// StopSound stops a specific sound from the AudioManager sound array based on the sound name imput. 
    /// </summary>
    /// <param name="soundName"></param>
    public void StopSound(string soundName) {
        Sound soundToPlay = Array.Find(_sounds, sound => sound.name == soundName);

        // If the sound is not playing, it should not be stopped
        if (soundToPlay == null || !soundToPlay.source.isPlaying) {
            return;
        }
        soundToPlay.source.Stop();
    }

    void Awake() {

        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound sound in _sounds) {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
        }
    }

}
