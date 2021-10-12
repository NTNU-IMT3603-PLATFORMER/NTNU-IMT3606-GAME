using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PB_Data : MonoBehaviour {

    public GameObject player { get; private set; }

    void Start () {
        player = GameObject.FindWithTag("Player");
    }

}
