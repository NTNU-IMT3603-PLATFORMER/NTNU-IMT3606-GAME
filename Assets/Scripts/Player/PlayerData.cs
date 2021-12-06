using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour {

    public static PlayerData INSTANCE { get; private set; }

    

    void Awake () {
        INSTANCE = this;
    }

}