using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlatformGlue : MonoBehaviour {

    CharacterController2D _characterController2D;

    void Start () {
        _characterController2D = GetComponent<CharacterController2D>();
        _characterController2D.eventOnGrounded.AddListener(CharacterController2D_OnGrounded);
        _characterController2D.eventOnLeftGround.AddListener(CharacterController2D_OnLeftGround);
    }

    void CharacterController2D_OnGrounded (Collider2D groundCollider) {
        if (groundCollider.GetComponentInParent<MovingPlatform>() != null) {
            _characterController2D.SetParent(groundCollider.transform);
        }
    }

    void CharacterController2D_OnLeftGround () {
        _characterController2D.SetParent(null);
    }

}