using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerTransformationData {

    [SerializeField]
    GameObject _hitBox;

    //Refer to CharacterController2D for variable descriptions


    // TODO: Complete the rest of the fields that should be updated when transforming
    // øk gravity
    // lower jump height
    // make slower
    // enable wall slide
    // enable wall jump
    // disable dashing
    // decrease extra air jumps
    // increase health
    // increase hitpoints

    // Change sprite with spriteRenderer
    // change hitbox with rigidBody2D
    
    
    [SerializeField]
    float _jumpHeight;

    public float jumpHeight => _jumpHeight;
    public GameObject hitBox => _hitBox;
}
  
