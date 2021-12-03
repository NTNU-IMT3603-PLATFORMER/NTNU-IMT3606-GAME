using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoomBoundary : MonoBehaviour
{
    AudioSource _bossMusic;
    AudioSource _journeyTheme;

    void Start(){
         _bossMusic = GetComponent<AudioSource>();
        _journeyTheme = GameObject.Find("LevelTheme").GetComponent<AudioSource>();
    }
    
    void OnTriggerEnter2D(Collider2D other) {
        _journeyTheme.Stop();
        if (_bossMusic.isPlaying) {
            return;
        }
        _bossMusic.Play();
    }
}
