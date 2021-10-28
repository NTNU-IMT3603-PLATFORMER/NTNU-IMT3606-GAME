using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoomBoundary : MonoBehaviour
{
    AudioSource _bossMusic;
    AudioSource _journeyTheme;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    void OnTriggerEnter2D(Collider2D other) {
        _bossMusic = GetComponent<AudioSource>();
        _journeyTheme = GameObject.Find("LevelTheme").GetComponent<AudioSource>();
        _journeyTheme.Stop();
        _bossMusic.Play();
    }
}
