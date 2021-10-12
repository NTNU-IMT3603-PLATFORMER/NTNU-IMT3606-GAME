using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PB_Data : MonoBehaviour {

    [SerializeField, Tooltip("The speed of the boss")] float _speed; 

    public GameObject player { get; private set; }

    public CharacterController2D characterController2D { get; private set; }
    public float speed => _speed;

    void Start () {
        player = GameObject.FindWithTag("Player");
        characterController2D = GetComponentInParent<CharacterController2D>();
    }

}
