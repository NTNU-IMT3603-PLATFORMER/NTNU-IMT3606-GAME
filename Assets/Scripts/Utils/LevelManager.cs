using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public Transform respawnPoint;
    [SerializeField]
    GameObject player;

    public void Awake()
    {
        instance = this;
    }

    public void Respawn()
    {
        Instantiate(player, respawnPoint.position, Quaternion.identity); 
    }

}


